using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.Building
{
    public partial class NewBuilding : BasePage
    {
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind()
        {
            Pager mPager = null;
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormAreaName.DataValueField = TB_DormArea.col_ID;
            this.ddlDormAreaName.DataTextField = TB_DormArea.col_Name;

            this.ddlDormAreaName.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager); ;
            this.ddlDormAreaName.DataBind();
            this.ddlDormAreaName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlBind();
                string strID = Request.QueryString["id"];
                int intID;
                TB_Building mTB_Building = null;
                BuildingBLL mBuildingBLL = new BuildingBLL();
                if (Int32.TryParse(strID, out intID))
                {
                    mTB_Building = mBuildingBLL.Get(intID);
                    this.txtBuildingName.Text = mTB_Building.Name;
                    this.ddlDormAreaName.SelectedValue = mTB_Building.DormAreaID.ToString();
                }
            }
        }
    }
}