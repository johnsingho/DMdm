using DormManage.BLL.FlexPlus;
using DormManage.Framework;
using DormManage.Models;
using System;
using System.Collections.Generic;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class ApplyDorm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSelect();
                BindApplys(1);
            }
        }

        private void BindApplys(int curPageIndex)
        {
            var bll  = new FlexPlusBLL();
            var pager = new Pager();
            pager.CurrentPageIndex = curPageIndex;
            pager.srcOrder = "  ID desc";

            var mTB_DormAreaApply = new TB_DormAreaApply();
            //
            var dt = bll.GetApplyDorms(mTB_DormAreaApply, ref pager);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        private class TNameVal
        {
            public TNameVal(string n, string v)
            {
                Name = n;
                Value = v;
            }
            public string Name { get; set; }
            public string Value { get; set; }
        }
        private void BindSelect()
        {
            var lst = new List<TNameVal>();
            lst.Add(new TNameVal("All", "0"));
            lst.Add(new TNameVal("新入住", "1"));
            lst.Add(new TNameVal("复入住", "2"));
            lst.Add(new TNameVal("调房", "3"));
            ddlRequiredType.DataSource = lst;
            ddlRequiredType.DataValueField = "Value";
            ddlRequiredType.DataTextField = "Name";
            ddlRequiredType.DataBind();
            ddlRequiredType.SelectedIndex = 0;

            lst = new List<TNameVal>();
            lst.Add(new TNameVal("All", "-1"));
            lst.Add(new TNameVal("等待处理", "0"));
            lst.Add(new TNameVal("批准", "1"));
            lst.Add(new TNameVal("拒绝", "2"));
            ddlStatus.DataSource = lst;
            ddlStatus.DataValueField = "Value";
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataBind();
            ddlStatus.SelectedIndex = 0;
        }

        protected void pagerList_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int dormAreaID = Convert.ToInt32(ViewState["dormAreaID"]);
            this.BindApplys(Convert.ToInt32(e.CommandArgument));
        }
    }
}