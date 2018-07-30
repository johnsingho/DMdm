using DormManage.BLL.DormManage;
using DormManage.BLL.FlexPlus;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class ApplyDormList : BasePage
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
            mTB_DormAreaApply.EmployeeNo = txtWorkDayNo.Text.Trim();
            mTB_DormAreaApply.CName = txtName.Text.Trim();
            mTB_DormAreaApply.CardNo = txtScanCardNO.Text.Trim();
            int nVal = 0;
            int.TryParse(ddlRequiredType.SelectedValue, out nVal);
            mTB_DormAreaApply.RequireType = nVal;
            nVal = -1;
            int.TryParse(ddlDormArea.SelectedValue, out nVal);
            mTB_DormAreaApply.DormAreaID = nVal;
            nVal = -1;
            int.TryParse(ddlStatus.SelectedValue, out nVal);
            mTB_DormAreaApply.Status = nVal;

            var dt = bll.GetApplyDorms(mTB_DormAreaApply, ref pager);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
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

            Pager mPager = null;
            #region 宿舍区
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormArea.DataValueField = TB_DormArea.col_ID;
            this.ddlDormArea.DataTextField = TB_DormArea.col_Name;

            this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager);
            this.ddlDormArea.DataBind();
            this.ddlDormArea.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
        }

        protected void pagerList_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int dormAreaID = Convert.ToInt32(ViewState["dormAreaID"]);
            this.BindApplys(Convert.ToInt32(e.CommandArgument));
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var dv = e.Row.DataItem as DataRowView;
                    Literal ltlSex = e.Row.Cells[3].FindControl("ltlSex") as Literal;
                    if (Convert.ToInt32(dv["Sex"]) == (int)TypeManager.Sex.Male)
                    {
                        ltlSex.Text = RemarkAttribute.GetEnumRemark(TypeManager.Sex.Male);
                    }
                    else
                    {
                        ltlSex.Text = RemarkAttribute.GetEnumRemark(TypeManager.Sex.Female);
                    }

                    int nVal = 0;
                    Literal ltlRequireType = e.Row.Cells[8].FindControl("ltlRequireType") as Literal;
                    int.TryParse(dv["RequireType"].ToString(), out nVal);
                    switch (nVal)
                    {
                        case 1:
                            ltlRequireType.Text = "新入住";
                            break;
                        case 2:
                            ltlRequireType.Text = "复入住";
                            break;
                        case 3:
                            ltlRequireType.Text = "调房";
                            break;
                    }

                    nVal = -1;
                    Literal ltlHasHousingAllowance = e.Row.Cells[8].FindControl("ltlHasHousingAllowance") as Literal;
                    int.TryParse(dv["HasHousingAllowance"].ToString(), out nVal);
                    ltlHasHousingAllowance.Text = (nVal > 0) ? "是" : "否";

                    nVal = -1;
                    Literal ltStatus = e.Row.Cells[12].FindControl("ltlStatus") as Literal;
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

                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("ApplyDorm::GridView1_RowDataBound", ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindApplys(1);
        }

    }
}