using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DormManage.BLL.DormManage;
using System.Web;

namespace DormManage.BLL.AssignRoom
{
    public class AssignRoomBLL
    {
        private TB_AssignRoomDAL _mTB_AssignRoomDAL = null;
        private TB_BedDAL _mTB_BedDAL = null;
        private TB_EmployeeCheckInDAL _mTB_EmployeeCheckInDAL = null;
        private Database _db;
        private DbConnection _connection;
        private DbTransaction _tran;

        public AssignRoomBLL()
        {
            _mTB_AssignRoomDAL = new TB_AssignRoomDAL();
            _mTB_BedDAL = new TB_BedDAL();
            _mTB_EmployeeCheckInDAL = new TB_EmployeeCheckInDAL();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstRoomID"></param>
        /// <returns></returns>
        public bool Add(List<int> lstRoomID)
        {
            TB_AssignRoom mTB_AssignRoom = null;
            int siteID = SessionHelper.Get(HttpContext.Current, TypeManager.Admin) == null ?
                ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).SiteID :
                ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).SiteID;
            string operatorUser = SessionHelper.Get(HttpContext.Current, TypeManager.Admin) == null ?
                ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount :
                ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account;
            DataTable dtBed = null;
            try
            {
                foreach (var roomID in lstRoomID)
                {
                    //获取到该房间下的所有床位
                    dtBed = _mTB_BedDAL.GetTable(new TB_Bed() { RoomID = roomID, SiteID = siteID });
                    foreach (DataRow dr in dtBed.Rows)
                    {
                        if (Convert.ToInt32(dr["Status"]) == (int)TypeManager.BedStatus.Busy
                            || Convert.ToInt32(dr["Status"]) == (int)TypeManager.BedStatus.Occupy)
                            continue;
                        mTB_AssignRoom = new TB_AssignRoom
                        {
                            RoomID = roomID,
                            SiteID = siteID,
                            BedID = Convert.ToInt32(dr[TB_Bed.col_ID]),
                            Creator = operatorUser
                        };
                        if (_mTB_AssignRoomDAL.GetTable(mTB_AssignRoom).Select("[BedID]='" + dr[TB_Bed.col_ID] + "'").Length <= 0)
                        {
                            _mTB_AssignRoomDAL.Create(mTB_AssignRoom);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="strCardNo">身份证号码</param>
        /// <param name="strStatus">0为未分配，1为已分配</param>
        /// <returns></returns>
        public DataTable GetPagerData(ref Pager pager, string strCardNo, string strStatus)
        {
            return _mTB_AssignRoomDAL.GetTable(ref pager, strCardNo, strStatus);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="strCardNo">身份证号码</param>
        /// <param name="strStatus">0为未分配，1为已分配</param>
        /// <returns></returns>
        public DataTable GetAssignedData(string strCardNo, string strStatus)
        {
            return _mTB_AssignRoomDAL.GetTable(strCardNo, strStatus);
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="intID"></param>
        public void Remove(int intID)
        {
            _mTB_AssignRoomDAL.Delete(intID, null, DBO.GetInstance());
        }


        /// <summary>
        /// 确认入住
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="intBedID"></param>
        /// <param name="intEmployeeCheckInID"></param>
        public void ConfirmCheckIn(int intID, int intBedID, int intEmployeeCheckInID)
        {
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                //更新床位状态为已入住
                _mTB_BedDAL.Update(intBedID, _tran, _db, TypeManager.BedStatus.Busy);
                //更新入住记录为有效
                _mTB_EmployeeCheckInDAL.Update(intEmployeeCheckInID, TypeManager.IsActive.Valid, _tran, _db);
                //删除分配记录
                _mTB_AssignRoomDAL.Delete(intID, _tran, _db);
                //提交事务
                _tran.Commit();
            }
            catch (Exception ex)
            {
                //回滚事务
                _tran.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                _connection.Close();
            }
        }

        /// <summary>
        /// 房间分配
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        public bool AssignRoom(TB_EmployeeCheckIn tb_EmployeeCheckIn)
        {
            //启用事务
            var bAssign = true;
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                //更新床位状态为已分配未入住（已修改分配且入住）
                _mTB_BedDAL.Update(tb_EmployeeCheckIn.BedID, _tran, _db, TypeManager.BedStatus.Busy);
                //添加入住记录，注意现在的入住记录是无效的(已修改未一键分配，入住记录有效)
                _mTB_EmployeeCheckInDAL.Create(tb_EmployeeCheckIn, _tran, _db);
                //删除分配信息
                _mTB_BedDAL.DeleteAssignDormArea(tb_EmployeeCheckIn.CardNo, _tran, _db);
                //提交事务
                _tran.Commit();
            }
            catch (Exception ex)
            {
                //回滚事务
                bAssign = false;
                _tran.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                _connection.Close();
            }
            return bAssign;
        }

        /// <summary>
        /// 取消入住
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="intBedID"></param>
        /// <param name="intEmployeeCheckInID"></param>
        public void CancelCheckIn(int intID, int intBedID, int intEmployeeCheckInID)
        {
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                //更新床位状态为空闲
                _mTB_BedDAL.Update(intBedID, _tran, _db, TypeManager.BedStatus.Free);
                //删除入住记录
                _mTB_EmployeeCheckInDAL.Delete(intEmployeeCheckInID, _tran, _db);
                //删除分配记录
                _mTB_AssignRoomDAL.Delete(intID, _tran, _db);
                //提交事务
                _tran.Commit();
            }
            catch (Exception ex)
            {
                //回滚事务
                _tran.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                _connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intDormAreaID"></param>
        /// <returns></returns>
        public IEnumerable<object> GetLockBuildingByDormAreaID(int intDormAreaID)
        {
            Pager pager = null;
            DataTable dt = this.GetPagerData(ref pager, string.Empty, string.Empty);
            DataTable dtSelect = dt.Clone();
            dt.Select("DormAreaID=" + intDormAreaID + "").CopyToDataTable(dtSelect, LoadOption.Upsert);
            IEnumerable<object> dataSourse = (from v in dt.AsEnumerable()       //  dtSelect.AsEnumerable()     //
                                              select new
                                              {
                                                  BuildingID = v.Field<int>("BuildingID"),
                                                  BuildingName = v.Field<string>("BuildingName"),
                                              }).ToList().Distinct();
            return dataSourse;
        }

        public DataTable GetAssignDormArea(string IDCardNo)
        {
           DataTable dt= _mTB_AssignRoomDAL.GetAssignDormInfo(IDCardNo);

            return dt;
        }

        public bool DelAssignDormArea(string IDCardNo)
        {
            int i = _mTB_AssignRoomDAL.DelAssignDormInfo(IDCardNo);

            return i>0?true:false;
        }

        public bool CheckAllowanceApply(string EmployeeNo)
        {
            DataTable dt1 = new TB_AllowanceApplyBLL().GetTableByEmployeeNo(EmployeeNo);
            DataTable dt2 = new TB_AllowanceApplyCancelBLL().GetTableByEmployeeNo(EmployeeNo);
            if(dt1.Rows.Count==0)
            {
                return false;
            }
            else if(dt1.Rows.Count>0&&dt2.Rows.Count==0)
            {
                return true;
            }
            else if(dt1.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                return false;
            }
            return false;
        }

        public bool AssignArea(TB_AssignDormArea tB_AssignDormArea)
        {
          
            try
            {
                //添加宿舍分配记录
               int i= _mTB_AssignRoomDAL.AddAssignDormInfo(tB_AssignDormArea);

                if (i > 0) return true;
                return false;
            }
            catch (Exception ex)
            {
               
               
                throw ex;
            }
            finally
            {
              
               
            }
        }
    }
}
