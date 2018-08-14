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

namespace DormManage.Web.UI.AssignRoom
{
    public partial class AssignDormArea : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_DormArea mTB_DormArea = new TB_DormArea();
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_DormArea.Name = "";
            mTB_DormArea.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            DataTable dt = mDormAreaBLL.GetTable(mTB_DormArea, ref pager);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            ViewState["dtDormArea"] = dt;
            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {                
                this.Bind(1);
                txtWorkDayNo.Focus();
                //txtScanCardNO.Focus();
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                var sidcard = this.txtScanCardNO.Text.Trim();
                var sWorkDayNO = this.txtWorkDayNo.Text.Trim();
                string sIdCard = string.Empty;
                if (!GetIdCardNumber(sidcard, sWorkDayNO, out sIdCard))
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('招聘系统找不到此用户！')", true);
                    return;
                }

                //获取选中的ID
                string areaID = selAreaID.Value;
                //查询room信息
                //DataTable dt = ViewState["dtDormArea"] as DataTable;
                //DataRow[] drAssignAreaArr = dt.Select("ID=" + areaID + "");

                //查询人员信息
                DataTable dtEmployeeInfo = new StaffingBLL().GetTableWithIDL(sWorkDayNO, sIdCard);

                if (null != dtEmployeeInfo && dtEmployeeInfo.Rows.Count > 0)
                {
                    //检查是否有分配记录
                    DataTable  dtAssignArea = new AssignRoomBLL().GetAssignDormArea(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString());
                    if (dtAssignArea.Rows.Count>0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有分配记录！')", true);
                        return;
                    }

                    //检查是否已经有CheckIn的记录
                    DataTable DtCheck = new AssignRoomBLL().GetAssignedData(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString(), "");
                    if (DtCheck != null && DtCheck.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有入住记录！')", true);
                    }
                    else
                    {
                        TB_AssignDormArea tB_AssignDormArea = new TB_AssignDormArea();
                        tB_AssignDormArea.DormAreaID = Convert.ToInt32(areaID);
                        tB_AssignDormArea.CardNo = sIdCard;
                        tB_AssignDormArea.EmployeeNo = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString();
                        tB_AssignDormArea.CreateUser = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount);
                        tB_AssignDormArea.CreateDate = System.DateTime.Now;

                        new AssignRoomBLL().AssignArea(tB_AssignDormArea);

                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('分配成功！')", true);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var sidcard = this.txtScanCardNO.Text.Trim();
                var sWorkDayNO = this.txtWorkDayNo.Text.Trim();
                string sIdCard = string.Empty;
                if (!GetIdCardNumber(sidcard, sWorkDayNO, out sIdCard))
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('招聘系统找不到此用户！')", true);
                    return;
                }

                //查询人员信息
                DataTable dtEmployeeInfo = new StaffingBLL().GetTableWithIDL(sWorkDayNO, sIdCard);
                if (null != dtEmployeeInfo && dtEmployeeInfo.Rows.Count > 0)
                {
                    //检查是否有分配记录
                    DataTable dtAssignArea = new AssignRoomBLL().GetAssignDormArea(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString());
                    if (dtAssignArea.Rows.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户没有分配记录！')", true);
                        return;
                    }
                    else
                    {
                        bool isDel = new AssignRoomBLL().DelAssignDormArea(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString());

                        if(isDel)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('删除成功！')", true);
                            this.Bind(1);
                            return;
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

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}