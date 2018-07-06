using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormPersonManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Models;
using DormManage.BLL.UserManage;
using DormManage.Framework.LogManager;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class ChangeRoomRecord : BasePage
    {

        private void Bind(int intCurrentIndex)
        {
            TB_ChangeRoomRecord mTB_ChangeRoomRecord = new TB_ChangeRoomRecord();
            ChangeRoomRecordBLL mChangeRoomRecordBLL = new ChangeRoomRecordBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_ChangeRoomRecord.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_ChangeRoomRecord.CardNo = this.txtScanCardNO.Text.Trim();
            mTB_ChangeRoomRecord.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_ChangeRoomRecord.BU = GetSelectedBu();

            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_ChangeRoomRecord.ChangeRoomDateBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_ChangeRoomRecord.ChangeRoomDateEnd = dtVal;
            }

            dtSource = mChangeRoomRecordBLL.GetPagerData(mTB_ChangeRoomRecord, ref pager);
            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }
        private string GetSelectedBu()
        {
            if (ddlBu.SelectedIndex > 0)
            {
                var item = ddlBu.SelectedItem;
                return item.Text;
            }
            return string.Empty;
        }
        private void BindBu()
        {
            TB_BU mTB_BU = new TB_BU();
            BUBLL mBUBLL = new BUBLL();
            Pager pager = null;
            mTB_BU.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);

            var dtSource = mBUBLL.GetPagerData(mTB_BU, ref pager);
            this.ddlBu.DataValueField = TB_BU.col_ID;
            this.ddlBu.DataTextField = TB_BU.col_Name;
            this.ddlBu.DataSource = dtSource;
            this.ddlBu.DataBind();
            this.ddlBu.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {
                try
                {
                    this.Bind(1);
                    BindBu();
                }
                catch (Exception ex)
                {
                    LogManager.GetInstance().ErrorLog("ChangeRoomRecord::Page_Load", ex);
                }                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bind(1);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            TB_ChangeRoomRecord mTB_ChangeRoomRecord = new TB_ChangeRoomRecord();
            ChangeRoomRecordBLL mChangeRoomRecordBLL = new ChangeRoomRecordBLL();
            mTB_ChangeRoomRecord.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_ChangeRoomRecord.CardNo = this.txtScanCardNO.Text.Trim();
            mTB_ChangeRoomRecord.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_ChangeRoomRecord.BU = GetSelectedBu();
            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_ChangeRoomRecord.ChangeRoomDateBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_ChangeRoomRecord.ChangeRoomDateEnd = dtVal;
            }

            string strFileName = mChangeRoomRecordBLL.Export(mTB_ChangeRoomRecord);
            this.DownLoadFile(this.Request, this.Response, "换房记录.xls", File.ReadAllBytes(strFileName), 10240000);
            //File.Delete(strFileName);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }
    }
}