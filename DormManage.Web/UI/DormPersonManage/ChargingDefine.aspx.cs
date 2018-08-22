using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormPersonManage;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;
using DormManage.BLL.UserManage;
using DormManage.Common;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class ChargingDefine : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_Charging mTB_Charging = new TB_Charging();
            ChargingBLL mChargingBLL = new ChargingBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_Charging.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Charging.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_Charging.Name = this.txtName.Text.Trim();
            mTB_Charging.BU = GetSelectedBu(); //this.txtBu.Text.Trim();
            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_Charging.CreateTimeBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_Charging.CreateTimeEnd = dtVal;
            }

            dtSource = mChargingBLL.GetPagerData(mTB_Charging, ref pager);
            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        private string GetSelectedBu()
        {
            if (ddlBu.SelectedIndex>0)
            {
                var item = ddlBu.SelectedItem;
                return item.Text;
            }
            return string.Empty;
        }
        private void BindBu()
        {
            TB_BU mTB_BU = new TB_BU();
            BUBLL mBUBLL = new BUBLL();
            Pager pager = null;
            mTB_BU.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);

            var dtSource = mBUBLL.GetPagerData(mTB_BU, ref pager);
            this.ddlBu.DataValueField = TB_BU.col_ID;
            this.ddlBu.DataTextField = TB_BU.col_Name;
            this.ddlBu.DataSource = dtSource;
            this.ddlBu.DataBind();
            this.ddlBu.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            try
            {
                if (!IsPostBack)
                {
                    this.Bind(1);
                    BindBu();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("ChargingDefine::Page_Load", ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Bind(1);
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            try
            {
                this.Bind(Convert.ToInt32(e.CommandArgument));
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            TB_Charging mTB_Charging = new TB_Charging();
            mTB_Charging.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Charging.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_Charging.Name = this.txtName.Text.Trim();
            mTB_Charging.BU = GetSelectedBu(); // this.txtBu.Text.Trim();
            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_Charging.CreateTimeBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_Charging.CreateTimeEnd = dtVal;
            }

            string strFileName = new ChargingBLL().Export(mTB_Charging);
            this.DownLoadFile(this.Request, this.Response, "扣费记录.xls", File.ReadAllBytes(strFileName), 10240000);
            //File.Delete(strFileName);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            string strFilePath = ViewState["FilePath"] as String;
            if (string.IsNullOrEmpty(strFilePath))
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Error", "alert('请先上传需要导入的文件！')", true);
                return;
            }
            try
            {
                //导入
                //EmployeeCheckInBLL mEmployeeCheckInBLL = new EmployeeCheckInBLL();
                ChargingBLL mChargingBLL = new ChargingBLL();
                var sErr = string.Empty;
                DataTable dtError = mChargingBLL.Import(strFilePath, out sErr);
                this.Bind(1);
                if (dtError.Rows.Count <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Success", "alert('导入成功！')", true);
                }
                else
                {
                    //string strFileName = Path.Combine(Server.MapPath("..\\..\\"), "report", DateTime.Now.ToString("yyMMddHHmmssms_") + "导入失败记录.xls");
                    //new ExcelHelper().RenderToExcel(dtError, strFileName);
                    //this.DownLoadFile(this.Request, this.Response, "导入失败记录.xls", File.ReadAllBytes(strFileName), 10240000);
                    //File.Delete(strFileName);
                    //Cache mCache = new Cache(this.UserInfo == null ? this.SystemAdminInfo.Account : this.UserInfo.ADAccount, (this.UserInfo == null ? this.SystemAdminInfo.SiteID : this.UserInfo.SiteID) + "dtError");
                    //mCache.SetCache(dtError);
                    SessionHelper.Set(HttpContext.Current, TypeManager.SESSIONKEY_ImpErrCharing, dtError);
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "myScript", "importComplete();", true);
                    var sAlert = string.Format("alert(\"部分导入成功,导入失败记录见文件 {0}\")", sErr);
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Success", sAlert, true);                    
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Error", "alert('导入失败！')", true);
            }
            finally
            {
                ViewState["FilePath"] = null;
            }
        }


        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string savePath = Path.Combine(Server.MapPath("..\\..\\"), "upload", DateTime.Now.ToString("yyyyMM"));
            string newFileName = string.Empty;
            if (!Directory.Exists(savePath))
            {
                //创建目录
                try
                {
                    Directory.CreateDirectory(savePath);
                }
                catch
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>alert('创建上传目录失败！')</script>");
                }
            }
            try
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + FileUpload1.FileName;
                FileUpload1.SaveAs(Path.Combine(savePath, newFileName));
                ViewState["FilePath"] = Path.Combine(savePath, newFileName);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>alert('文件上传成功！')</script>");
            }
            catch
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>alert('文件上传失败！')</script>");
            }
        }
    }
}