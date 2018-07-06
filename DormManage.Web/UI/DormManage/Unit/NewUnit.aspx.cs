using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.Unit
{
    public partial class NewUnit : BasePage
    {
        /// <summary>
        /// 绑定宿舍区
        /// </summary>
        private void ddlBind()
        {
            Pager mPager = null;
            #region 宿舍区
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormAreaName.DataValueField = TB_DormArea.col_ID;
            this.ddlDormAreaName.DataTextField = TB_DormArea.col_Name;

            this.ddlDormAreaName.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager); ;
            this.ddlDormAreaName.DataBind();
            this.ddlDormAreaName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼栋
            this.ddlBuildingName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
        }

        /// <summary>
        /// 绑定楼栋
        /// </summary>
        /// <param name="intDormAreaID"></param>
        private void BindDDLBuilding(int intDormAreaID)
        {
            if (intDormAreaID > 0)
            {
                BuildingBLL mBuildingBLL = new BuildingBLL();
                this.ddlBuildingName.DataValueField = TB_Building.col_ID;
                this.ddlBuildingName.DataTextField = TB_Building.col_Name;
                this.ddlBuildingName.DataSource = mBuildingBLL.GetBuildingByDormAreaID(Convert.ToInt32(this.ddlDormAreaName.SelectedValue));
                this.ddlBuildingName.DataBind();
                this.ddlBuildingName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
            else
            {
                this.ddlBuildingName.Items.Clear();
                this.ddlBuildingName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlBind();
                string strID = Request.QueryString["id"];
                int intID;
                TB_Unit mTB_Unit = null;
                UnitBLL mUnitBLL = new UnitBLL();
                if (Int32.TryParse(strID, out intID))
                {
                    mTB_Unit = mUnitBLL.Get(intID);
                    this.txtUnitName.Text = mTB_Unit.Name;
                    this.ddlDormAreaName.SelectedValue = mTB_Unit.DormAreaID.ToString();
                    this.BindDDLBuilding(mTB_Unit.DormAreaID);
                    this.ddlBuildingName.SelectedValue = mTB_Unit.BuildingID.ToString();
                }
            }
        }

        protected void ddlDormAreaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindDDLBuilding(Convert.ToInt32(this.ddlDormAreaName.SelectedValue));
        }
    }
}