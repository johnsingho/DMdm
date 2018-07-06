using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.Room
{
    public partial class NewRoom : BasePage
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
            #region 楼层
            this.ddlFloor.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 房间类型
            RoomTypeBLL mRoomTypeBLL = new RoomTypeBLL();
            this.ddlRoomType.DataValueField = TB_RoomType.col_ID;
            this.ddlRoomType.DataTextField = TB_RoomType.col_Name;
            this.ddlRoomType.DataSource = mRoomTypeBLL.GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            this.ddlRoomType.DataBind();
            this.ddlRoomType.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
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
        /// 绑定楼层
        /// </summary>
        /// <param name="intBuildingID"></param>
        private void BindDDLFloor(int intUnitID)
        {
            if (intUnitID > 0)
            {
                FloorBLL mFloorBLL = new FloorBLL();
                this.ddlFloor.DataValueField = TB_Floor.col_ID;
                this.ddlFloor.DataTextField = TB_Floor.col_Name;
                this.ddlFloor.DataSource = mFloorBLL.GetFloorByUnitID(Convert.ToInt32(this.ddlUnit.SelectedValue));
                this.ddlFloor.DataBind();
                this.ddlFloor.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
            else
            {
                this.ddlFloor.Items.Clear();
                this.ddlFloor.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            }
        }


        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitPage()
        {
            string strID = Request.QueryString["id"];
            int intID;
            TB_Room mTB_Room = null;
            RoomBLL mRoomBLL = new RoomBLL();
            if (Int32.TryParse(strID, out intID))
            {
                mTB_Room = mRoomBLL.Get(intID);
                this.txtRoom.Text = mTB_Room.Name;
                //this.txtKeyCount.Text = mTB_Room.KeyCount.ToString();
                this.ddlDormArea.SelectedValue = mTB_Room.DormAreaID.ToString();

                this.BindDDLBuilding(mTB_Room.DormAreaID);
                this.ddlBuilding.SelectedValue = mTB_Room.BuildingID.ToString();

                this.BindDDLUnit(mTB_Room.BuildingID);
                this.ddlUnit.SelectedValue = mTB_Room.UnitID.ToString();

                this.ddlRoomType.SelectedValue = mTB_Room.RoomType.ToString();

                this.ddlRoomSexType.SelectedValue = mTB_Room.RoomSexType.ToString();

                this.BindDDLFloor(mTB_Room.UnitID);
                this.ddlFloor.SelectedValue = mTB_Room.FloorID.ToString();

                this.ddlRoomType2.SelectedValue = mTB_Room.RoomType2.ToString();
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