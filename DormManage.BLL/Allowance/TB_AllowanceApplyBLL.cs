using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;
using DormManage.Models;
using DormManage.Framework;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DormManage.Common;
using DormManage.Model;
using System.IO;
using System.Web;

namespace DormManage.BLL.DormManage
{
    public class TB_AllowanceApplyBLL
    {
        private TB_AllowanceApplyDAL _mTB_AllowanceApply;
         ExcelHelper _mExcelHelper=null;
        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public TB_AllowanceApplyBLL()
        {
            _mTB_AllowanceApply = new TB_AllowanceApplyDAL();
            _mExcelHelper = new ExcelHelper();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="mTB_DormArea"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_AllowanceApply tb_AllowanceApply, ref Pager pager)
        {
            return _mTB_AllowanceApply.GetTable(tb_AllowanceApply, ref pager);
        }

        /// <summary>
        /// ID查询
        /// </summary>
        /// <param name="mTB_DormArea"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTableByID(TB_AllowanceApply tb_AllowanceApply, ref Pager pager)
        {
          
            return _mTB_AllowanceApply.GetTable(tb_AllowanceApply, ref pager);
        }

        /// <summary>
        /// ID查询
        /// </summary>
        /// <param name="mTB_DormArea"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTableByEmployeeNo(string EmployeeNo)
        {

            return _mTB_AllowanceApply.GetTable(EmployeeNo);
        }

        public object GetAllEmployeeTypes()
        {
            return _mTB_AllowanceApply.GetAllEmployeeTypes();
        }

        public int ADDAllowanceApply(TB_AllowanceApply tb_AllowanceApply)
        {
            return _mTB_AllowanceApply.Create(tb_AllowanceApply);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <returns></returns>
        public string Export(TB_AllowanceApply tB_AllowanceApply)
        {
            DataTable dt = _mTB_AllowanceApply.GetTable(tB_AllowanceApply);
        
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "Report");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, DateTime.Now.ToString("yyMMddHHmmssms_") + "住房津贴申请.xls");
            _mExcelHelper.RenderToExcel(dt, strFileName);
            return strFileName;
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
          
            //获取到整个site的所有入住人员信息
            DataTable dtEmployeeCheckIn = new TB_EmployeeCheckInDAL().GetTableBySiteID(intSiteID);
          
            DataTable dtError = dt.Clone();

            StringBuilder sbBedID = new StringBuilder();//用于保存床位ID
         
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    DataRow[] drEmployeeCheckInArr = dtEmployeeCheckIn.Select("CardNo='" + dr["身份证号码"] + "'");
                    if (drEmployeeCheckInArr.Length > 0)
                    {
                        dr["BZ"] = "已有入住记录";
                        dtError.ImportRow(dr);
                   
                    }
                    else
                    {
                        TB_AllowanceApply tB_AllowanceApply = new TB_AllowanceApply();
                        tB_AllowanceApply.EmployeeNo = dr["工号"].ToString();
                        tB_AllowanceApply.Name = dr["姓名"].ToString();
                        tB_AllowanceApply.CardNo = dr["身份证号码"].ToString();
                        tB_AllowanceApply.Sex = dr["性别"].ToString();
                        tB_AllowanceApply.Company = dr["公司"].ToString(); 
                        tB_AllowanceApply.BU = dr["事业部"].ToString();
                        tB_AllowanceApply.Grade = Convert.ToInt32(dr["级别"].ToString());
                        tB_AllowanceApply.CheckOutDate = dr["退宿日期"].ToString();
                        tB_AllowanceApply.EmployeeTypeName = dr["用工类型"].ToString();
                        tB_AllowanceApply.CreateUser = currentUser;
                        tB_AllowanceApply.CreateDate = System.DateTime.Now;
                        tB_AllowanceApply.SiteID = intSiteID;
                        tB_AllowanceApply.Hire_Date = Convert.ToDateTime(dr["入职日期"].ToString());
                        ADDAllowanceApply(tB_AllowanceApply);
                    }
                }
                catch
                {
                    dr["BZ"] = "已有申请记录";
                    dtError.ImportRow(dr);
                }
            }
         
            return dtError;
        }


    }
}
