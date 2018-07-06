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

namespace DormManage.Web.UI.DormManage.Building
{
    public partial class BuildingDefine : BasePage
    {
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="intCurrentIndex"></param>
        private void Bind(int intCurrentIndex)
        {
            TB_Building mTB_Building = new TB_Building();
            BuildingBLL mBuildingBLL = new BuildingBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  DormAreaID desc";

            mTB_Building.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Building.Name = this.txtBuildingName.Text;
            mTB_Building.DormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            GridView1.DataSource = mBuildingBLL.GetTable(mTB_Building, ref pager);
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
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormArea.DataValueField = TB_DormArea.col_ID;
            this.ddlDormArea.DataTextField = TB_DormArea.col_Name;
            this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() {SiteID=(base.UserInfo==null?base.SystemAdminInfo.SiteID:base.UserInfo.SiteID) }, ref mPager);
            this.ddlDormArea.DataBind();
            this.ddlDormArea.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
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
                e.Row.Cells[2].Text = "<a href='javascript:void(0);' onclick='view(" + dv[TB_Building.col_ID] + ");'>" + dv[TB_Building.col_Name].ToString() + "</a>";
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            string strID = this.GetCheckedIDs(this.GridView1, "chkLeftSingle");
            try
            {
                new BuildingBLL().Remove(strID);
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