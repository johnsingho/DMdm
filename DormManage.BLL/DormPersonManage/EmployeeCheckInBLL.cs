using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DormManage.BLL.AssignRoom;
using DormManage.Framework.LogManager;
using System.Web;

namespace DormManage.BLL.DormPersonManage
{
    public class EmployeeCheckInBLL
    {
        private TB_EmployeeCheckInDAL _mTB_EmployeeCheckInDAL = null;
        private TB_EmployeeCheckOutDAL _mTB_EmployeeCheckOutDAL = null;
        private ExcelHelper _mExcelHelper = null;
        private TB_DormAreaDAL _mTB_DormAreaDAL = null;

        private TB_BuildingDAL _mTB_BuildingDAL = null;
        private TB_RoomDAL _mTB_RoomDAL = null;
        private TB_BedDAL _mTB_BedDAL = new TB_BedDAL();
        private Database _db;
        private DbConnection _connection;
        private DbTransaction _tran;
        private TB_ChangeRoomRecordDAL _mTB_ChangeRoomRecordDAL;
        private TB_BUDAL _mTB_BUDAL =new TB_BUDAL();

        public EmployeeCheckInBLL()
        {
            _mTB_EmployeeCheckInDAL = new TB_EmployeeCheckInDAL();
            _mTB_EmployeeCheckOutDAL = new TB_EmployeeCheckOutDAL();
            _mTB_BedDAL = new TB_BedDAL();
            _mExcelHelper = new ExcelHelper();
            _mTB_DormAreaDAL = new TB_DormAreaDAL();
            _mTB_BuildingDAL = new TB_BuildingDAL();
            _mTB_RoomDAL = new TB_RoomDAL();
            _mTB_ChangeRoomRecordDAL = new TB_ChangeRoomRecordDAL();
        }

        public int EditTB_EmployeeCheckIn(TB_EmployeeCheckIn info)
        {
            return _mTB_EmployeeCheckInDAL.EditTB_EmployeeCheckIn(info);
        }

        /// <summary>
        /// 查询入住记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_EmployeeCheckIn tb_EmployeeCheckIn, ref Pager pager)
        {
            return _mTB_EmployeeCheckInDAL.GetTable(tb_EmployeeCheckIn, ref pager);
        }

        public DataTable GetCheckInDateByID(int intID)
        {
           DataTable dtCheckIn = _mTB_EmployeeCheckInDAL.Get(intID);
            return dtCheckIn;
        }

        /// <summary>
        /// 查询入住记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_EmployeeCheckIn tb_EmployeeCheckIn, int iDormAreaID, int iRoomTypeID, string sRoomName, ref Pager pager)
        {
            return _mTB_EmployeeCheckInDAL.GetTable(tb_EmployeeCheckIn, iDormAreaID, iRoomTypeID,sRoomName, ref pager);
        }

        /// <summary>
        /// 员工退房
        /// </summary>
        /// <param name="intID"></param>
        public void CheckOut(int intID, string sTotal)
        {            
            DataTable dtCheckIn = null;//入住信息
            TB_EmployeeCheckOut mTB_EmployeeCheckOut = null;//退房记录
            bool bCanLeave = false;
            bool bSuccess = false;
            decimal? dSum = null;

            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                dtCheckIn = _mTB_EmployeeCheckInDAL.Get(intID);

                //添加扣费记录
                string[] sData = sTotal.Split('@');
                string sReason = sData[0].ToString();
                string sChargeContent = sData[1].ToString();
                string sMoney = sData[2].ToString();
                string sAirConditionFee = sData[3].ToString();
                string sAirConditionFeeMoney = sData[4].ToString();
                string sRoomKeyFee = sData[5].ToString();
                string sRoomKeyFeeMoney = sData[6].ToString();
                string sOtherFee = sData[7].ToString();
                string sOtherFeeMoney = sData[8].ToString();
                string sRemark = sData[9].ToString();
                string sCanLeave = sData[10].ToString();

                //调房--分配到未入住区域
                if (sReason.Contains("调房"))
                {
                    TB_AssignDormArea tB_AssignDormArea = new TB_AssignDormArea();
                    tB_AssignDormArea.DormAreaID = Convert.ToInt32(sReason.Split('#')[1]);
                    tB_AssignDormArea.CardNo = dtCheckIn.Rows[0]["CardNo"].ToString();
                    tB_AssignDormArea.EmployeeNo = dtCheckIn.Rows[0]["EmployeeNo"].ToString();
                    tB_AssignDormArea.CreateUser = SessionHelper.Get(HttpContext.Current, TypeManager.User) == null ? ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account : ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount;
                    tB_AssignDormArea.CreateDate = System.DateTime.Now;

                    new AssignRoomBLL().AssignArea(tB_AssignDormArea);

                    sReason = sReason.Split('#')[0];
                }
                
                //添加退房记录
                mTB_EmployeeCheckOut = new TB_EmployeeCheckOut();
                mTB_EmployeeCheckOut.BedID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BedID]);
                mTB_EmployeeCheckOut.BU = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BU] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BU].ToString();
                mTB_EmployeeCheckOut.BUID = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BUID] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BUID]);
                mTB_EmployeeCheckOut.CardNo = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CardNo] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CardNo].ToString();
                mTB_EmployeeCheckOut.CheckInDate = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CheckInDate] is DBNull ? DateTime.Now : Convert.ToDateTime(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CheckInDate]);
                mTB_EmployeeCheckOut.CheckOutDate = DateTime.Now;
                mTB_EmployeeCheckOut.Company = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Company] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Company].ToString();
                mTB_EmployeeCheckOut.Creator = SessionHelper.Get(HttpContext.Current, TypeManager.User) == null ? ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account : ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount;
                mTB_EmployeeCheckOut.EmployeeNo = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeNo] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeNo].ToString();
                mTB_EmployeeCheckOut.IsSmoking = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_IsSmoking] is DBNull ? false : Convert.ToBoolean(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_IsSmoking]);
                mTB_EmployeeCheckOut.Name = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Name] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Name].ToString();
                mTB_EmployeeCheckOut.Province = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Province] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Province].ToString();
                mTB_EmployeeCheckOut.RoomID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_RoomID]);
                mTB_EmployeeCheckOut.Sex = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Sex] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Sex]);
                mTB_EmployeeCheckOut.SiteID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_SiteID]);
                mTB_EmployeeCheckOut.Telephone = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Telephone] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Telephone].ToString();
                mTB_EmployeeCheckOut.Reason = sReason == "" ? string.Empty : sReason;
                mTB_EmployeeCheckOut.Remark= sRemark == "" ? string.Empty : sRemark;
                bCanLeave = (Convert.ToInt32(sCanLeave) > 0);
                mTB_EmployeeCheckOut.CanLeave = bCanLeave ? 1 : 0;
                mTB_EmployeeCheckOut.EmployeeTypeName = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeTypeName].ToString();

                _mTB_EmployeeCheckOutDAL.Create(mTB_EmployeeCheckOut, _tran, _db);

                //更新床位状态
                _mTB_BedDAL.Update(mTB_EmployeeCheckOut.BedID, _tran, _db, TypeManager.BedStatus.Free);
                //删除入住信息
                _mTB_EmployeeCheckInDAL.Delete(intID, _tran, _db);

                //添加扣费记录
                var dMoney = sMoney.Length > 0 ? Convert.ToDecimal(sMoney) : 0;
                var dAirConditionFeeMoney = sAirConditionFeeMoney.Length > 0 ? Convert.ToDecimal(sAirConditionFeeMoney) : 0;
                var dRoomKeyFeeMoney = sRoomKeyFeeMoney.Length > 0 ? Convert.ToDecimal(sRoomKeyFeeMoney) : 0;
                var dOtherFeeMoney = sOtherFeeMoney.Length > 0 ? Convert.ToDecimal(sOtherFeeMoney) : 0;
                dSum = dMoney + dAirConditionFeeMoney + dRoomKeyFeeMoney + dOtherFeeMoney; //总扣费

                ChargingBLL mChargingBLL = new ChargingBLL();
                TB_Charging mTB_Charging = new TB_Charging();
                mTB_Charging.Name = mTB_EmployeeCheckOut.Name;
                mTB_Charging.EmployeeNo = mTB_EmployeeCheckOut.EmployeeNo;
                mTB_Charging.ChargeContent = sChargeContent;
                mTB_Charging.Money = dMoney;
                mTB_Charging.AirConditionFee = sAirConditionFee;
                mTB_Charging.AirConditionFeeMoney = dAirConditionFeeMoney;
                mTB_Charging.RoomKeyFee = sRoomKeyFee;
                mTB_Charging.RoomKeyFeeMoney = dRoomKeyFeeMoney;
                mTB_Charging.OtherFee = sOtherFee;
                mTB_Charging.OtherFeeMoney = dOtherFeeMoney;
                mTB_Charging.SiteID = mTB_EmployeeCheckOut.SiteID;
                mTB_Charging.Creator = mTB_EmployeeCheckOut.Creator;
                mTB_Charging.BU = mTB_EmployeeCheckOut.BU;
                mChargingBLL.Add(mTB_Charging, _tran);

                //提交事务
                _tran.Commit();
                bSuccess = true;
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

            if (bSuccess && bCanLeave)
            {
                SigningExitForEM(-1, mTB_EmployeeCheckOut.EmployeeNo, dSum);
            }
        }

        /// <summary>
        /// 通过上传excel批量退房
        /// 不支持换房
        /// </summary>
        public bool CheckOutBatch(string filePath, out int nSuccess, out DataTable dtErr)
        {
            nSuccess = 0;
            dtErr = null;
            //读取Excel内容
            DataTable dt = _mExcelHelper.GetDataFromExcel(filePath);
            if(DataTableHelper.IsEmptyDataTable(dt))
            {
                LogManager.GetInstance().ErrorLog("CheckOutBatch,导入的excel表记录为空");
                return false;
            }

            int nTotalCnt = dt.Rows.Count; //全部待导入的个数
            dtErr = dt.Clone();
            var lstDel = new List<DataRow>();
            try
            {
                foreach (DataRow r in dt.Rows)
                {
                    var sWorkID = r["工号"].ToString().Trim();
                    var sIDCardNo = r["身份证号码"].ToString().Trim();
                    var sReason = r["退房原因"].ToString().Trim();

                    //排除明显无效的项
                    var bValid = true;
                    if (string.IsNullOrEmpty(sWorkID) && string.IsNullOrEmpty(sIDCardNo))
                    {
                        bValid = false;
                    }
                    bValid = bValid && IsValidReason(sReason);
                    if (!bValid)
                    {
                        DataTableHelper.CopyDataRow(dtErr, r);
                        lstDel.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("CheckOutBatch,关键列不存在", ex);
                return false;
            }


            foreach (var oDel in lstDel)
            {
                dt.Rows.Remove(oDel);
            }

            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            try
            {
                foreach (DataRow rImp in dt.Rows)
                {
                    if (!CheckOutBatch_part(rImp))
                    {
                        DataTableHelper.CopyDataRow(dtErr, rImp);
                    }
                    else
                    {
                        nSuccess++;
                    }
                }
            }
            finally
            {
                _connection.Close();
            }

            return (nSuccess == nTotalCnt);
        }

        private bool CheckOutBatch_part(DataRow rImp)
        {
            var bSuccess = false;
            var dtCheckIn = _mTB_EmployeeCheckInDAL.GetByWorkID(DataTableHelper.TryGet(rImp, "工号"), 
                                                                DataTableHelper.TryGet(rImp, "身份证号码"));
            if (DataTableHelper.IsEmptyDataTable(dtCheckIn)) { return false; }

            bool bCanLeave = false;
            decimal? dSum = null;
            var sWorkID = string.Empty;

            //启用事务
            using (_tran = _connection.BeginTransaction())
            {
                try
                {
                    var sReason = DataTableHelper.TryGet(rImp, "退房原因");
                    var sRemark = DataTableHelper.TryGet(rImp, "备注");
                    var sCanLeave = DataTableHelper.TryGet(rImp, "同步签退离职系统");

                    //添加退房记录
                    var mTB_EmployeeCheckOut = new TB_EmployeeCheckOut();
                    mTB_EmployeeCheckOut.BedID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BedID]);
                    mTB_EmployeeCheckOut.BU = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BU] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BU].ToString();
                    mTB_EmployeeCheckOut.BUID = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BUID] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BUID]);
                    mTB_EmployeeCheckOut.CardNo = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CardNo] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CardNo].ToString();
                    mTB_EmployeeCheckOut.CheckInDate = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CheckInDate] is DBNull ? DateTime.Now : Convert.ToDateTime(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CheckInDate]);
                    mTB_EmployeeCheckOut.CheckOutDate = DateTime.Now;
                    mTB_EmployeeCheckOut.Company = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Company] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Company].ToString();
                    mTB_EmployeeCheckOut.Creator = SessionHelper.Get(HttpContext.Current, TypeManager.User) == null ? ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account : ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount;
                    sWorkID = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeNo] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeNo].ToString();
                    mTB_EmployeeCheckOut.EmployeeNo = sWorkID;
                    mTB_EmployeeCheckOut.IsSmoking = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_IsSmoking] is DBNull ? false : Convert.ToBoolean(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_IsSmoking]);
                    mTB_EmployeeCheckOut.Name = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Name] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Name].ToString();
                    mTB_EmployeeCheckOut.Province = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Province] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Province].ToString();
                    mTB_EmployeeCheckOut.RoomID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_RoomID]);
                    mTB_EmployeeCheckOut.Sex = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Sex] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Sex]);
                    mTB_EmployeeCheckOut.SiteID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_SiteID]);
                    mTB_EmployeeCheckOut.Telephone = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Telephone] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Telephone].ToString();
                    mTB_EmployeeCheckOut.Reason = sReason == "" ? string.Empty : sReason;
                    mTB_EmployeeCheckOut.Remark = sRemark == "" ? string.Empty : sRemark;
                    bCanLeave = (0 == string.Compare(sCanLeave, "是", true));
                    mTB_EmployeeCheckOut.CanLeave = bCanLeave ? 1 : 0;
                    mTB_EmployeeCheckOut.EmployeeTypeName = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeTypeName].ToString();

                    //调房--分配到未入住区域
                    //if (sReason.Contains("调房"))
                    //{
                    //    TB_AssignDormArea tB_AssignDormArea = new TB_AssignDormArea();
                    //    tB_AssignDormArea.DormAreaID = Convert.ToInt32(sReason.Split('#')[1]);
                    //    tB_AssignDormArea.CardNo = dtCheckIn.Rows[0]["CardNo"].ToString();
                    //    tB_AssignDormArea.EmployeeNo = dtCheckIn.Rows[0]["EmployeeNo"].ToString();
                    //    tB_AssignDormArea.CreateUser = SessionHelper.Get(HttpContext.Current, TypeManager.User) == null ? ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account : ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount;
                    //    tB_AssignDormArea.CreateDate = System.DateTime.Now;
                    //    new AssignRoomBLL().AssignArea(tB_AssignDormArea);
                    //    sReason = sReason.Split('#')[0];
                    //}

                    _mTB_EmployeeCheckOutDAL.Create(mTB_EmployeeCheckOut, _tran, _db);
       
                    //更新床位状态
                    _mTB_BedDAL.Update(mTB_EmployeeCheckOut.BedID, _tran, _db, TypeManager.BedStatus.Free);
                    //删除入住信息
                    int intID = (int)dtCheckIn.Rows[0]["ID"];
                    _mTB_EmployeeCheckInDAL.Delete(intID, _tran, _db);

                    //添加扣费记录
                    ChargingBLL mChargingBLL = new ChargingBLL();
                    TB_Charging mTB_Charging = new TB_Charging();
                    mTB_Charging.Name = mTB_EmployeeCheckOut.Name;
                    mTB_Charging.EmployeeNo = mTB_EmployeeCheckOut.EmployeeNo;
                    mTB_Charging.ChargeContent = "管理费";
                    decimal num = 0.0M;
                    decimal.TryParse(DataTableHelper.TryGet(rImp,"管理费"), out num);
                    mTB_Charging.Money = num;

                    mTB_Charging.AirConditionFee = "空调费";
                    num = 0.0M;
                    decimal.TryParse(DataTableHelper.TryGet(rImp, "空调费"), out num);
                    mTB_Charging.AirConditionFeeMoney = num;

                    mTB_Charging.RoomKeyFee = "钥匙费";
                    num = 0.0M;
                    decimal.TryParse(DataTableHelper.TryGet(rImp, "钥匙费"), out num);
                    mTB_Charging.RoomKeyFeeMoney = num;

                    mTB_Charging.OtherFee = "其他费";
                    num = 0.0M;
                    decimal.TryParse(DataTableHelper.TryGet(rImp, "其他费"), out num);
                    mTB_Charging.OtherFeeMoney = num;
                    mTB_Charging.SiteID = mTB_EmployeeCheckOut.SiteID;
                    mTB_Charging.Creator = mTB_EmployeeCheckOut.Creator;
                    mTB_Charging.BU = mTB_EmployeeCheckOut.BU;
                    mChargingBLL.Add(mTB_Charging, _tran);

                    dSum = mTB_Charging.Money + mTB_Charging.AirConditionFeeMoney 
                         + mTB_Charging.RoomKeyFeeMoney + mTB_Charging.OtherFeeMoney; //总扣费

                    //提交事务
                    _tran.Commit();
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    //回滚事务
                    _tran.Rollback();
                    //throw ex;
                    LogManager.GetInstance().ErrorLog("批量退房失败CheckOutBatch_part", ex);
                }
            }
            
            if (bSuccess && bCanLeave)
            {
                SigningExitForEM(-1, sWorkID, dSum);
            }
            return bSuccess;
        }

        //TODO 现在不支持批量调房
        private bool IsValidReason(string sReason)
        {
            //调房需要传入宿舍区号，时间关系，不处理
            var lst = new List<string>() { "辞职", "自离", "外住", "解雇", "未入职" /*,"调房"*/};
            return lst.IndexOf(sReason) >= 0;            
        }

        //更改退宿原因
        public bool ChangeCheckOutReason(int id, string sTotal)
        {
            string[] sData = sTotal.Split('@');
            var sReason = sData[0];
            var bCanLeave = Convert.ToInt32(sData[1]) > 0;

            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            var bSuccess = false;
            try
            {
                _mTB_EmployeeCheckOutDAL.ChangeCheckOutReason(id, sReason, bCanLeave, _tran, _db);
                //提交事务
                _tran.Commit();
                bSuccess = true;
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

            if(bSuccess && bCanLeave)
            {
                SigningExitForEM(id);
            }

            return bSuccess;
        }

        //访问离职系统，进行签退
        private int SigningExitForEM(int id, string workID="", decimal? cost=null)
        {
            var sWorkID = workID;
            DbConnection dbConn = null;
            if (string.IsNullOrEmpty(sWorkID))
            {
                try
                {// get checkout Employee No
                    var dbDorm = DBO.CreateDatabase();
                    dbConn = dbDorm.CreateConnection();
                    dbConn.Open();

                    string strSQL = @"select RoomID,BedID,EmployeeNo,Name,CardNo from TB_EmployeeCheckOut
                                      where id=@ID";
                    var dbCommandWrapper = dbDorm.DbProviderFactory.CreateCommand();
                    dbCommandWrapper.CommandType = CommandType.Text;
                    dbCommandWrapper.CommandText = strSQL;
                    dbDorm.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, id);
                    var ds = dbDorm.ExecuteDataSet(dbCommandWrapper);
                    if (DataTableHelper.IsEmptyDataSet(ds))
                    {
                        return -1;
                    }
                    var dr = DataTableHelper.GetDataSet_Row0(ds);
                    sWorkID = dr["EmployeeNo"] as string;
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    dbConn.Close();
                }
            }

            if (string.IsNullOrEmpty(sWorkID))
            {
                return -1;
            }

            //update
            DbTransaction dbTran = null;
            try
            {
                string sAppGroupID = "097F36B9-B8A2-478B-97DA-79E76E384571";
                var dbEM = DBO.CreateDatabaseEM();
                dbConn = dbEM.CreateConnection();
                dbConn.Open();
                dbTran = dbConn.BeginTransaction();

                var strSQL = string.Empty;
                int nRet = 0;
                var sCreateUser = SessionHelper.Get(HttpContext.Current, TypeManager.User) == null ? ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account : ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).EName;

                //create EM_Approved
                if (null!=cost && cost.HasValue)
                {
                    strSQL = @"insert into EM_Approved([Approved_ID],[FormID],[ApprovalGroupID],[EmpID],[Cost],[Balance],
                                [DeleteMark],[Remark],[CreateDate],[CreateUserId],[CreateUserName])
                            select [Approving_ID],[FormID],[ApprovalGroupID],[EmpID],@Cost,[Balance],
                                [DeleteMark],'宿舍系统自动签退',GetDate(),NULL,@CreateUserName
                            from EM_Approving
                            where ApprovalGroupID=@AppGroupID
                            and EmpID=@EmpID
                            ";
                }
                else
                {
                    strSQL = @"insert into EM_Approved([Approved_ID],[FormID],[ApprovalGroupID],[EmpID],[Cost],[Balance],
                                [DeleteMark],[Remark],[CreateDate],[CreateUserId],[CreateUserName])
                            select [Approving_ID],[FormID],[ApprovalGroupID],[EmpID],[Cost],[Balance],
                                [DeleteMark],'宿舍系统自动签退',GetDate(),NULL,@CreateUserName
                            from EM_Approving
                            where ApprovalGroupID=@AppGroupID
                            and EmpID=@EmpID
                            ";
                }

                var dbCommandWrapper = dbEM.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                dbCommandWrapper.CommandText = strSQL;
                dbEM.AddInParameter(dbCommandWrapper, "@CreateUserName", DbType.String, sCreateUser);
                dbEM.AddInParameter(dbCommandWrapper, "@AppGroupID", DbType.String, sAppGroupID);
                dbEM.AddInParameter(dbCommandWrapper, "@EmpID", DbType.String, sWorkID);
                if (null!=cost && cost.HasValue)
                {
                    dbEM.AddInParameter(dbCommandWrapper, "@Cost", DbType.Decimal, cost.Value);
                }
                nRet = dbEM.ExecuteNonQuery(dbCommandWrapper, dbTran);

                //delete EM_Approving
                strSQL = @"delete from EM_Approving
                            where 1 = 1
                            and ApprovalGroupID = @AppGroupID
                            and EmpID = @EmpID";
                dbCommandWrapper = dbEM.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                dbCommandWrapper.CommandText = strSQL;
                dbEM.AddInParameter(dbCommandWrapper, "@AppGroupID", DbType.String, sAppGroupID);
                dbEM.AddInParameter(dbCommandWrapper, "@EmpID", DbType.String, sWorkID);
                nRet = dbEM.ExecuteNonQuery(dbCommandWrapper, dbTran);

                dbTran.Commit();
            }
            catch(Exception ex)
            {
                dbTran.Rollback();
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }

            return 0;
        }

        /// <summary>
        /// 调房---退房记录
        /// </summary>
        /// <param name="intID"></param>
        public void AddCheckOut(int intID)
        {
            DataTable dtCheckIn = null;//入住信息
            TB_EmployeeCheckOut mTB_EmployeeCheckOut = null;//退房记录
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
           
            try
            {
                dtCheckIn = _mTB_EmployeeCheckInDAL.Get(intID);


                //添加退房记录
                mTB_EmployeeCheckOut = new TB_EmployeeCheckOut();
                mTB_EmployeeCheckOut.BedID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BedID]);
                mTB_EmployeeCheckOut.BU = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BU] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BU].ToString();
                mTB_EmployeeCheckOut.BUID = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BUID] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_BUID]);
                mTB_EmployeeCheckOut.CardNo = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CardNo] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CardNo].ToString();
                mTB_EmployeeCheckOut.CheckInDate = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CheckInDate] is DBNull ? DateTime.Now : Convert.ToDateTime(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_CheckInDate]);
                mTB_EmployeeCheckOut.CheckOutDate = DateTime.Now;
                mTB_EmployeeCheckOut.Company = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Company] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Company].ToString();
                mTB_EmployeeCheckOut.Creator = SessionHelper.Get(HttpContext.Current, TypeManager.User) == null ? ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account : ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount;
                mTB_EmployeeCheckOut.EmployeeNo = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeNo] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeNo].ToString();
                mTB_EmployeeCheckOut.IsSmoking = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_IsSmoking] is DBNull ? false : Convert.ToBoolean(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_IsSmoking]);
                mTB_EmployeeCheckOut.Name = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Name] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Name].ToString();
                mTB_EmployeeCheckOut.Province = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Province] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Province].ToString();
                mTB_EmployeeCheckOut.RoomID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_RoomID]);
                mTB_EmployeeCheckOut.Sex = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Sex] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Sex]);
                mTB_EmployeeCheckOut.SiteID = Convert.ToInt32(dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_SiteID]);
                mTB_EmployeeCheckOut.Telephone = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Telephone] is DBNull ? string.Empty : dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_Telephone].ToString();
                mTB_EmployeeCheckOut.Reason = "换房";
                mTB_EmployeeCheckOut.Remark = "换房产生";
                mTB_EmployeeCheckOut.EmployeeTypeName = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeTypeName].ToString();

                _mTB_EmployeeCheckOutDAL.Create(mTB_EmployeeCheckOut);

                //提交事务
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
        /// 导入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>导入失败记录</returns>
        public DataTable Import(string filePath)
        {
            //读取Excel内容
            DataTable dt = _mExcelHelper.GetDataFromExcel(filePath);
            dt.Columns.Add("BZ");
            DataTable dtBU = new DataTable();
            //SiteID
            int intSiteID = SessionHelper.Get(HttpContext.Current, TypeManager.User) != null ?
                ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).SiteID :
                ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).SiteID;
            //操作用户账号
            string currentUser = SessionHelper.Get(HttpContext.Current, TypeManager.User) != null ?
                ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount :
                ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account;
            DataTable dtTB_EmployeeCheckInInsert = new DataTable();//入住人员信息
            dtTB_EmployeeCheckInInsert.Columns.Add("RoomID");
            dtTB_EmployeeCheckInInsert.Columns.Add("BedID");
            dtTB_EmployeeCheckInInsert.Columns.Add("EmployeeNo");
            dtTB_EmployeeCheckInInsert.Columns.Add("Sex");
            dtTB_EmployeeCheckInInsert.Columns.Add("BUID");
            dtTB_EmployeeCheckInInsert.Columns.Add("BU");
            dtTB_EmployeeCheckInInsert.Columns.Add("Company");
            dtTB_EmployeeCheckInInsert.Columns.Add("CardNo");
            dtTB_EmployeeCheckInInsert.Columns.Add("CheckInDate");
            dtTB_EmployeeCheckInInsert.Columns.Add("Creator");
            dtTB_EmployeeCheckInInsert.Columns.Add("SiteID");
            dtTB_EmployeeCheckInInsert.Columns.Add("Name");
            dtTB_EmployeeCheckInInsert.Columns.Add("IsActive");
            dtTB_EmployeeCheckInInsert.Columns.Add("Telephone");
            dtTB_EmployeeCheckInInsert.Columns.Add("EmployeeTypeName");
            //获取到整个site的所有入住人员信息
            DataTable dtEmployeeCheckIn = _mTB_EmployeeCheckInDAL.GetTableBySiteID(intSiteID);
            //获取到整个site的所有未入住床位信息
            DataTable dtTB_Bed = _mTB_BedDAL.GetTableBySiteCheckIn(intSiteID);
            DataTable dtError = dt.Clone();
            
            StringBuilder sbBedID = new StringBuilder();//用于保存床位ID
            #region 
            int buID = 0;

            #endregion

            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    dtBU = _mTB_BUDAL.Get(intSiteID, dr[TypeManager.Col_BU].ToString());//.Rows.Count
                    buID = dtBU.Rows.Count >0 ? Convert.ToInt32(dtBU.Rows[0][0]) : 0 ; 
                    DataRow[] drEmployeeCheckInArr = dtEmployeeCheckIn.Select("CardNo='" + dr["身份证号码"] + "'");
                    DataRow[] drEmployeeCheckEmployeeNo = dtEmployeeCheckIn.Select("EmployeeNo='" + dr["工号"] + "'");
                    if (drEmployeeCheckInArr.Length > 0)
                    {
                        dr["BZ"] = "已有入住记录";
                        dtError.ImportRow(dr);
                        //更新状态
                        _mTB_EmployeeCheckInDAL.Update(Convert.ToInt32(drEmployeeCheckInArr[0][TB_EmployeeCheckIn.col_ID])
                            , TypeManager.IsActive.Valid, null, DBO.GetInstance());
                    }
                    else if (drEmployeeCheckEmployeeNo.Length > 0)
                    {
                        dr["BZ"] = "已有入住记录";
                        dtError.ImportRow(dr);
                        //更新状态
                        _mTB_EmployeeCheckInDAL.Update(Convert.ToInt32(drEmployeeCheckInArr[0][TB_EmployeeCheckIn.col_ID])
                            , TypeManager.IsActive.Valid, null, DBO.GetInstance());
                    }
                    else
                    {
                        DataRow[] drTB_BedArr = dtTB_Bed.Select("DormAreaName='" + dr["宿舍区"] + "' and BuildingName='" + dr["楼栋"] + "' and RoomName='" + dr["房间号"] + "' and  Name='" + dr["床位号"] + "'");
                        if (drTB_BedArr.Length > 0)
                        {
                            if(drTB_BedArr[0]["IsEnable"]!=null)
                            {
                                if (drTB_BedArr[0]["IsEnable"].ToString() == "已禁用")
                                {
                                    dr["BZ"] = "床位信息错误：已禁用";
                                    dtError.ImportRow(dr);
                                    continue;
                                }
                            }
                           
                            DataRow drTB_EmployeeCheckInInsert = dtTB_EmployeeCheckInInsert.NewRow();
                            drTB_EmployeeCheckInInsert["BedID"] = Convert.ToInt32(drTB_BedArr[0]["ID"]);
                            drTB_EmployeeCheckInInsert["RoomID"] = Convert.ToInt32(drTB_BedArr[0]["RoomID"]);
                            drTB_EmployeeCheckInInsert["SiteID"] = intSiteID;
                            drTB_EmployeeCheckInInsert["CardNo"] = dr[TypeManager.Col_CardNo].ToString();
                            drTB_EmployeeCheckInInsert["BUID"] = buID;
                            drTB_EmployeeCheckInInsert["BU"] = dr[TypeManager.Col_BU].ToString();
                            drTB_EmployeeCheckInInsert["CheckInDate"] = Convert.ToDateTime(dr[TypeManager.Col_CheckInDate]);
                            drTB_EmployeeCheckInInsert["Company"] = dr[TypeManager.Col_Company].ToString();
                            drTB_EmployeeCheckInInsert["Creator"] = currentUser;
                            drTB_EmployeeCheckInInsert["IsActive"] = (int)TypeManager.IsActive.Valid;
                            drTB_EmployeeCheckInInsert["EmployeeNo"] = dr[TypeManager.Col_EmployeeNo].ToString();
                            drTB_EmployeeCheckInInsert["Name"] = dr[TypeManager.Col_Name].ToString();
                            drTB_EmployeeCheckInInsert["Sex"] = RemarkAttribute.GetEnumRemark(TypeManager.Sex.Male) == dr[TypeManager.Col_Sex].ToString() ? (int)TypeManager.Sex.Male : (int)TypeManager.Sex.Female;
                            drTB_EmployeeCheckInInsert["Telephone"] = dr["手机号码"];
                            drTB_EmployeeCheckInInsert["EmployeeTypeName"] = dr["用工类型"];
                            dtTB_EmployeeCheckInInsert.Rows.Add(drTB_EmployeeCheckInInsert);

                           
                            sbBedID.Append(drTB_BedArr[0]["ID"] + ",");
                        }
                        else
                        {
                            dr["BZ"] = "床位信息错误：已有入住信息或不存在";
                            dtError.ImportRow(dr);
                        }
                    }
                }
                catch(Exception ex)
                {
                    dr["BZ"] = ex.Message;
                    dtError.ImportRow(dr);
                }
            }
            if (dtTB_EmployeeCheckInInsert.Rows.Count > 0)
            {
                //添加入住记录
                new CommonManager().DTToDB(dtTB_EmployeeCheckInInsert, "TB_EmployeeCheckIn"
                    , new string[] { "RoomID", "BedID", "EmployeeNo", "Sex","BUID", "BU", "Company", "CardNo", "CheckInDate", "Creator", "SiteID", "Name", "IsActive" , "Telephone", "EmployeeTypeName" }
                    , DBO.GetInstance().CreateConnection().ConnectionString);
                //更新床位状态
                _mTB_BedDAL.Update(sbBedID.ToString().TrimEnd(','), TypeManager.BedStatus.Busy);

            }
            return dtError;
        }

        /// <summary>
        /// 员工换房
        /// </summary>
        /// <param name="intID">需要换房的员工ID</param>
        /// <param name="intBedID">新换的床位ID</param>
        public void ChangeRoom(int intID, int intBedID)
        {
            TB_EmployeeCheckIn mTB_EmployeeCheckIn = null;
            TB_ChangeRoomRecord mTB_ChangeRoomRecord = null;
            string operatorUser = SessionHelper.Get(HttpContext.Current, TypeManager.User) != null 
                                    ? ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount 
                                    : ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account;
            int intOldBedID = 0;
            TB_Bed mTB_Bed = null;
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();

          
            try
            {
                mTB_EmployeeCheckIn = this.ConvertTableToList(_mTB_EmployeeCheckInDAL.Get(intID)).FirstOrDefault();
                mTB_Bed = _mTB_BedDAL.Get(intBedID);
                intOldBedID = mTB_EmployeeCheckIn.BedID;
                TB_Bed mOldTB_Bed= _mTB_BedDAL.Get(intOldBedID);

                //更新床位状态为入住
                _mTB_BedDAL.Update(intBedID, _tran, _db, TypeManager.BedStatus.Busy);
                //更新床位状态为空闲
                _mTB_BedDAL.Update(intOldBedID, _tran, _db, TypeManager.BedStatus.Free);

                //添加换房记录
                mTB_ChangeRoomRecord = new TB_ChangeRoomRecord();
                mTB_ChangeRoomRecord.BU = mTB_EmployeeCheckIn.BU;
                mTB_ChangeRoomRecord.BUID = mTB_EmployeeCheckIn.BUID;       //new add buid 2015-02-07
                mTB_ChangeRoomRecord.CardNo = mTB_EmployeeCheckIn.CardNo;
                mTB_ChangeRoomRecord.ChangeRoomDate = DateTime.Now;
                mTB_ChangeRoomRecord.CheckInDate = mTB_EmployeeCheckIn.CheckInDate;
                mTB_ChangeRoomRecord.Company = mTB_EmployeeCheckIn.Company;
                mTB_ChangeRoomRecord.Creator = operatorUser;
                mTB_ChangeRoomRecord.EmployeeNo = mTB_EmployeeCheckIn.EmployeeNo;
                mTB_ChangeRoomRecord.IsSmoking = mTB_EmployeeCheckIn.IsSmoking;
                mTB_ChangeRoomRecord.Name = mTB_EmployeeCheckIn.Name;
                mTB_ChangeRoomRecord.NewBedID = intBedID;
                mTB_ChangeRoomRecord.OldBedID = mTB_EmployeeCheckIn.BedID;
                mTB_ChangeRoomRecord.Province = mTB_EmployeeCheckIn.Province;
                mTB_ChangeRoomRecord.Sex = mTB_EmployeeCheckIn.Sex;
                mTB_ChangeRoomRecord.SiteID = mTB_EmployeeCheckIn.SiteID;
                mTB_ChangeRoomRecord.Telephone = mTB_EmployeeCheckIn.Telephone;
                mTB_ChangeRoomRecord.EmployeeTypeName = mTB_EmployeeCheckIn.EmployeeTypeName;
                _mTB_ChangeRoomRecordDAL.Create(mTB_ChangeRoomRecord, _tran, _db);

                if (mTB_Bed.BuildingID != mOldTB_Bed.BuildingID)//不是一栋的增加退宿记录
                {
                    //增加退宿记录
                    AddCheckOut(intID);
                }

                //更新入住人员信息
                mTB_EmployeeCheckIn.BedID = intBedID;
                mTB_EmployeeCheckIn.RoomID = mTB_Bed.RoomID;
                mTB_EmployeeCheckIn.UpdateBy = operatorUser;
                mTB_EmployeeCheckIn.CheckInDate = DateTime.Now;
                _mTB_EmployeeCheckInDAL.Edit(mTB_EmployeeCheckIn, _tran, _db);
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
        /// 导出
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <returns></returns>
        public string Export(TB_EmployeeCheckIn tb_EmployeeCheckIn, int iDormAreaID, int iRoomTypeID, string sRoomName)
        {
            DataTable dt = _mTB_EmployeeCheckInDAL.GetTable(tb_EmployeeCheckIn, iDormAreaID, iRoomTypeID, sRoomName);
            dt.Columns.Remove("楼层");
            dt.Columns.Remove("单元");
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "Report");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, DateTime.Now.ToString("yyMMddHHmmssms_") + "入住记录.xls");
            _mExcelHelper.RenderToExcel(dt, strFileName);
            return strFileName;
        }

        public string Export(DataTable dt, string name)
        {
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "Report");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            var sfn = string.Format("{0:yyMMddHHmmssms}_{1}.xls", DateTime.Now, name);
            string strFileName = Path.Combine(strFilePath, sfn);
            _mExcelHelper.RenderToExcel(dt, strFileName);
            return strFileName;
        }

        /// <summary>
        /// 将datatable转换成list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<TB_EmployeeCheckIn> ConvertTableToList(DataTable dtCheckIn)
        {
            TB_EmployeeCheckIn mTB_EmployeeCheckIn = null;
            List<TB_EmployeeCheckIn> lstTB_EmployeeCheckIn = new List<TB_EmployeeCheckIn>();
            for (int i = 0; i < dtCheckIn.Rows.Count; i++)
            {
                mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
                mTB_EmployeeCheckIn.ID = Convert.ToInt32(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_ID]);
                mTB_EmployeeCheckIn.RoomID = Convert.ToInt32(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_RoomID]);
                mTB_EmployeeCheckIn.BedID = Convert.ToInt32(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_BedID]);
                mTB_EmployeeCheckIn.BU = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_BU] is DBNull ? string.Empty : dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_BU].ToString();
                mTB_EmployeeCheckIn.BUID = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_BUID] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_BUID]);
                mTB_EmployeeCheckIn.CardNo = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_CardNo] is DBNull ? string.Empty : dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_CardNo].ToString();
                mTB_EmployeeCheckIn.CheckInDate = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_CheckInDate] is DBNull ? DateTime.Now : Convert.ToDateTime(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_CheckInDate]);
                mTB_EmployeeCheckIn.Company = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Company] is DBNull ? string.Empty : dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Company].ToString();
                mTB_EmployeeCheckIn.Creator = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Creator].ToString();
                mTB_EmployeeCheckIn.EmployeeNo = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_EmployeeNo] is DBNull ? string.Empty : dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_EmployeeNo].ToString();
                mTB_EmployeeCheckIn.IsSmoking = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_IsSmoking] is DBNull ? false : Convert.ToBoolean(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_IsSmoking]);
                mTB_EmployeeCheckIn.Name = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Name] is DBNull ? string.Empty : dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Name].ToString();
                mTB_EmployeeCheckIn.Province = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Province] is DBNull ? string.Empty : dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Province].ToString();
                mTB_EmployeeCheckIn.CreateDate = Convert.ToDateTime(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_CreateDate]);
                mTB_EmployeeCheckIn.Sex = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Sex] is DBNull ? 0 : Convert.ToInt32(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Sex]);
                mTB_EmployeeCheckIn.SiteID = Convert.ToInt32(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_SiteID]);
                mTB_EmployeeCheckIn.Telephone = dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Telephone] is DBNull ? string.Empty : dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_Telephone].ToString();
                mTB_EmployeeCheckIn.IsActive = Convert.ToInt32(dtCheckIn.Rows[i][TB_EmployeeCheckIn.col_IsActive]);
                mTB_EmployeeCheckIn.EmployeeTypeName = dtCheckIn.Rows[0][TB_EmployeeCheckIn.col_EmployeeTypeName].ToString();

                lstTB_EmployeeCheckIn.Add(mTB_EmployeeCheckIn);
            }
            return lstTB_EmployeeCheckIn;
        }
    }
}
