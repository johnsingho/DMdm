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

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class ChooseBed : BasePage
    {
        #region 私有方法
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="intCurrentIndex"></param>
        private void Bind(int intCurrentIndex)
        {
            TB_Bed mTB_Bed = new TB_Bed();
            BedBLL mBedBLL = new BedBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_Bed.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Bed.DormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            mTB_Bed.BuildingID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_Bed.UnitID = Convert.ToInt32(Request.Form[this.ddlUnit.UniqueID.ToString()]);
            mTB_Bed.FloorID = Convert.ToInt32(Request.Form[this.ddlFloor.UniqueID.ToString()]);
            mTB_Bed.RoomID = Convert.ToInt32(Request.Form[this.ddlRoom.UniqueID.ToString()]);
            mTB_Bed.Status = (int)TypeManager.BedStatus.Free;
            //mTB_Bed.RoomType = Convert.ToInt32(this.ddlRoomType.SelectedValue);
            //mTB_Bed.RoomSexType = this.ddlRoomSexType.SelectedValue;
            mTB_Bed.Name = this.txtBed.Text.Trim();

            GridView1.DataSource = mBedBLL.GetTableByEnableStatus(mTB_Bed, ref pager);
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
            //RoomTypeBLL mRoomTypeBLL = new RoomTypeBLL();
            //this.ddlRoomType.DataValueField = TB_RoomType.col_ID;
            //this.ddlRoomType.DataTextField = TB_RoomType.col_Name;

            //this.ddlRoomType.DataSource = mRoomTypeBLL.GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            //this.ddlRoomType.DataBind();
            //this.ddlRoomType.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
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
                this.ddlBind();
                this.Bind(1);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bind(1);
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
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }
    }
}