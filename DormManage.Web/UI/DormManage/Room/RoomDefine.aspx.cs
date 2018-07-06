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

namespace DormManage.Web.UI.DormManage.Room
{
    public partial class RoomDefine : BasePage
    {
        #region 私有方法
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="intCurrentIndex"></param>
        private void Bind(int intCurrentIndex)
        {
            TB_Room mTB_Room = new TB_Room();
            RoomBLL mRoomBLL = new RoomBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_Room.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Room.DormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            mTB_Room.BuildingID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_Room.UnitID = Convert.ToInt32(Request.Form[this.ddlUnit.UniqueID.ToString()]);
            mTB_Room.FloorID = Convert.ToInt32(Request.Form[this.ddlFloor.UniqueID.ToString()]);
            mTB_Room.RoomType = Convert.ToInt32(this.ddlRoomType.SelectedValue);
            mTB_Room.RoomSexType = this.ddlRoomSexType.SelectedValue;
            mTB_Room.RoomType2 = string.IsNullOrEmpty(this.ddlRoomType2.SelectedValue) ? 0 : Convert.ToInt32(this.ddlRoomType2.SelectedValue);
            mTB_Room.Name = this.txtRoom.Text.Trim();

            GridView1.DataSource = mRoomBLL.GetTable(mTB_Room, ref pager);
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

            this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID=(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager);
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
        }
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            ClientScript.RegisterForEventValidation(this.ddlBuildingName.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlUnit.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFloor.UniqueID, "argument");
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {                
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
                e.Row.Cells[3].Text = "<a href='javascript:void(0);' onclick='view(" + dv[TB_Room.col_ID] + ");'>" + dv[TB_Room.col_Name].ToString() + "</a>";
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            string strID = this.GetCheckedIDs(this.GridView1, "chkLeftSingle");
            try
            {
                new RoomBLL().Remove(strID);
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
    }
}