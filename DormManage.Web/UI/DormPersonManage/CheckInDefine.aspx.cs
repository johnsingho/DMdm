using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormPersonManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;
using DormManage.BLL.DormManage;
using DormManage.BLL.UserManage;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class CheckInDefine : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_EmployeeCheckIn mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
            EmployeeCheckInBLL mEmployeeCheckInBLL = new EmployeeCheckInBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_EmployeeCheckIn.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_EmployeeCheckIn.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_EmployeeCheckIn.Name = this.txtName.Text.Trim();
            mTB_EmployeeCheckIn.CardNo = this.txtScanCardNO.Text.Trim();

            mTB_EmployeeCheckIn.BUID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_EmployeeCheckIn.BU = GetSelectedBu(); //this.txtBu.Text.Trim();
            mTB_EmployeeCheckIn.Telephone = this.txtMobile.Text.Trim();

            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckIn.CheckInDateBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckIn.CheckInDateEnd = dtVal;
            }

            int iDormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            int iRoomTypeID = Convert.ToInt32(this.ddlRoomType.SelectedValue);
            string sRoomName = Convert.ToString(this.txtRoom.Text);           

            dtSource = mEmployeeCheckInBLL.GetPagerData(mTB_EmployeeCheckIn,iDormAreaID, iRoomTypeID,sRoomName, ref pager);
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

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind()
        {
            Pager mPager = null;
            #region 宿舍区
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormArea.DataValueField = TB_DormArea.col_ID;
            this.ddlDormArea.DataTextField = TB_DormArea.col_Name;

            this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager);
            this.ddlDormArea.DataBind();
            this.ddlDormArea.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼栋
            this.ddlBuildingName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 房间类型
            RoomTypeBLL mRoomTypeBLL = new RoomTypeBLL();
            this.ddlRoomType.DataValueField = TB_RoomType.col_ID;
            this.ddlRoomType.DataTextField = TB_RoomType.col_Name;
            this.ddlRoomType.DataSource = mRoomTypeBLL.GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            this.ddlRoomType.DataBind();
            this.ddlRoomType.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion

            //事业部
            BindBu();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            try
            {                
                if (!IsPostBack)
                {
                    ddlBind();
                    this.Bind(1);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("CheckInDefine::Page_Load", ex);
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

        protected void btnNew_Click(object sender, EventArgs e)
        {

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
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dv = e.Row.DataItem as DataRowView;
                    Literal ltlSex = e.Row.Cells[3].FindControl("ltlSex") as Literal;
                    if (Convert.ToInt32(dv[TB_EmployeeCheckIn.col_Sex]) == (int)TypeManager.Sex.Male)
                    {
                        ltlSex.Text = RemarkAttribute.GetEnumRemark(TypeManager.Sex.Male);
                    }
                    else if (Convert.ToInt32(dv[TB_EmployeeCheckIn.col_Sex]) == (int)TypeManager.Sex.Female)
                    {
                        ltlSex.Text = RemarkAttribute.GetEnumRemark(TypeManager.Sex.Female);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
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
                EmployeeCheckInBLL mEmployeeCheckInBLL = new EmployeeCheckInBLL();
                DataTable dtError = mEmployeeCheckInBLL.Import(strFilePath);
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
                    Cache mCache = new Cache(this.UserInfo == null ? this.SystemAdminInfo.Account : this.UserInfo.ADAccount, (this.UserInfo == null ? this.SystemAdminInfo.SiteID : this.UserInfo.SiteID) + "dtError");
                    mCache.SetCache(dtError);
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Success", "alert('部分导入成功,导入失败记录见文件')", true);
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "myScript", "importComplete();", true);
                    
                }
            }
            catch(Exception ex)
            {
                var sErr = string.Format("alert(\"导入失败！{0}\")", ex.Message);
                sErr = sErr.Replace("\r\n", "");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Error", sErr, true);
            }
            finally
            {
                ViewState["FilePath"] = null;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            TB_EmployeeCheckIn mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
            EmployeeCheckInBLL mEmployeeCheckInBLL = new EmployeeCheckInBLL();
            mTB_EmployeeCheckIn.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_EmployeeCheckIn.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_EmployeeCheckIn.Name = this.txtName.Text.Trim();
            mTB_EmployeeCheckIn.CardNo = this.txtScanCardNO.Text.Trim();
            mTB_EmployeeCheckIn.BUID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_EmployeeCheckIn.BU = GetSelectedBu();//this.txtBu.Text.Trim();
            mTB_EmployeeCheckIn.Telephone = this.txtMobile.Text.Trim();

            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckIn.CheckInDateBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckIn.CheckInDateEnd = dtVal;
            }

            int iDormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            int iRoomTypeID = Convert.ToInt32(this.ddlRoomType.SelectedValue);
            string sRoomName = Convert.ToString(this.txtRoom.Text);

            string strFileName = mEmployeeCheckInBLL.Export(mTB_EmployeeCheckIn, iDormAreaID, iRoomTypeID, sRoomName);
            this.DownLoadFile(this.Request, this.Response, "入住记录.xls", File.ReadAllBytes(strFileName), 10240000);
            //File.Delete(strFileName);
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