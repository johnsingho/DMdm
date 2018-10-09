using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using System.Data.Common;
using System.Web;

namespace DormManage.BLL.DormPersonManage
{
    public class ChargingBLL
    {
        private TB_EmployeeCheckInDAL _mTB_EmployeeCheckInDAL = null;
        private TB_ChargingDAL _mTB_ChargingDAL = null;
        private TB_BUDAL _mTB_BUDAL = null;
        private ExcelHelper _mExcelHelper = null;

        public ChargingBLL()
        {
            _mTB_EmployeeCheckInDAL = new TB_EmployeeCheckInDAL();
            _mTB_ChargingDAL = new TB_ChargingDAL();
            _mExcelHelper = new ExcelHelper();
            _mTB_BUDAL = new TB_BUDAL();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Charging"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_Charging tb_Charging, ref Pager pager)
        {
            return _mTB_ChargingDAL.GetTable(tb_Charging, ref pager);
        }

        public DataTable GetTableMonthByEmployeeNo(string EmployeeNo)
        {
            return _mTB_ChargingDAL.GetTableMonthByEmployeeNo(EmployeeNo);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tb_Charging"></param>
        /// <returns></returns>
        public int Add(TB_Charging tb_Charging, DbTransaction tran)
        {
            return _mTB_ChargingDAL.Create(tb_Charging, tran);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tb_Charging"></param>
        /// <returns></returns>
        public int Edit(TB_Charging tb_Charging)
        {
            return _mTB_ChargingDAL.Edit(tb_Charging);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="tb_ChangeRoomRecord"></param>
        /// <returns></returns>
        public string Export(TB_Charging tb_Charging)
        {
            DataTable dt = _mTB_ChargingDAL.GetTable(tb_Charging);
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "Report");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, DateTime.Now.ToString("yyMMddHHmmssms_") + "扣费记录.xls");
            new ExcelHelper().RenderToExcel(dt, strFileName);
            return strFileName;
        }


        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>导入失败记录</returns>
        public DataTable Import(string filePath, out string sErr)
        {
            sErr = string.Empty;
            //读取Excel内容
            DataTable dt = _mExcelHelper.GetDataFromExcel(filePath);
            DataTable dtBU = new DataTable();
            //SiteID
            int intSiteID = SessionHelper.Get(HttpContext.Current, TypeManager.User) != null ?
                ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).SiteID :
                ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).SiteID;
            //操作用户账号
            string currentUser = SessionHelper.Get(HttpContext.Current, TypeManager.User) != null ?
                ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount :
                ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account;
            DataTable dtTB_ChargingInsert = new DataTable();//费用信息
            dtTB_ChargingInsert.Columns.Add("EmployeeNo");
            dtTB_ChargingInsert.Columns.Add("Name");
            dtTB_ChargingInsert.Columns.Add("CardNo");
            dtTB_ChargingInsert.Columns.Add("ChargeContent");
            dtTB_ChargingInsert.Columns.Add("Money");
            dtTB_ChargingInsert.Columns.Add("AirConditionFee");
            dtTB_ChargingInsert.Columns.Add("AirConditionFeeMoney");
            dtTB_ChargingInsert.Columns.Add("RoomKeyFee");
            dtTB_ChargingInsert.Columns.Add("RoomKeyFeeMoney");
            dtTB_ChargingInsert.Columns.Add("OtherFee");
            dtTB_ChargingInsert.Columns.Add("OtherFeeMoney");
            dtTB_ChargingInsert.Columns.Add("CreateTime");
            dtTB_ChargingInsert.Columns.Add("Creator");
            dtTB_ChargingInsert.Columns.Add("UpdateDate");
            dtTB_ChargingInsert.Columns.Add("UpdateBy");
            dtTB_ChargingInsert.Columns.Add("SiteID");
            dtTB_ChargingInsert.Columns.Add("BUID");
            dtTB_ChargingInsert.Columns.Add("BU");
  

            //获取到整个site的所有入住人员信息
            //DataTable dtEmployeeCheckIn = _mTB_EmployeeCheckInDAL.GetTableBySiteID(intSiteID);
            //获取到整个site的所有床位信息
            //DataTable dtTB_Bed = _mTB_BedDAL.GetTableBySite(intSiteID);
            DataTable dtError = dt.Clone();
            StringBuilder sbBedID = new StringBuilder();//用于保存床位ID
            #region
            int buID = 0;

            #endregion

            dtBU = _mTB_BUDAL.Get();

            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    DataRow[] drBu = dtBU.Select("name='" + dr[TypeManager.Col_BU].ToString() + "'");
                    buID = drBu.Length>0? Convert.ToInt32(drBu[0]["ID"].ToString()):0;
                  
                    DataRow drTB_ChargingInsert = dtTB_ChargingInsert.NewRow();

                    drTB_ChargingInsert["EmployeeNo"] = dr[TypeManager.Col_EmployeeNo].ToString();
                    drTB_ChargingInsert["Name"] = dr[TypeManager.Col_Name].ToString();
                    drTB_ChargingInsert["CardNo"] =""; 
                    drTB_ChargingInsert["ChargeContent"] = dr[TypeManager.Col_ChargingContent].ToString();
                    drTB_ChargingInsert["Money"] = dr[TypeManager.Col_Charging].ToString();
                    drTB_ChargingInsert["AirConditionFee"] = dr["AirConditionFee"].ToString();
                    drTB_ChargingInsert["AirConditionFeeMoney"] = dr["AirConditionFeeMoney"].ToString();
                    drTB_ChargingInsert["RoomKeyFee"] = dr["RoomKeyFee"].ToString();
                    drTB_ChargingInsert["RoomKeyFeeMoney"] = dr["RoomKeyFeeMoney"].ToString();
                    drTB_ChargingInsert["OtherFee"] = dr["OtherFee"].ToString();
                    drTB_ChargingInsert["OtherFeeMoney"] = dr["OtherFeeMoney"].ToString();
                    drTB_ChargingInsert["CreateTime"] = DateTime.Now;
                    drTB_ChargingInsert["Creator"] = currentUser;
                    drTB_ChargingInsert["UpdateDate"] = DateTime.Now;
                    drTB_ChargingInsert["UpdateBy"] = currentUser;
                    drTB_ChargingInsert["SiteID"] = intSiteID;

                    drTB_ChargingInsert["BUID"] = buID;
                    drTB_ChargingInsert["BU"] = dr[TypeManager.Col_BU].ToString();



                    dtTB_ChargingInsert.Rows.Add(drTB_ChargingInsert);


                    if (dtTB_ChargingInsert.Rows.Count > 0)
                    {
                        //添加入住记录
                        new CommonManager().DTToDB(dtTB_ChargingInsert, "TB_Charging"
                            , new string[] { "EmployeeNo", "Name", "CardNo", "ChargeContent", "Money", "AirConditionFee", "AirConditionFeeMoney", "RoomKeyFee", "RoomKeyFeeMoney", "OtherFee", "OtherFeeMoney", "CreateTime", "Creator", "UpdateDate", "UpdateBy", "SiteID", "BUID", "BU" }
                            , DBO.GetInstance().CreateConnection().ConnectionString);

                        dtTB_ChargingInsert.Rows.Clear();
                    }
                }
                catch(Exception ex)
                {
                    sErr = ex.Message;
                    dtError.ImportRow(dr);
                }
            }

            return dtError;
        }

    }
}
