using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Models;
using DormManage.BLL.AssignRoom;
using DormManage.BLL;

namespace DormManage.Web.UI.AssignRoom
{
    public partial class AssignRoom : BasePage
    {

        #region 私有方法
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="intCurrentIndex"></param>
        private void Bind(int CurrentPageIndex, int DormAreaID)
        {
            TB_Bed mTB_Bed = new TB_Bed();
            BedBLL mBedBLL = new BedBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = CurrentPageIndex;
            pager.srcOrder = "  ID desc";

            mTB_Bed.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Bed.DormAreaID = DormAreaID;
            mTB_Bed.BuildingID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_Bed.UnitID = Convert.ToInt32(Request.Form[this.ddlUnit.UniqueID.ToString()]);
            mTB_Bed.FloorID = Convert.ToInt32(Request.Form[this.ddlFloor.UniqueID.ToString()]);
            mTB_Bed.RoomID = Convert.ToInt32(Request.Form[this.ddlRoom.UniqueID.ToString()]);
            mTB_Bed.Status = (int)TypeManager.BedStatus.Free;
            mTB_Bed.RoomType = Convert.ToInt32(this.ddlRoomType.SelectedValue==""?"0": this.ddlRoomType.SelectedValue);
            mTB_Bed.RoomSexType = this.ddlRoomSexType.SelectedValue;
            mTB_Bed.ID = Convert.ToInt32(Request.Form[this.ddlBeg.UniqueID.ToString()]);
           
            //查询空床位
            DataTable dt  = mBedBLL.GetTableByEnableStatus(mTB_Bed, ref pager);
            ViewState["dtFreeRoom"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind(DataTable dtBuildingName)
        {
            Pager mPager = null;
            #region 宿舍区
            //DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            //this.ddlDormArea.DataValueField = TB_DormArea.col_ID;
            //this.ddlDormArea.DataTextField = TB_DormArea.col_Name;

            //this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager);
            //this.ddlDormArea.DataBind();
            //this.ddlDormArea.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼栋
            this.ddlBuildingName.DataSource = dtBuildingName.DefaultView;
            this.ddlBuildingName.DataValueField = TB_DormArea.col_ID;
            this.ddlBuildingName.DataTextField = TB_DormArea.col_Name;
            this.ddlBuildingName.DataBind();
            this.ddlBuildingName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
        
           
            #endregion
            #region 单元
            this.ddlUnit.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼层
            this.ddlFloor.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 房间类型
            RoomTypeBLL mRoomTypeBLL = new RoomTypeBLL();
            this.ddlRoomType.DataValueField = TB_RoomType.col_ID;
            this.ddlRoomType.DataTextField = TB_RoomType.col_Name;

            this.ddlRoomType.DataSource = mRoomTypeBLL.GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            this.ddlRoomType.DataBind();
            this.ddlRoomType.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 房间号
            this.ddlRoom.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
        }
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            ClientScript.RegisterForEventValidation(this.ddlBuildingName.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlUnit.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFloor.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlRoom.UniqueID, "argument");
            //ClientScript.RegisterForEventValidation(this.ddlBedStatus.UniqueID, "argument");
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
            if (!IsPostBack)
            {                
                this.GridView1.Columns[3].Visible = false;
                this.GridView1.Columns[4].Visible = false;
                txtWorkDayNo.Focus();
                //txtScanCardNO.Focus();

                var sWordDayNo = string.Empty;
                var sIDNo = string.Empty;
                var obj = Request.Params["WordDayNo"];
                if (null != obj)
                {
                    txtWorkDayNo.Text = obj.ToString();
                }
                obj = Request.Params["IDNo"];
                if (null != obj)
                {
                    txtScanCardNO.Text = obj.ToString();
                }

            }
        }

        private void ClearControl()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ddlBuildingName.DataSource = null;
            ddlBuildingName.DataBind();
            txtWorkDayNo.Text = "";
            txtScanCardNO.Text = "";
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = e.Row.DataItem as DataRowView;
                Literal ltlBedStatus = e.Row.Cells[7].FindControl("ltlBedStatus") as Literal;
                if (Convert.ToInt32(dv[TB_Bed.col_Status]) == (int)TypeManager.BedStatus.Free)
                {
                    ltlBedStatus.Text = RemarkAttribute.GetEnumRemark(TypeManager.BedStatus.Free);
                }
                else if (Convert.ToInt32(dv[TB_Bed.col_Status]) == (int)TypeManager.BedStatus.Occupy)
                {
                    ltlBedStatus.Text = RemarkAttribute.GetEnumRemark(TypeManager.BedStatus.Occupy);
                }
                else if (Convert.ToInt32(dv[TB_Bed.col_Status]) == (int)TypeManager.BedStatus.Busy)
                {
                    ltlBedStatus.Text = RemarkAttribute.GetEnumRemark(TypeManager.BedStatus.Busy);
                }
            }
        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            int dormAreaID = Convert.ToInt32(ViewState["dormAreaID"]);
            this.Bind(Convert.ToInt32(e.CommandArgument), dormAreaID);
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var sInputID = this.txtScanCardNO.Text.Trim();
            var sWorkDayNO = this.txtWorkDayNo.Text.Trim();
            string sIdCard = string.Empty;
            GetIdCardNumber(sInputID, sWorkDayNO, out sIdCard);

            //查询人员信息
            DataTable dtEmployeeInfo = new StaffingBLL().GetTableWithIDL(sWorkDayNO, sIdCard);
            if (!DataTableHelper.IsEmptyDataTable(dtEmployeeInfo))
            {
                ddlRoomSexType.SelectedValue = dtEmployeeInfo.Rows[0]["Sex"].ToString();

                //检查是否申请住房津贴
                bool isHaveApplyAllowance = new AssignRoomBLL().CheckAllowanceApply(dtEmployeeInfo.Rows[0]["EmployeeID"].ToString());
                if (isHaveApplyAllowance)
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户申请了住房津贴！如需入住，请申请取消津贴')", true);
                    return;
                }

                //检查是否有分配记录
                DataTable dtAssignArea = new AssignRoomBLL().GetAssignDormArea(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString());
                if (dtAssignArea.Rows.Count > 0)
                {
                    int dormAreaID = Convert.ToInt32(dtAssignArea.Rows[0]["DormAreaID"].ToString());
                    ViewState["dormAreaID"] = dormAreaID;
                    DormManageAjaxServices ser = new DormManageAjaxServices();
                    DataTable dtBuild = ser.GetBuildingByDormAreaID(dormAreaID);
                    ddlBind(dtBuild);
                    Bind(1, dormAreaID);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户还没分配宿舍！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('招聘系统找不到此用户！')", true);
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选中的ID
                string bagID = selbagID.Value;
                //查询room信息
                DataTable dt = ViewState["dtFreeRoom"] as DataTable;
                DataRow[] drAssignRoomArr = dt.Select("ID=" + bagID + "");
                var drAssign = drAssignRoomArr[0];

                var sInputID = this.txtScanCardNO.Text.Trim();
                var sWorkDayNO = this.txtWorkDayNo.Text.Trim();
                string sIdCard = string.Empty;
                GetIdCardNumber(sInputID, sWorkDayNO, out sIdCard);

                //查询人员信息
                DataTable dtEmployeeInfo = new StaffingBLL().GetTableWithIDL(sWorkDayNO, sIdCard);
                if (!DataTableHelper.IsEmptyDataTable(dtEmployeeInfo))
                {
                    var drEmp = dtEmployeeInfo.Rows[0];
                    if (drEmp["Sex"].ToString() != drAssign["RoomSexType"].ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('性别不符合宿舍定义，不能分配！')", true);
                        return;
                    }

                    string sDormAreaName = drAssign["DormAreaName"].ToString();
                    string sBuildingName = drAssign["BuildingName"].ToString();
                    string sRoomName = drAssign["RoomName"].ToString();
                    string sBedName = drAssign["Name"].ToString();
                                      

                    //检查是否已经有CheckIn的记录
                    DataTable DtCheckIDCard = new AssignRoomBLL().GetAssignedData(drEmp["IDCardNumber"].ToString(), "");
                    DataTable DtCheckEmployeeID = new AssignRoomBLL().GetAssignedData(drEmp["EmployeeID"].ToString(), "");
                    if (DtCheckIDCard != null && DtCheckIDCard.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有分配记录！')", true);
                    }
                    else if(DtCheckEmployeeID != null && DtCheckEmployeeID.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有分配记录！')", true);
                    }
                    else
                    {                        
                        TB_EmployeeCheckIn mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
                        mTB_EmployeeCheckIn.RoomID = Convert.ToInt32(drAssign["RoomID"]);
                        mTB_EmployeeCheckIn.BedID = int.Parse(drAssign["ID"].ToString());
                        mTB_EmployeeCheckIn.BU = Util.NormalBU(drEmp["SegmentName"].ToString());
                        //TODO 2018-02-07 
                        //由于 EHR.[Segment] 的ID 与 DormManage.[TB_BU]根本不对应
                        //因此，换房记录无法导出事业部
                        mTB_EmployeeCheckIn.BUID = drEmp["SegmentID"] != DBNull.Value 
                                                    ? Convert.ToInt32(drEmp["SegmentID"]) 
                                                    : 0;
                        mTB_EmployeeCheckIn.CardNo = string.IsNullOrEmpty(sIdCard) ? drEmp["IDCardNumber"].ToString() : sIdCard;
                        mTB_EmployeeCheckIn.CheckInDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                        mTB_EmployeeCheckIn.Company = string.Empty;
                        mTB_EmployeeCheckIn.EmployeeNo = drEmp["EmployeeID"].ToString();
                        mTB_EmployeeCheckIn.Name = drEmp["ChineseName"].ToString();
                        mTB_EmployeeCheckIn.Sex = drAssign["RoomSexType"].ToString() == "男" ? 1 : 2;
                        mTB_EmployeeCheckIn.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
                        mTB_EmployeeCheckIn.Creator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount);
                        mTB_EmployeeCheckIn.IsActive = (int)TypeManager.IsActive.Valid;
                        mTB_EmployeeCheckIn.Telephone = drEmp["Phone"].ToString();
                        mTB_EmployeeCheckIn.EmployeeTypeName = drEmp["EmployeeTypeName"].ToString();
                        var bAssign = new AssignRoomBLL().AssignRoom(mTB_EmployeeCheckIn);

                        if (!bAssign)
                        {
                            var msg = "分配房间失败，请重新分配";
                            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('" + msg + "')", true);
                            return;
                        }
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('分配成功！')", true);
                        ClearControl();
                        if (drEmp["Phone"].ToString() != "")
                        {
                            string sContent = drEmp["EmployeeID"].ToString() + "亲，以下是你被分配的宿舍信息：" + sDormAreaName + "宿舍 " + sBuildingName + "栋 " + sRoomName + "房间 " + sBedName + "床.  该宿舍的服务热线18926980019,请于3天内前往宿舍区办理入住手续，谢谢！ ";
                            try
                            {
                                //SendSMS(drEmp["Phone"].ToString(), sContent);
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
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('"+ex.Message+"')", true);
            }
            finally
            {
                ClearWorkIDInput();
            }
        }

        public void SendSMS(string sPhone, string sContent)
        {
            WebReferenceSMS.FlexDevCommonSMS oSMS = new WebReferenceSMS.FlexDevCommonSMS();
            oSMS.SendSMS("55f39cad-b65d-425d-a0d2-b15cc19f423b", sPhone, sContent);
        }

        protected void btnReLoad_Click(object sender, EventArgs e)
        {
            int dormAreaID = Convert.ToInt32(ViewState["dormAreaID"]);
            this.Bind(1, dormAreaID);
        }
    }
}