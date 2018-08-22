using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL;
using DormManage.Framework;
using DormManage.Models;
using DormManage.BLL.AssignRoom;
using DormManage.BLL.DormManage;
using DormManage.Model;
using DormManage.BLL.DormPersonManage;
using System.IO;
using DormManage.Common;

namespace DormManage.Web.UI.Allowance
{
    public partial class AllowanceApply : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_AllowanceApply mTB_AllowanceApply = new TB_AllowanceApply();
            TB_AllowanceApplyBLL mTB_AllowanceApplyBLL = new TB_AllowanceApplyBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_AllowanceApply.Name = "";
            mTB_AllowanceApply.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_AllowanceApply.CardNo = this.txtScanCardNO.Text.Trim();
            mTB_AllowanceApply.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_AllowanceApply.BU = this.txtBu.Text.Trim();
            DateTime dtCheckOut = DateTime.Now;
            if (DateTime.TryParse(this.txtHireDate.Text.Trim(), out dtCheckOut))
            {
                mTB_AllowanceApply.Hire_Date = dtCheckOut;
            }
            dtCheckOut = DateTime.Now;
            if (DateTime.TryParse(this.txtEffectiveDate.Text.Trim(), out dtCheckOut))
            {
                mTB_AllowanceApply.Effective_Date = dtCheckOut;
            }
            if (ddlEmpType.SelectedIndex > 0)
            {
                mTB_AllowanceApply.EmployeeTypeName = ddlEmpType.SelectedItem.Text;
            }

            DataTable dt = mTB_AllowanceApplyBLL.GetTable(mTB_AllowanceApply, ref pager);
            GridView1.DataSource = dt;
            GridView1.DataBind();
           
            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {
                ddlBind();
                this.Bind(1);
                txtWorkDayNo.Focus();
                //txtScanCardNO.Focus();
            }
        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind()
        {
            TB_AllowanceApply mTB_AllowanceApply = new TB_AllowanceApply();
            TB_AllowanceApplyBLL mTB_AllowanceApplyBLL = new TB_AllowanceApplyBLL();
            #region 宿舍区            
            this.ddlEmpType.DataValueField = "EmployeeTypeName";
            this.ddlEmpType.DataTextField = "EmployeeTypeName";

            this.ddlEmpType.DataSource = mTB_AllowanceApplyBLL.GetAllEmployeeTypes();
            this.ddlEmpType.DataBind();
            this.ddlEmpType.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
        }

        private bool GetIdCardNumber(string sidcard, string sWorkDayNO, out string sIdCardNumber)
        {
            if (string.IsNullOrEmpty(sidcard))
            {
                if (string.IsNullOrEmpty(sWorkDayNO))
                {
                    sIdCardNumber = string.Empty;
                    return false;
                }
                var empInfo = CommonBLL.GetEmployeeInfo(sWorkDayNO);
                if (null == empInfo)
                {
                    sIdCardNumber = string.Empty;
                    return false;
                }
                else
                {
                    sIdCardNumber = empInfo.idCardNum;
                    return true;
                }
            }
            else
            {
                sIdCardNumber = sidcard;
                return true;
            }
        }
        private void ClearWorkIDInput()
        {
            this.txtWorkDayNo.Text = "";
            this.txtWorkDayNo.Focus();
            this.txtScanCardNO.Text = "";
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                var sInputID = this.txtScanCardNO.Text.Trim();
                var sWorkDayNO = this.txtWorkDayNo.Text.Trim();
                string sIdCard = string.Empty;
                GetIdCardNumber(sInputID, sWorkDayNO, out sIdCard);

                TB_AllowanceApplyBLL bll = new TB_AllowanceApplyBLL();
                //查询人员信息
                DataTable dtEmployeeInfo = new StaffingBLL().GetTableWithIDL(sWorkDayNO, sIdCard);

                if (null != dtEmployeeInfo && dtEmployeeInfo.Rows.Count > 0)
                {
                    //检查用工类型
                    if(dtEmployeeInfo.Rows[0]["EmployeeTypeName"].ToString()!="普工")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户用工类型为："+ dtEmployeeInfo.Rows[0]["EmployeeTypeName"].ToString() + "，不能申请住房津贴')", true);
                        return;
                    }

                    //检查是否已经申请津贴
                    TB_AllowanceApply tb_AllowanceApply = new TB_AllowanceApply();
                    tb_AllowanceApply.EmployeeNo = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString();
                    tb_AllowanceApply.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
                    Pager pager = new Pager();
                    pager.CurrentPageIndex = 1;
                    pager.srcOrder = "  ID desc";

                    DataTable dt= bll.GetTableByID(tb_AllowanceApply, ref pager);
                    if(dt.Rows.Count>0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已经申请过住房津贴，不能重复申请')", true);
                        return;
                    }

                    //检查是否有分配记录
                    DataTable  dtAssignArea = new AssignRoomBLL().GetAssignDormArea(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString());
                    if (dtAssignArea.Rows.Count>0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有分配住宿记录！暂时不能申请')", true);
                        return;
                    }

                    //检查是否已经有CheckIn的记录
                    DataTable DtCheck = new AssignRoomBLL().GetAssignedData(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString(), "");
                    if (DtCheck != null && DtCheck.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有入住记录！请先退宿舍')", true);
                    }
                    else
                    {
                        TB_AllowanceApply tB_AllowanceApply = new TB_AllowanceApply();
                        tB_AllowanceApply.EmployeeNo = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString();
                        tB_AllowanceApply.Name = dtEmployeeInfo.Rows[0]["ChineseName"].ToString();
                        tB_AllowanceApply.CardNo = sIdCard;
                        tB_AllowanceApply.Sex = dtEmployeeInfo.Rows[0]["Sex"].ToString();
                        tB_AllowanceApply.Company = "";
                        tB_AllowanceApply.BU = dtEmployeeInfo.Rows[0]["SegmentName"].ToString();
                        tB_AllowanceApply.Grade = 0;
                        tB_AllowanceApply.CheckOutDate = GetCheckOutDate(tB_AllowanceApply.EmployeeNo);
                        tB_AllowanceApply.EmployeeTypeName = dtEmployeeInfo.Rows[0]["EmployeeTypeName"].ToString();
                        tB_AllowanceApply.CreateUser = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount);
                        tB_AllowanceApply.CreateDate = System.DateTime.Now;
                        tB_AllowanceApply.SiteID= (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
                        tB_AllowanceApply.Hire_Date= Convert.ToDateTime(dtEmployeeInfo.Rows[0]["Hire_Date"]);
                        tB_AllowanceApply.Effective_Date = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddHours(-DateTime.Now.Hour + 1).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
                        bll.ADDAllowanceApply(tB_AllowanceApply);

                       

                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('申请成功！')", true);
                        this.Bind(1);
                        if (dtEmployeeInfo.Rows[0]["Phone"].ToString() != "")
                        {
                            //string sContent = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString() + "亲，以下是你被分配的宿舍信息：" + sDormAreaName + "宿舍 " + sBuildingName + "栋 " + sRoomName + "房间 " + sBedName + "床.  该宿舍的服务热线18926980019,请于3天内前往宿舍区办理入住手续，谢谢！ ";
                            try
                            {
                                //SendSMS(dtEmployeeInfo.Rows[0]["Phone"].ToString(), sContent);
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('招聘系统找不到此用户！')", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('" + ex.Message + "')", true);
            }
            finally
            {
                ClearWorkIDInput();
            }
        }

        private string  GetCheckOutDate(string EmployeeID)
        {
            TB_EmployeeCheckOut mTB_EmployeeCheckOut = new TB_EmployeeCheckOut();
            EmployeeCheckOutBLL mEmployeeCheckOutBLL = new EmployeeCheckOutBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = 1;
            pager.srcOrder = "  ID desc";

            mTB_EmployeeCheckOut.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_EmployeeCheckOut.EmployeeNo = EmployeeID;
            mTB_EmployeeCheckOut.Name ="";
            mTB_EmployeeCheckOut.CardNo ="";
            dtSource = mEmployeeCheckOutBLL.GetPagerData(mTB_EmployeeCheckOut, ref pager);

            return dtSource.Rows.Count > 0 ? dtSource.Rows[0]["CheckOutDate"].ToString() : "未住宿舍";
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            TB_AllowanceApply mTB_AllowanceApply = new TB_AllowanceApply();
            TB_AllowanceApplyBLL mTB_AllowanceApplyBLL = new TB_AllowanceApplyBLL();
            mTB_AllowanceApply.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_AllowanceApply.Name = "";
            mTB_AllowanceApply.CardNo = this.txtScanCardNO.Text.Trim();
            mTB_AllowanceApply.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_AllowanceApply.BU = this.txtBu.Text.Trim();
            DateTime dtCheckOut = DateTime.Now;
            if (DateTime.TryParse(this.txtHireDate.Text.Trim(), out dtCheckOut))
            {
                mTB_AllowanceApply.Hire_Date = dtCheckOut;
            }
            dtCheckOut = DateTime.Now;
            if (DateTime.TryParse(this.txtEffectiveDate.Text.Trim(), out dtCheckOut))
            {
                mTB_AllowanceApply.Effective_Date = dtCheckOut;
            }
            if (ddlEmpType.SelectedIndex > 0)
            {
                mTB_AllowanceApply.EmployeeTypeName = ddlEmpType.SelectedItem.Text;
            }

            string strFileName = mTB_AllowanceApplyBLL.Export(mTB_AllowanceApply);
            this.DownLoadFile(this.Request, this.Response, "住房津申请记录.xls", File.ReadAllBytes(strFileName), 10240000);
            //File.Delete(strFileName);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            string strID = this.GetCheckedIDs(this.GridView1, "chkLeftSingle");
            try
            {
                new DormAreaBLL().Remove(strID);
                this.Bind(1);
            }
            catch
            {

            }
        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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
                TB_AllowanceApplyBLL mTB_AllowanceApplyBLL = new TB_AllowanceApplyBLL();
                DataTable dtError = mTB_AllowanceApplyBLL.Import(strFilePath);
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
                    SessionHelper.Set(HttpContext.Current, TypeManager.SESSIONKEY_ImpErrAllowanceApply, dtError);
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "myScript", "importComplete();", true);
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Success", "alert('部分导入成功,导入失败记录见文件')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Error", "alert('导入失败！" + ex.Message + "')", true);
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
        
        protected void btnSel_Click(object sender, EventArgs e)
        {
            Bind(1);
        }
    }
}