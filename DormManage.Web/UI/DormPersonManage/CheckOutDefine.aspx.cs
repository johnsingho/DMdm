using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using DormManage.BLL.DormPersonManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;
using DormManage.BLL.DormManage;
using DormManage.BLL.UserManage;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class CheckOutDefine : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_EmployeeCheckOut mTB_EmployeeCheckOut = new TB_EmployeeCheckOut();
            EmployeeCheckOutBLL mEmployeeCheckOutBLL = new EmployeeCheckOutBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_EmployeeCheckOut.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_EmployeeCheckOut.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_EmployeeCheckOut.Name = this.txtName.Text.Trim();
            mTB_EmployeeCheckOut.CardNo = this.txtScanCardNO.Text.Trim();

            mTB_EmployeeCheckOut.BUID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_EmployeeCheckOut.BU = GetSelectedBu(); //this.txtBu.Text.Trim();

            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckOut.CheckOutDateBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckOut.CheckOutDateEnd = dtVal;
            }

            int iDormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            int iRoomTypeID = Convert.ToInt32(this.ddlRoomType.SelectedValue);
            string sRoomName = this.txtRoom.Text.Trim();

            dtSource = mEmployeeCheckOutBLL.GetPagerData(mTB_EmployeeCheckOut, iDormAreaID, iRoomTypeID, sRoomName, ref pager);


            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind()
        {
            Pager mPager = null;
            #region 宿舍区
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormArea.DataValueField = TB_DormArea.col_ID;
            this.ddlDormArea.DataTextField = TB_DormArea.col_Name;

            this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager);
            this.ddlDormArea.DataBind();
            this.ddlDormArea.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼栋
            this.ddlBuildingName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 房间类型
            RoomTypeBLL mRoomTypeBLL = new RoomTypeBLL();
            this.ddlRoomType.DataValueField = TB_RoomType.col_ID;
            this.ddlRoomType.DataTextField = TB_RoomType.col_Name;
            this.ddlRoomType.DataSource = mRoomTypeBLL.GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            this.ddlRoomType.DataBind();
            this.ddlRoomType.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion

            BindBu();
        }

        private string GetSelectedBu()
        {
            if (ddlBu.SelectedIndex>0)
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
            try
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
                if (!IsPostBack)
                {
                    ddlBind();
                    this.Bind(1);                  
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("CheckOutDefine::Page_Load", ex);
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
                LogManager.GetInstance().ErrorLog("CheckOutDefine::btnSearch_Click", ex);
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
                LogManager.GetInstance().ErrorLog("CheckOutDefine::pagerList_Command", ex);
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
                LogManager.GetInstance().ErrorLog("CheckOutDefine::GridView1_RowDataBound", ex);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            TB_EmployeeCheckOut mTB_EmployeeCheckOut = new TB_EmployeeCheckOut();
            mTB_EmployeeCheckOut.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_EmployeeCheckOut.EmployeeNo = this.txtWorkDayNo.Text.Trim();
            mTB_EmployeeCheckOut.Name = this.txtName.Text.Trim();
            mTB_EmployeeCheckOut.CardNo = this.txtScanCardNO.Text.Trim();

            mTB_EmployeeCheckOut.BUID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_EmployeeCheckOut.RoomID= Convert.ToInt32(this.ddlRoomType.SelectedValue);
            mTB_EmployeeCheckOut.BU = GetSelectedBu(); // this.txtBu.Text.Trim();
            DateTime dtVal = DateTime.Now;
            if (DateTime.TryParse(this.txtStartDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckOut.CheckOutDateBegin = dtVal;
            }
            if (DateTime.TryParse(this.txtEndDay.Text.Trim(), out dtVal))
            {
                mTB_EmployeeCheckOut.CheckOutDateEnd = dtVal;
            }

            int iDormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            string strFileName = new EmployeeCheckOutBLL().Export(mTB_EmployeeCheckOut, iDormAreaID);
            this.DownLoadFile(this.Request, this.Response, "退房记录.xls", File.ReadAllBytes(strFileName), 10240000);
            //File.Delete(strFileName);
        }
    }
}