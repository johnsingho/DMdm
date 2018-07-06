using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.UserManage;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.Web.UI.UserManage
{
    public partial class BUList : BasePage
    {

        private void Bind(int intCurrentIndex)
        {
            TB_BU mTB_BU = new TB_BU();
            BUBLL mBUBLL = new BUBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID asc";

            mTB_BU.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_BU.Name = this.txtBU.Text.Trim();

            dtSource = mBUBLL.GetPagerData(mTB_BU, ref pager);
            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Bind(1);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bind(1);
        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = e.Row.DataItem as DataRowView;
                e.Row.Cells[1].Text = "<a href='javascript:void(0);' onclick='view(" + dv[TB_BU.col_ID] + ");'>" + dv[TB_BU.col_Name].ToString() + "</a>";
            }
        }
    }
}