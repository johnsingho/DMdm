using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormPersonManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class CheckOut : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_EmployeeCheckIn mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
            EmployeeCheckInBLL mEmployeeCheckInBLL = new EmployeeCheckInBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_EmployeeCheckIn.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_EmployeeCheckIn.CardNo = this.txtScanCardNO.Text.Trim();
            mTB_EmployeeCheckIn.Name = this.txtName.Text.Trim();
            mTB_EmployeeCheckIn.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckIn.CheckInDateBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckIn.CheckInDateEnd = dtVal;
            }

            dtSource = mEmployeeCheckInBLL.GetPagerData(mTB_EmployeeCheckIn, ref pager);

            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
            try
            {                
                if (!IsPostBack)
                {                    
                    this.Bind(1);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("CheckOut::Page_Load", ex);
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
                LogManager.GetInstance().ErrorLog("CheckOut::btnSearch_Click", ex);
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
                LogManager.GetInstance().ErrorLog("CheckOut::pagerList_Command", ex);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dv = e.Row.DataItem as DataRowView;
                    Literal ltlSex = e.Row.Cells[3].FindControl("ltlSex") as Literal;
                    if (Convert.ToInt32(dv[TB_EmployeeCheckIn.col_Sex]) == (int)TypeManager.Sex.Male)
                    {
                        ltlSex.Text = RemarkAttribute.GetEnumRemark(TypeManager.Sex.Male);
                    }
                    else if (Convert.ToInt32(dv[TB_EmployeeCheckIn.col_Sex]) == (int)TypeManager.Sex.Female)
                    {
                        ltlSex.Text = RemarkAttribute.GetEnumRemark(TypeManager.Sex.Female);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("CheckOut::GridView1_RowDataBound", ex);
            }
        }
    }
}