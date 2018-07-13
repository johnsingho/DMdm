using DormManage.BLL.FlexPlus;
using DormManage.Framework;
using DormManage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class ReissueKeyList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {
                this.BindSelect();
                this.Bind(1);
            }
        }

        private void BindSelect()
        {
            var lst = new List<TNameVal>();
            lst.Add(new TNameVal("All", "-1"));
            lst.Add(new TNameVal("等待处理", "0"));
            lst.Add(new TNameVal("批准", "1"));
            lst.Add(new TNameVal("拒绝", "2"));
            ddlStatus.DataSource = lst;
            ddlStatus.DataValueField = "Value";
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataBind();
            ddlStatus.SelectedIndex = 0;

            // app的钥匙类型对应
            lst = new List<TNameVal>();
            lst.Add(new TNameVal("All", "All"));
            lst.Add(new TNameVal("大门钥匙", "大门钥匙"));
            lst.Add(new TNameVal("衣柜外门", "衣柜外门"));
            lst.Add(new TNameVal("衣柜抽屉", "衣柜抽屉"));
            ddlKeyType.DataSource = lst;
            ddlKeyType.DataValueField = "Value";
            ddlKeyType.DataTextField = "Name";
            ddlKeyType.DataBind();
            ddlKeyType.SelectedIndex = 0;
        }

        private void Bind(int iPage)
        {
            var bll = new FlexPlusBLL();
            var pager = new Pager();
            pager.CurrentPageIndex = iPage;
            pager.srcOrder = " CreateDate desc";
            var mItem = new TB_DormReissueKey();

            var sSel = ddlKeyType.SelectedItem.Text;
            if(0!=string.Compare("all", sSel, true))
            {
                mItem.KeyTypes = sSel;
            }            
            mItem.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            var dt = bll.GetReissueKeyList(mItem, ref pager);
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
                    Literal ltStatus = e.Row.Cells[10].FindControl("ltlStatus") as Literal;
                    int.TryParse(dv["Status"].ToString(), out nVal);
                    switch (nVal)
                    {
                        case 0:
                            ltStatus.Text = "<span style='color:#FF9900'>等待处理</span>";
                            break;
                        case 1:
                            ltStatus.Text = "<span style='color:Blue'>批准</span>";
                            break;
                        default:
                            ltStatus.Text = "<span style='color:Red'>拒绝</span>";
                            break;
                    }

                    //总费用 
                    Literal ltlMoney = e.Row.Cells[6].FindControl("ltlMoney") as Literal;
                    ltlMoney.Text = string.Format("<span style='color:Red'>{0}</span>", dv["Money"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bind(1);
        }

    }
}