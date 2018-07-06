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
    public partial class UserList : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_User mTB_User = new TB_User();
            UserBLL mUserBLL = new UserBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_User.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_User.ADAccount = this.txtAdAccount.Text.Trim();
            mTB_User.EmployeeNo = this.txtEmployeeNo.Text.Trim();
            mTB_User.EName = this.txtEName.Text.Trim();
            mTB_User.CName = this.txtCName.Text.Trim();

            dtSource = mUserBLL.GetPagerData(mTB_User, ref pager);
            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            #region 新版本
            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
            #endregion

            #region 旧版本
            //Pager pager1 = null;
            //this.Pager1.ItemCount = mUserBLL.GetPagerData(mTB_User, ref pager1).Rows.Count;
            //this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            //this.Pager1.PageSize = pager.PageSize;

            //if (this.Pager1.ItemCount % this.Pager1.PageSize == 0)
            //{
            //    this.Pager1.PageCount = (int)this.Pager1.ItemCount / this.Pager1.PageSize;
            //}
            //else
            //{
            //    this.Pager1.PageCount = (int)(this.Pager1.ItemCount / this.Pager1.PageSize + 1);
            //}
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
                if (!IsPostBack)
                {
                    this.Bind(1);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("UserList::Page_Load", ex);
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
            string strID = this.GetCheckedIDs(this.GridView1, "chkLeftSingle");
            try
            {
                //new BedBLL().Remove(strID);
                new UserBLL().Remove(strID);
                this.Bind(1);
            }
            catch
            {

            }
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
                    e.Row.Cells[1].Text = "<a href='javascript:void(0);' onclick='view(" + dv[TB_User.col_ID] + ");'>" + dv[TB_User.col_ADAccount].ToString() + "</a>";
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }
    }
}