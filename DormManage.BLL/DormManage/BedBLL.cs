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

namespace DormManage.BLL.DormManage
{
    public class BedBLL
    {
        private TB_DormAreaDAL _mTB_DormAreaDAL = null;
        private TB_BuildingDAL _mTB_BuildingDAL = null;
        private TB_RoomDAL _mTB_RoomDAL = null;
        private TB_RoomTypeDAL _mTB_RoomTypeDAL = null;
        private TB_BedDAL _mTB_BedDAL = null;

        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public BedBLL()
        {
            _mTB_DormAreaDAL = new TB_DormAreaDAL();
            _mTB_BuildingDAL = new TB_BuildingDAL();
            _mTB_RoomDAL = new TB_RoomDAL();
            _mTB_BedDAL = new TB_BedDAL();
            _mTB_RoomTypeDAL = new TB_RoomTypeDAL();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Bed"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_Bed tb_Bed, ref Pager pager)
        {
            return _mTB_BedDAL.GetTable(tb_Bed, ref pager);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Bed"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTableByEnableStatus(TB_Bed tb_Bed, ref Pager pager)
        {
            return _mTB_BedDAL.GetTableByEnableStatus(tb_Bed, ref pager);
        }
        
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="tb_Bed"></param>
        /// <returns></returns>
        public int Edit(TB_Bed tb_Bed)
        {
            DataTable dt = null;
            //查询宿舍区域名称是否存在
            dt = _mTB_BedDAL.GetTable(tb_Bed);
            if (dt != null && dt.Rows.Count > 0) _errMessage = "名称重复！";
            else
            {
                //更新操作
                if (tb_Bed.ID > 0) _mTB_BedDAL.Edit(tb_Bed);
                //添加操作
                else tb_Bed.ID = _mTB_BedDAL.Create(tb_Bed);
            }
            return tb_Bed.ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intBedID"></param>
        /// <returns></returns>
        public TB_Bed Get(int intBedID)
        {
            return _mTB_BedDAL.Get(intBedID);
        }

        /// <summary>
        /// 根据房间ID获取到床位号
        /// </summary>
        /// <param name="intRoomID"></param>
        /// <returns></returns>
        public DataTable GetBedByRoomID(int intRoomID)
        {
            Pager mPager = null;
            return _mTB_BedDAL.GetTable(new TB_Bed() { RoomID = intRoomID }, ref mPager);
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="dt"></param>
        public int Import(DataTable dt,ref string errorMsg)
        {
            int site = System.Web.HttpContext.Current.Session[TypeManager.User] != null ?
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).SiteID :
                ((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).SiteID;
            string user = System.Web.HttpContext.Current.Session[TypeManager.User] != null ?
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ADAccount :
                ((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).Account;
            CommonManager mCommonManager = new CommonManager();

            try
            {
            #region 宿舍区
           
                DataTable dtTB_DormArea = _mTB_DormAreaDAL.GetTableBySite(site);
                DataTable dtTB_DormAreaInsert = dtTB_DormArea.Clone();
                DataRow[] drTB_DormAreaArr = null;
       
            var queryDormArea = from v in dt.AsEnumerable()
                                select new
                                {
                                    DormAreaName = v.Field<string>("宿舍区")
                                };
            foreach (var item in queryDormArea.ToList().Distinct())
            {
                drTB_DormAreaArr = dtTB_DormArea.Select("name='" + item.DormAreaName + "'");
                if (drTB_DormAreaArr.Length <= 0)
                {
                    DataRow drDormAreaInsert = dtTB_DormAreaInsert.NewRow();
                    drDormAreaInsert["Name"] = item.DormAreaName;
                    drDormAreaInsert["SiteID"] = site;
                    drDormAreaInsert["Creator"] = user;
                    dtTB_DormAreaInsert.Rows.Add(drDormAreaInsert);
                }
            }
            dtTB_DormAreaInsert.Columns.Remove("ID");
            if (dtTB_DormAreaInsert.Rows.Count > 0)
                mCommonManager.DTToDB(dtTB_DormAreaInsert, "TB_DormArea", new string[] { "SiteID", "Name", "Creator" }, DBO.GetInstance().CreateConnection().ConnectionString);

            #endregion
            #region 楼栋
            dtTB_DormArea = _mTB_DormAreaDAL.GetTableBySite(site);
            DataTable dtTB_Building = _mTB_BuildingDAL.GetTableBySiteID(site);
            DataTable dtTB_BuildingInsert = dtTB_Building.Clone();
            DataRow[] drTB_BuildingArr = null;
            var queryBuilding = from v in dt.AsEnumerable()
                                select new
                                {
                                    DormAreaName = v.Field<string>("宿舍区"),
                                    BuildingName = v.Field<string>("楼栋"),
                                };
            foreach (var item in queryBuilding.ToList().Distinct())
            {
                drTB_BuildingArr = dtTB_Building.Select("Name='" + item.BuildingName + "' and DormAreaName='" + item.DormAreaName + "'");
                if (drTB_BuildingArr.Length <= 0)
                {
                    drTB_DormAreaArr = dtTB_DormArea.Select("Name='" + item.DormAreaName + "'");
                    DataRow drTB_BuildingInsert = dtTB_BuildingInsert.NewRow();
                    drTB_BuildingInsert["DormAreaID"] = drTB_DormAreaArr[0]["ID"];
                    drTB_BuildingInsert["Name"] = item.BuildingName;
                    drTB_BuildingInsert["SiteID"] = site;
                    drTB_BuildingInsert["Creator"] = user;
                    dtTB_BuildingInsert.Rows.Add(drTB_BuildingInsert);
                }
            }
            dtTB_BuildingInsert.Columns.Remove("ID");
            dtTB_BuildingInsert.Columns.Remove("DormAreaName");
            if (dtTB_BuildingInsert.Rows.Count > 0)
                mCommonManager.DTToDB(dtTB_BuildingInsert, "TB_Building", new string[] { "DormAreaID", "Name", "Creator", "SiteID" }, DBO.GetInstance().CreateConnection().ConnectionString);
            #endregion
            #region 房间
            dtTB_DormArea = _mTB_DormAreaDAL.GetTableBySite(site);
            dtTB_Building = _mTB_BuildingDAL.GetTableBySiteID(site);
            DataTable dtTB_RoomType = _mTB_RoomTypeDAL.GetTable(site);
            DataTable dtTB_Room = _mTB_RoomDAL.GetTableBySiteID(site);
            DataTable dtTB_RoomInsert = dtTB_Room.Clone();
            DataRow[] drTB_RoomArr = null;
            DataTable dtRoomType = new TB_RoomTypeDAL().GetTable(site);
            var queryRoom = from v in dt.AsEnumerable()
                            select new
                            {
                                DormAreaName = v.Field<string>("宿舍区"),
                                BuildingName = v.Field<string>("楼栋"),
                                RoomName = v.Field<string>("房间号"),
                                RoomType = v.Field<string>("房间类型"),
                                RoomSexType = v.Field<string>("性别"),
                            };
            foreach (var item in queryRoom.ToList().Distinct())
            {
                DataRow[] dr=dtTB_RoomType.Select("Name='"+ item.RoomType+ "'");
                if(dr.Length==0)
                {
                   errorMsg = "房间类型:" + item.RoomType + " 不存在，请检查后再导入";
                   return 0;
                }
                drTB_RoomArr = dtTB_Room.Select("BuildingName='" + item.BuildingName + "' and DormAreaName='" + item.DormAreaName + "' and Name='" + item.RoomName + "'");
                if (drTB_RoomArr.Length <= 0)
                {
                    drTB_DormAreaArr = dtTB_DormArea.Select("Name='" + item.DormAreaName + "'");
                    drTB_BuildingArr = dtTB_Building.Select("Name='" + item.BuildingName + "' and DormAreaName='" + item.DormAreaName + "'");
                    DataRow drTB_RoomInsert = dtTB_RoomInsert.NewRow();
                    drTB_RoomInsert["DormAreaID"] = drTB_DormAreaArr[0]["ID"];
                    drTB_RoomInsert["BuildingID"] = drTB_BuildingArr[0]["ID"];
                    drTB_RoomInsert["Name"] = item.RoomName;
                    drTB_RoomInsert["SiteID"] = site;
                    drTB_RoomInsert["Creator"] = user;
                    drTB_RoomInsert["RoomSexType"] = item.RoomSexType;
                    drTB_RoomInsert["RoomType2"] = 0;
                    drTB_RoomInsert["KeyCount"] = "";
                    DataRow[] drRoomTypeArr = dtRoomType.Select("Name='" + item.RoomType + "'");
                    if (drRoomTypeArr.Length > 0)
                        drTB_RoomInsert["RoomType"] = drRoomTypeArr[0]["ID"];
                    dtTB_RoomInsert.Rows.Add(drTB_RoomInsert);
                }
            }
            dtTB_RoomInsert.Columns.Remove("ID");
            dtTB_RoomInsert.Columns.Remove("DormAreaName");
            dtTB_RoomInsert.Columns.Remove("BuildingName");
            if (dtTB_RoomInsert.Rows.Count > 0)
                mCommonManager.DTToDB(dtTB_RoomInsert, "TB_Room", new string[] { "Name", "RoomSexType","RoomType"
                ,"RoomType2", "Creator", "SiteID", "DormAreaID","BuildingID","KeyCount" }, DBO.GetInstance().CreateConnection().ConnectionString);
            #endregion
            #region 床位号
            dtTB_DormArea = _mTB_DormAreaDAL.GetTableBySite(site);
            dtTB_Building = _mTB_BuildingDAL.GetTableBySiteID(site);
            dtTB_Room = _mTB_RoomDAL.GetTableBySiteID(site);
            DataTable dtTB_Bed = _mTB_BedDAL.GetTableBySite(site);
            DataTable dtTB_BedInsert = dtTB_Bed.Clone();
            DataRow[] drTB_BedArr = null;
            var queryBed = from v in dt.AsEnumerable()
                           select new
                           {
                               DormAreaName = v.Field<string>("宿舍区"),
                               BuildingName = v.Field<string>("楼栋"),
                               RoomName = v.Field<string>("房间号"),
                               BedName = v.Field<string>("床位号"),
                               BedStatus = v.Field<string>("床位状态"),
                               KeyCount = v.Field<string>("钥匙数量"),
                           };
                int iInsertCount = 0;
            foreach (var item in queryBed.ToList().Distinct())
            {
              if (string.IsNullOrEmpty(item.BedName))
              {
                   errorMsg = item.DormAreaName + item.BuildingName+ item.RoomName + " 存在空床位，请检查后再导入";
                   return 0;
              }
                    drTB_BedArr = dtTB_Bed.Select("BuildingName='" + item.BuildingName + "' and DormAreaName='" + item.DormAreaName + "' and RoomName='" + item.RoomName + "' and  Name='" + item.BedName + "'");
                if (drTB_BedArr.Length <= 0)
                {
                    drTB_DormAreaArr = dtTB_DormArea.Select("Name='" + item.DormAreaName + "'");
                    drTB_BuildingArr = dtTB_Building.Select("Name='" + item.BuildingName + "' and DormAreaName='" + item.DormAreaName + "'");
                    drTB_RoomArr = dtTB_Room.Select("Name='" + item.RoomName + "' and BuildingName='" + item.BuildingName + "' and DormAreaName='" + item.DormAreaName + "'");
                    DataRow drTB_BedInsert = dtTB_BedInsert.NewRow();
                    drTB_BedInsert["DormAreaID"] = drTB_DormAreaArr[0]["ID"];
                    drTB_BedInsert["BuildingID"] = drTB_BuildingArr[0]["ID"];
                    drTB_BedInsert["RoomID"] = drTB_RoomArr[0]["ID"];
                    drTB_BedInsert["Name"] = item.BedName;
                    drTB_BedInsert["SiteID"] = site;
                    drTB_BedInsert["Creator"] = user;
                    drTB_BedInsert["KeyCount"] = item.KeyCount;
                        if (item.BedStatus == RemarkAttribute.GetEnumRemark(TypeManager.BedStatus.Busy))
                        drTB_BedInsert["Status"] = (int)TypeManager.BedStatus.Busy;
                    else if (item.BedStatus == RemarkAttribute.GetEnumRemark(TypeManager.BedStatus.Free))
                        drTB_BedInsert["Status"] = (int)TypeManager.BedStatus.Free;
                    else if (item.BedStatus == RemarkAttribute.GetEnumRemark(TypeManager.BedStatus.Occupy))
                        drTB_BedInsert["Status"] = (int)TypeManager.BedStatus.Occupy;
                    dtTB_BedInsert.Rows.Add(drTB_BedInsert);

                    iInsertCount++;
                }
            }
            dtTB_BedInsert.Columns.Remove("ID");
            dtTB_BedInsert.Columns.Remove("DormAreaName");
            dtTB_BedInsert.Columns.Remove("BuildingName");
            dtTB_BedInsert.Columns.Remove("RoomName");
            if (dtTB_BedInsert.Rows.Count > 0)
                mCommonManager.DTToDB(dtTB_BedInsert, "TB_Bed", new string[] { "Name", "Creator", "SiteID", "DormAreaID"
                ,"BuildingID","RoomID" ,"Status","KeyCount"}, DBO.GetInstance().CreateConnection().ConnectionString);

                return iInsertCount;
            #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        internal void Remove(string strID, DbTransaction tran, Database db)
        {
            TB_AssignRoomDAL mTB_AssignRoomDAL = new TB_AssignRoomDAL();
            TB_ChangeRoomRecordDAL mTB_ChangeRoomRecordDAL = new TB_ChangeRoomRecordDAL();
            TB_EmployeeCheckInDAL mTB_EmployeeCheckInDAL = new TB_EmployeeCheckInDAL();
            TB_EmployeeCheckOutDAL mTB_EmployeeCheckOutDAL = new TB_EmployeeCheckOutDAL();
            mTB_AssignRoomDAL.DeleteByBedID(strID, tran, db);
            mTB_ChangeRoomRecordDAL.DeleteByBedID(strID, tran, db);
            mTB_EmployeeCheckInDAL.DeleteByBedID(strID, tran, db);
            mTB_EmployeeCheckOutDAL.DeleteByBedID(strID, tran, db);
            _mTB_BedDAL.Delete(strID, tran, db);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        public void Remove(string strID)
        {
            Database db = DBO.GetInstance();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction tran = connection.BeginTransaction();
            try
            {
                this.Remove(strID, tran, db);
                tran.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
