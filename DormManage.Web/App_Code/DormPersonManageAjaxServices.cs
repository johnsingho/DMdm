using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using AjaxPro;
using DormManage.BLL.DormPersonManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Models;

public class DormPersonManageAjaxServices
{
    /// <summary>
    /// 入住员工退房
    /// </summary>
    /// <param name="intID">入住员工id</param>
    [AjaxMethod]
    public void CheckOut(int intID,string sReason)
    {
        new EmployeeCheckInBLL().CheckOut(intID, sReason);
    }

    [AjaxMethod]
    public void ChangeCheckOutReason(int intID, string sReason)
    {
        new EmployeeCheckInBLL().ChangeCheckOutReason(intID, sReason);
    }

    /// <summary>
    /// 入住员工换房
    /// </summary>
    /// <param name="intID"></param>
    /// <param name="intBedID"></param>
    [AjaxMethod]
    public void ChangeRoom(int intID, int intBedID)
    {
        new EmployeeCheckInBLL().ChangeRoom(intID, intBedID);
    }

    /// <summary>
    /// 导出导入入住记录时失败记录
    /// </summary>
    /// <returns></returns>
    //[AjaxMethod]
    //public string[] ExportImportErrorData()
    //{
    //    DataTable DataSource = null;
    //    Cache mCache = new Cache((SessionHelper.Get(HttpContext.Current, TypeManager.User) != null ?
    //            ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ADAccount :
    //            ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).Account), (SessionHelper.Get(HttpContext.Current, TypeManager.User) != null ?
    //            ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).SiteID :
    //            ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).SiteID) + "dtError");
    //    DataSource = mCache.GetCache() as DataTable;
    //    String[] reportSource = new String[3];
    //    StringBuilder sb = new StringBuilder();
    //    try
    //    {
    //        foreach (System.Data.DataRow dr in DataSource.Rows)
    //        {
    //            for (int i = 0; i < DataSource.Columns.Count; i++)
    //            {
    //                sb.Append(dr[i].ToString().Replace("\r\n", " ").Replace("\"", string.Empty));
    //                if (i == DataSource.Columns.Count - 1)
    //                    sb.Append("\n");
    //                else
    //                    sb.Append("\t");
    //            }
    //        }
    //    }
    //    catch { }

    //    reportSource[0] = sb.ToString();
    //    reportSource[1] = DataSource.Rows.Count.ToString();
    //    reportSource[2] = DataSource.Columns.Count.ToString();

    //    return reportSource;
    //}

    [AjaxMethod]
    public DataTable GetEmployeeInfoByEmployeeNo(string strEmployeeNo)
    {
        if (!String.IsNullOrEmpty(strEmployeeNo))
        {
            Pager pager = null;
            DataTable dt = new EmployeeCheckInBLL().GetPagerData(new TB_EmployeeCheckIn { EmployeeNo = strEmployeeNo }, ref pager);
            return dt;
        }
        else return null;
    }

}
