using DormManage.BLL.FlexPlus;
using DormManage.Framework;
using DormManage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class DormSuggest : BasePage
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
            lst.Add(new TNameVal("已处理", "1"));

            ddlStatus.DataSource = lst;
            ddlStatus.DataValueField = "Value";
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataBind();
            ddlStatus.SelectedIndex = 0;
        }

        private void Bind(int iPage)
        {
            var bll = new FlexPlusBLL();
            var pager = new Pager();
            pager.CurrentPageIndex = iPage;
            pager.srcOrder = " CreateDate desc";
            var mItem = GetParam();
            var dt = bll.GetDormSuggestList(mItem, ref pager);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }
        private TB_DormSuggest GetParam()
        {
            var mItem = new TB_DormSuggest();
            mItem.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(txtSubmitDayBegin.Text.Trim(), out dtVal))
            {
                mItem.SubmitDayBegin = dtVal;
            }
            if (DateTime.TryParse(txtSubmitDayEnd.Text.Trim(), out dtVal))
            {
                mItem.SubmitDayEnd = dtVal;
            }
            return mItem;
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
                    Literal ltStatus = e.Row.Cells[7].FindControl("ltlStatus") as Literal;
                    int.TryParse(dv["Status"].ToString(), out nVal);
                    switch (nVal)
                    {
                        case 0:
                            ltStatus.Text = "<span style='color:#FF9900'>等待处理</span>";
                            break;
                        case 1:
                            ltStatus.Text = "<span style='color:Blue'>已处理</span>";
                            break;
                    }
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //export to excel
            var mItem = GetParam();
            var bll = new FlexPlusBLL();
            var sfn = DateTime.Now.ToString("yyMMddHHmmssms_") + "宿舍建议.xls";
            string strFileName = bll.ExportDormSugget(mItem, sfn);
            DownLoadFile(this.Request, this.Response, sfn, File.ReadAllBytes(strFileName), 10240000);
        }
    }
}