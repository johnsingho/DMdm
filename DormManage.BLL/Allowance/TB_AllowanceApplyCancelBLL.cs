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
using DormManage.Model.Allowance;

namespace DormManage.BLL.DormManage
{
    public class TB_AllowanceApplyCancelBLL
    {
        private TB_AllowanceApplyCancelDAL _mTB_AllowanceApplyCancel;
         ExcelHelper _mExcelHelper=null;
        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public TB_AllowanceApplyCancelBLL()
        {
            _mTB_AllowanceApplyCancel = new TB_AllowanceApplyCancelDAL();
            _mExcelHelper = new ExcelHelper();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="mTB_DormArea"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_AllowanceApplyCancel tb_AllowanceApply, ref Pager pager)
        {
            return _mTB_AllowanceApplyCancel.GetTable(tb_AllowanceApply, ref pager);
        }

        public DataTable GetAllEmployeeTypes()
        {
            return _mTB_AllowanceApplyCancel.GetAllEmployeeTypes();
        }

        /// <summary>
        /// ID查询
        /// </summary>
        /// <param name="mTB_DormArea"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTableByID(TB_AllowanceApplyCancel tb_AllowanceApply, ref Pager pager)
        {
          
            return _mTB_AllowanceApplyCancel.GetTable(tb_AllowanceApply, ref pager);
        }

        public int ADDAllowanceCancelApply(TB_AllowanceApplyCancel tb_AllowanceApplyCancel)
        {
            return _mTB_AllowanceApplyCancel.Create(tb_AllowanceApplyCancel);
        }

        public DataTable GetTableByEmployeeNo(string EmployeeNo)
        {
            return _mTB_AllowanceApplyCancel.GetTable(EmployeeNo);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <returns></returns>
        public string Export(TB_AllowanceApplyCancel tB_AllowanceApply)
        {
            DataTable dt = _mTB_AllowanceApplyCancel.GetTable(tB_AllowanceApply);
        
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "Report");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, DateTime.Now.ToString("yyMMddHHmmssms_") + "住房津贴申请取消.xls");
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
            int intSiteID = System.Web.HttpContext.Current.Session[TypeManager.User] != null ?
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).SiteID :
                ((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).SiteID;
            //操作用户账号
            string currentUser = System.Web.HttpContext.Current.Session[TypeManager.User] != null ?
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ADAccount :
                ((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).Account;

            TB_AllowanceApplyBLL mTB_AllowanceApplyBLL = new TB_AllowanceApplyBLL();

            DataTable dtError = dt.Clone();

            StringBuilder sbBedID = new StringBuilder();//用于保存床位ID

            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    DataTable dtAllowanceApply= mTB_AllowanceApplyBLL.GetTableByEmployeeNo(dr["身份证号码"].ToString());
                    if (dtAllowanceApply.Rows.Count==0)
                    {
                        dr["BZ"] = "未申请津贴";
                        dtError.ImportRow(dr);
                    }
                    else
                    {
                        TB_AllowanceApplyCancel tB_AllowanceApplyCancel = new TB_AllowanceApplyCancel();
                        tB_AllowanceApplyCancel.EmployeeNo = dr["工号"].ToString();
                        tB_AllowanceApplyCancel.Name = dr["姓名"].ToString();
                        tB_AllowanceApplyCancel.CardNo = dr["身份证号码"].ToString();
                        tB_AllowanceApplyCancel.Sex = dr["性别"].ToString();
                        tB_AllowanceApplyCancel.Company = dr["公司"].ToString();
                        tB_AllowanceApplyCancel.BU = dr["事业部"].ToString();
                        tB_AllowanceApplyCancel.Grade = Convert.ToInt32(dr["级别"].ToString()); 
                        tB_AllowanceApplyCancel.AlloWanceApplyID = Convert.ToInt32(dtAllowanceApply.Rows[0]["ID"]);
                        tB_AllowanceApplyCancel.EmployeeTypeName = dr["姓名"].ToString();
                        tB_AllowanceApplyCancel.CreateUser = currentUser;
                        tB_AllowanceApplyCancel.CreateDate = System.DateTime.Now;
                        tB_AllowanceApplyCancel.SiteID = intSiteID;
                        tB_AllowanceApplyCancel.Hire_Date = Convert.ToDateTime(dr["入职日期"].ToString());
                        ADDAllowanceCancelApply(tB_AllowanceApplyCancel);
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
