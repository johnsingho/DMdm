using DormManage.BLL.AssignRoom;
using DormManage.BLL.DormManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Models;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.AssignRoom
{
    public partial class QuickAssignRoom : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {
                this.GridView1.Columns[3].Visible = false;
                this.GridView1.Columns[4].Visible = false;
                ddlBind();
                txtScanName.Focus();                
                //txtScanCardNO.Focus();
            }
        }

        #region other
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
            mTB_Bed.RoomType = Convert.ToInt32(this.ddlRoomType.SelectedValue == "" ? "0" : this.ddlRoomType.SelectedValue);
            mTB_Bed.RoomSexType = this.ddlRoomSexType.SelectedValue;
            mTB_Bed.ID = Convert.ToInt32(Request.Form[this.ddlBeg.UniqueID.ToString()]);

            //查询空床位
            DataTable dt = mBedBLL.GetTableByEnableStatus(mTB_Bed, ref pager);
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

        protected override void Render(HtmlTextWriter writer)
        {
            ClientScript.RegisterForEventValidation(this.ddlBuildingName.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlUnit.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFloor.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlRoom.UniqueID, "argument");
            //ClientScript.RegisterForEventValidation(this.ddlBedStatus.UniqueID, "argument");
            base.Render(writer);
        }

        private void ClearControl()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ddlBuildingName.DataSource = null;
            ddlBuildingName.DataBind();
            txtScanName.Text = "";
            txtScanCardNO.Text = "";
        }
        
        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            //int dormAreaID = Convert.ToInt32(ViewState["dormAreaID"]);
            int dormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            Bind(Convert.ToInt32(e.CommandArgument), dormAreaID);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dv = e.Row.DataItem as DataRowView;
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

        private void ClearWorkIDInput()
        {
            txtScanCardNO.Text = "";
            txtScanName.Text = "";
            txtScanName.Focus();            
        }
                
        #endregion

        //查询空房
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //int dormAreaID = Convert.ToInt32(ViewState["dormAreaID"]);
            int dormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            Bind(1, dormAreaID);
        }
        

        //分配
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

                var sIdCard = this.txtScanCardNO.Text.Trim();
                var sName = this.txtScanName.Text.Trim();
                if (!Util.IsValidIDCard(sIdCard))
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('不是有效的中国身份证！')", true);
                    return;
                }
                if (string.IsNullOrEmpty(sIdCard) || string.IsNullOrEmpty(sName))
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('姓名应当提供！')", true);
                    return;
                }

                //查询人员信息
                var sSex = Util.IsIDCardMan(sIdCard) > 0 ? "男" : "女";
                if (sSex != drAssign["RoomSexType"].ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('身份证的性别与房间的性别不匹配，不能分配！')", true);
                    return;
                }

                string sDormAreaName = drAssign["DormAreaName"].ToString();
                string sBuildingName = drAssign["BuildingName"].ToString();
                string sRoomName = drAssign["RoomName"].ToString();
                string sBedName = drAssign["Name"].ToString();
                
                //检查是否已经有CheckIn的记录
                DataTable DtCheckIDCard = new AssignRoomBLL().GetAssignedData(sIdCard, "");
                if (DtCheckIDCard != null && DtCheckIDCard.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有分配记录！')", true);
                    return;
                }
                else
                {
                    TB_EmployeeCheckIn mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
                    mTB_EmployeeCheckIn.RoomID = Convert.ToInt32(drAssign["RoomID"]);
                    mTB_EmployeeCheckIn.BedID = int.Parse(drAssign["ID"].ToString());
                    mTB_EmployeeCheckIn.BU = string.Empty;
                    //TODO 2018-02-07 
                    //由于 EHR.[Segment] 的ID 与 DormManage.[TB_BU]根本不对应
                    //因此，换房记录无法导出事业部
                    mTB_EmployeeCheckIn.BUID =  0;
                    mTB_EmployeeCheckIn.CardNo = sIdCard;
                    mTB_EmployeeCheckIn.CheckInDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                    mTB_EmployeeCheckIn.Company = string.Empty;
                    mTB_EmployeeCheckIn.EmployeeNo = string.Empty;
                    mTB_EmployeeCheckIn.Name = sName;
                    mTB_EmployeeCheckIn.Sex = drAssign["RoomSexType"].ToString() == "男" ? 1 : 2;
                    mTB_EmployeeCheckIn.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
                    mTB_EmployeeCheckIn.Creator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount);
                    mTB_EmployeeCheckIn.IsActive = (int)TypeManager.IsActive.Valid;
                    mTB_EmployeeCheckIn.Telephone = string.Empty;
                    mTB_EmployeeCheckIn.EmployeeTypeName = string.Empty;
                    var bAssign = new AssignRoomBLL().AssignRoom(mTB_EmployeeCheckIn);

                    if (!bAssign)
                    {
                        var msg = "分配房间失败，请重新分配";
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('" + msg + "')", true);
                        return;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('分配成功！')", true);
                    ClearControl(); 
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('" + ex.Message + "')", true);
            }
        }
        
    }
}