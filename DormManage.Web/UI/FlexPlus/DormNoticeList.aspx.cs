using DormManage.BLL.FlexPlus;
using DormManage.Framework;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class DormNoticeList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {
                this.Bind(1);
            }
        }

        private void Bind(int iPage)
        {
            var bll = new FlexPlusBLL();
            var pager = new Pager();
            pager.CurrentPageIndex = iPage;
            pager.srcOrder = " CreateDate desc";
            var dt = bll.GetDormNotice(ref pager);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var dv = e.Row.DataItem as DataRowView;                 

                    int nVal = 0;
                    Literal ltStatus = e.Row.Cells[4].FindControl("ltlStatus") as Literal;
                    int.TryParse(dv["IsDelete"].ToString(), out nVal);
                    switch (nVal)
                    {
                        case 1:
                            ltStatus.Text = "<span style='color:Blue'>启用</span>";
                            break;
                        default:
                            ltStatus.Text = "<span style='color:Red'>禁用</span>";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}