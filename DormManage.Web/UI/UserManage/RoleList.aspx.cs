using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.UserManage;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;

namespace DormManage.Web.UI.UserManage
{
    public partial class RoleList : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_Role mTB_Role = new TB_Role();
            RoleBLL mRoleBLL = new RoleBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_Role.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Role.Name = this.txtRoleName.Text.Trim();

            dtSource = mRoleBLL.GetPagerData(mTB_Role, ref pager);
            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.Bind(1);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("RoleList::Page_Load", ex);
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
                    e.Row.Cells[1].Text = "<a href='javascript:void(0);' onclick='view(" + dv[TB_Role.col_ID] + ");'>" + dv[TB_Role.col_Name].ToString() + "</a>";
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }
    }
}