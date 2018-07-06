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

namespace DormManage.Web.UI.DormManage.DormArea
{
    public partial class DormAreaDefine : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_DormArea mTB_DormArea = new TB_DormArea();
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_DormArea.Name = txtDormAreaName.Text;
            mTB_DormArea.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            GridView1.DataSource = mDormAreaBLL.GetTable(mTB_DormArea, ref pager);
            GridView1.DataBind();

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
                if (base.SystemAdminInfo == null)
                {
                    this.btnNew.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bind(1);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            string strID = this.GetCheckedIDs(this.GridView1, "chkLeftSingle");
            try
            {
                new DormAreaBLL().Remove(strID);
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = e.Row.DataItem as DataRowView;
                e.Row.Cells[1].Text = "<a href='javascript:void(0);' onclick='view(" + dv[TB_DormArea.col_ID] + ");'>" + dv[TB_DormArea.col_Name].ToString() + "</a>";
            }
        }
    }
}