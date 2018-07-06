using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.Floor
{
    public partial class FloorDefine : BasePage
    {
        #region 私有方法
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="intCurrentIndex"></param>
        private void Bind(int intCurrentIndex)
        {
            TB_Floor mTB_Floor = new TB_Floor();
            FloorBLL mFloorBLL = new FloorBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_Floor.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Floor.Name = this.txtFloorName.Text.Trim();
            mTB_Floor.DormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            mTB_Floor.BuildingID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_Floor.UnitID = Convert.ToInt32(Request.Form[this.ddlUnit.UniqueID.ToString()]);
            GridView1.DataSource = mFloorBLL.GetTable(mTB_Floor, ref pager);
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
        }
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            ClientScript.RegisterForEventValidation(this.ddlBuildingName.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlUnit.UniqueID, "argument");
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

        protected void btnNew_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = e.Row.DataItem as DataRowView;
                e.Row.Cells[4].Text = "<a href='javascript:void(0);' onclick='view(" + dv[TB_Floor.col_ID] + ");'>" + dv[TB_Floor.col_Name].ToString() + "</a>";
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }
    }
}