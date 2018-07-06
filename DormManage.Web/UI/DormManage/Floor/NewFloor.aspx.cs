using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.Floor
{
    public partial class NewFloor : BasePage
    {
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
            this.ddlBuilding.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 单元
            this.ddlUnit.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
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
                this.ddlBuilding.DataValueField = TB_Building.col_ID;
                this.ddlBuilding.DataTextField = TB_Building.col_Name;
                this.ddlBuilding.DataSource = mBuildingBLL.GetBuildingByDormAreaID(Convert.ToInt32(this.ddlDormArea.SelectedValue));
                this.ddlBuilding.DataBind();
                this.ddlBuilding.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
            else
            {
                this.ddlBuilding.Items.Clear();
                this.ddlBuilding.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
        }

        /// <summary>
        /// 绑定单元
        /// </summary>
        /// <param name="intBuildingID"></param>
        private void BindDDLUnit(int intBuildingID)
        {
            if (intBuildingID > 0)
            {
                UnitBLL mUnitBLL = new UnitBLL();
                this.ddlUnit.DataValueField = TB_Unit.col_ID;
                this.ddlUnit.DataTextField = TB_Unit.col_Name;
                this.ddlUnit.DataSource = mUnitBLL.GetUnitByBuildingID(Convert.ToInt32(this.ddlBuilding.SelectedValue));
                this.ddlUnit.DataBind();
                this.ddlUnit.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
            else
            {
                this.ddlUnit.Items.Clear();
                this.ddlUnit.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
        }


        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitPage()
        {
            string strID = Request.QueryString["id"];
            int intID;
            TB_Floor mTB_Floor = null;
            FloorBLL mFloorBLL = new FloorBLL();
            if (Int32.TryParse(strID, out intID))
            {
                mTB_Floor = mFloorBLL.Get(intID);
                this.txtFloor.Text = mTB_Floor.Name;
                this.ddlDormArea.SelectedValue = mTB_Floor.DormAreaID.ToString();
                this.BindDDLBuilding(mTB_Floor.DormAreaID);
                this.ddlBuilding.SelectedValue = mTB_Floor.BuildingID.ToString();
                this.BindDDLUnit(mTB_Floor.BuildingID);
                this.ddlUnit.SelectedValue = mTB_Floor.UnitID.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {                
                this.ddlBind();
                this.InitPage();
            }
        }
    }
}