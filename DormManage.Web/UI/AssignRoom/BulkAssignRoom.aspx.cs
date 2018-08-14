using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.AssignRoom;
using DormManage.BLL.DormManage;
using DormManage.BLL.DormPersonManage;
using DormManage.BLL.UserManage;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Models;
using DormManage.BLL;

namespace DormManage.Web.UI.AssignRoom
{
    public partial class BulkAssignRoom : BasePage
    {

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind()
        {
            #region 宿舍区
            this.ddlDormArea0.DataValueField = "DormAreaID";
            this.ddlDormArea0.DataTextField = "DormAreaName";
            DataTable dt = ViewState["dtAssignedRoom"] as DataTable;
            IEnumerable<object> dataSource = (from v in dt.AsEnumerable()
                                              select new
                                              {
                                                  DormAreaID = v.Field<int>("DormAreaID"),
                                                  DormAreaName = v.Field<string>("DormAreaName")
                                              }).ToList().Distinct();
            this.ddlDormArea0.DataSource = dataSource;
            this.ddlDormArea0.DataBind();
            this.ddlDormArea0.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            foreach (var item in this.ddlDormArea0.Items)
            {
                this.ddlDormArea1.Items.Add((ListItem)item);
                this.ddlDormArea2.Items.Add((ListItem)item);
                this.ddlDormArea3.Items.Add((ListItem)item);
                this.ddlDormArea4.Items.Add((ListItem)item);
                this.ddlDormArea5.Items.Add((ListItem)item);
                this.ddlDormArea6.Items.Add((ListItem)item);
                this.ddlDormArea7.Items.Add((ListItem)item);
                this.ddlDormArea8.Items.Add((ListItem)item);
                this.ddlDormArea9.Items.Add((ListItem)item);

                this.ddlFemaleDormArea0.Items.Add((ListItem)item);
                this.ddlFemaleDormArea1.Items.Add((ListItem)item);
                this.ddlFemaleDormArea2.Items.Add((ListItem)item);
                this.ddlFemaleDormArea3.Items.Add((ListItem)item);
                this.ddlFemaleDormArea4.Items.Add((ListItem)item);
                this.ddlFemaleDormArea5.Items.Add((ListItem)item);
                this.ddlFemaleDormArea6.Items.Add((ListItem)item);
                this.ddlFemaleDormArea7.Items.Add((ListItem)item);
                this.ddlFemaleDormArea8.Items.Add((ListItem)item);
                this.ddlFemaleDormArea9.Items.Add((ListItem)item);
            }
            #endregion
            #region 楼栋
            this.ddlBuilding0.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding1.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding2.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding3.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding4.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding5.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding6.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding7.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding8.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlBuilding9.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });

            this.ddlFemaleBuilding0.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding1.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding2.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding3.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding4.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding5.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding6.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding7.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding8.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            this.ddlFemaleBuilding9.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 单元
            //this.ddlUnit.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼层
            //this.ddlFloor.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 房间类型
            RoomTypeBLL mRoomTypeBLL = new RoomTypeBLL();
            this.ddlRoomType0.DataValueField = TB_RoomType.col_ID;
            this.ddlRoomType0.DataTextField = TB_RoomType.col_Name;
            this.ddlRoomType0.DataSource = mRoomTypeBLL.GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            this.ddlRoomType0.DataBind();
            this.ddlRoomType0.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            foreach (var item in this.ddlRoomType0.Items)
            {
                this.ddlRoomType1.Items.Add((ListItem)item);
                this.ddlRoomType2.Items.Add((ListItem)item);
                this.ddlRoomType3.Items.Add((ListItem)item);
                this.ddlRoomType4.Items.Add((ListItem)item);
                this.ddlRoomType5.Items.Add((ListItem)item);
                this.ddlRoomType6.Items.Add((ListItem)item);
                this.ddlRoomType7.Items.Add((ListItem)item);
                this.ddlRoomType8.Items.Add((ListItem)item);
                this.ddlRoomType9.Items.Add((ListItem)item);

                this.ddlFemaleRoomType0.Items.Add((ListItem)item);
                this.ddlFemaleRoomType1.Items.Add((ListItem)item);
                this.ddlFemaleRoomType2.Items.Add((ListItem)item);
                this.ddlFemaleRoomType3.Items.Add((ListItem)item);
                this.ddlFemaleRoomType4.Items.Add((ListItem)item);
                this.ddlFemaleRoomType5.Items.Add((ListItem)item);
                this.ddlFemaleRoomType6.Items.Add((ListItem)item);
                this.ddlFemaleRoomType7.Items.Add((ListItem)item);
                this.ddlFemaleRoomType8.Items.Add((ListItem)item);
                this.ddlFemaleRoomType9.Items.Add((ListItem)item);
            }
            #endregion
            #region BU
            this.ddlBU0.DataTextField = "Name";
            this.ddlBU0.DataValueField = "Name";
            this.ddlBU0.DataSource = new BUBLL().GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            this.ddlBU0.DataBind();
            this.ddlBU0.Items.Insert(0, new ListItem() { Value = "", Text = "--请选择--" });
            foreach (ListItem item in ddlBU0.Items)
            {
                this.ddlBU1.Items.Add(item);
                this.ddlBU2.Items.Add(item);
                this.ddlBU3.Items.Add(item);
                this.ddlBU4.Items.Add(item);
                this.ddlBU5.Items.Add(item);
                this.ddlBU6.Items.Add(item);
                this.ddlBU7.Items.Add(item);
                this.ddlBU8.Items.Add(item);
                this.ddlBU9.Items.Add(item);

                this.ddlFemaleBU0.Items.Add(item);
                this.ddlFemaleBU1.Items.Add(item);
                this.ddlFemaleBU2.Items.Add(item);
                this.ddlFemaleBU3.Items.Add(item);
                this.ddlFemaleBU4.Items.Add(item);
                this.ddlFemaleBU5.Items.Add(item);
                this.ddlFemaleBU6.Items.Add(item);
                this.ddlFemaleBU7.Items.Add(item);
                this.ddlFemaleBU8.Items.Add(item);
                this.ddlFemaleBU9.Items.Add(item);
            }
            #endregion
        }


        private void CacheData()
        {
            Pager pager = null;
            //已经锁定的房间数
            DataTable dtAssignedRoom = new AssignRoomBLL().GetPagerData(ref pager, string.Empty, string.Empty);
            ViewState["dtAssignedRoom"] = dtAssignedRoom;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            ClientScript.RegisterForEventValidation(this.ddlBuilding0.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding1.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding2.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding3.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding4.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding5.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding6.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding7.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding8.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlBuilding9.UniqueID, "argument");

            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding0.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding1.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding2.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding3.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding4.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding5.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding6.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding7.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding8.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFemaleBuilding9.UniqueID, "argument");
            base.Render(writer);
        }

        private void RegisterEvent()
        {
            this.btnCheck0.Click += new EventHandler(btnCheck_Click);
            this.btnCheck1.Click += new EventHandler(btnCheck_Click);
            this.btnCheck2.Click += new EventHandler(btnCheck_Click);
            this.btnCheck3.Click += new EventHandler(btnCheck_Click);
            this.btnCheck4.Click += new EventHandler(btnCheck_Click);
            this.btnCheck5.Click += new EventHandler(btnCheck_Click);
            this.btnCheck6.Click += new EventHandler(btnCheck_Click);
            this.btnCheck7.Click += new EventHandler(btnCheck_Click);
            this.btnCheck8.Click += new EventHandler(btnCheck_Click);
            this.btnCheck9.Click += new EventHandler(btnCheck_Click);

            this.btnFemaleCheck0.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck1.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck2.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck3.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck4.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck5.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck6.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck7.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck8.Click += new EventHandler(btnCheck_Click);
            this.btnFemaleCheck9.Click += new EventHandler(btnCheck_Click);

            this.ddlDormArea0.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea1.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea2.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea3.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea4.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea5.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea6.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea7.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea8.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlDormArea9.Attributes.Add("onchange", "bindBuilding(this)");

            this.ddlFemaleDormArea0.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea1.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea2.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea3.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea4.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea5.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea6.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea7.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea8.Attributes.Add("onchange", "bindBuilding(this)");
            this.ddlFemaleDormArea9.Attributes.Add("onchange", "bindBuilding(this)");
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Button btnCheck = (Button)sender;
            DataTable dtAssignedRoom = ViewState["dtAssignedRoom"] as DataTable;
            DataRow[] drAssignedRoom = null;//剩余的床位数
            switch (btnCheck.ID)
            {
                case "btnCheck0":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea0.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding0.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType0.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck1":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea1.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding1.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType1.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck2":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea2.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding2.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType2.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck3":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea3.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding3.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType3.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck4":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea4.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding4.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType4.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck5":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea5.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding5.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType5.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck6":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea6.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding6.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType6.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck7":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea7.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding7.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType7.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck8":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea8.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding8.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType8.SelectedValue + "  and RoomSexType='男'");
                    break;
                case "btnCheck9":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlDormArea9.SelectedValue + " and BuildingID=" + Request.Form[this.ddlBuilding9.UniqueID.ToString()] + " and RoomType=" + this.ddlRoomType9.SelectedValue + "  and RoomSexType='男'");
                    break;

                case "btnFemaleCheck0":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea0.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding0.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType0.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck1":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea1.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding1.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType1.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck2":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea2.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding2.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType2.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck3":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea3.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding3.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType3.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck4":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea4.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding4.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType4.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck5":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea5.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding5.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType5.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck6":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea6.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding6.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType6.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck7":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea7.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding7.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType7.SelectedValue + "  and RoomSexType='女'");
                    break;
                case "btnFemaleCheck8":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea8.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding8.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType8.SelectedValue + " and RoomSexType='女'");
                    break;
                case "btnFemaleCheck9":
                    drAssignedRoom = dtAssignedRoom.Select("DormAreaID=" + this.ddlFemaleDormArea9.SelectedValue + " and BuildingID=" + Request.Form[this.ddlFemaleBuilding9.UniqueID.ToString()] + " and RoomType=" + this.ddlFemaleRoomType9.SelectedValue + " and RoomSexType='女'");
                    break;
            }
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('剩余床位数：" + drAssignedRoom.Length + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RegisterEvent();
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {
                this.CacheData();
                this.ddlBind();
            }
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DormAreaID");
            dt.Columns.Add("BuildingID");
            dt.Columns.Add("RoomType");
            dt.Columns.Add("RoomType2");
            dt.Columns.Add("BU");
            dt.Columns.Add("Sex");
            return dt;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["dtSetupContent"] as DataTable;
            if (null == dt)
            {
                dt = this.CreateDataTable();
            }
            if (this.ddlDormArea0.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea0.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding0.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType0.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType20.SelectedValue;
                dr["BU"] = this.ddlBU0.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea1.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea1.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding1.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType1.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType21.SelectedValue;
                dr["BU"] = this.ddlBU1.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea2.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea2.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding2.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType2.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType22.SelectedValue;
                dr["BU"] = this.ddlBU2.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea3.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea3.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding3.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType3.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType23.SelectedValue;
                dr["BU"] = this.ddlBU3.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea4.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea4.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding4.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType4.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType24.SelectedValue;
                dr["BU"] = this.ddlBU4.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea5.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea5.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding5.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType5.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType25.SelectedValue;
                dr["BU"] = this.ddlBU5.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea6.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea6.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding6.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType6.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType26.SelectedValue;
                dr["BU"] = this.ddlBU6.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea7.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea7.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding7.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType7.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType27.SelectedValue;
                dr["BU"] = this.ddlBU7.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea8.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea8.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding8.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType8.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType28.SelectedValue;
                dr["BU"] = this.ddlBU8.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlDormArea9.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "男";
                dr["DormAreaID"] = this.ddlDormArea9.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlBuilding9.UniqueID.ToString()];
                dr["RoomType"] = this.ddlRoomType9.SelectedValue;
                //dr["RoomType2"] = this.ddlRoomType29.SelectedValue;
                dr["BU"] = this.ddlBU9.SelectedValue;
                dt.Rows.Add(dr);
            }
            ViewState["dtSetupContent"] = dt;
        }

        protected void btnFemaleOK_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["dtSetupContent"] as DataTable;
            if (null == dt)
            {
                dt = this.CreateDataTable();
            }
            if (this.ddlFemaleDormArea0.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea0.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding0.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType0.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType20.SelectedValue;
                dr["BU"] = this.ddlFemaleBU0.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea1.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea1.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding1.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType1.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType21.SelectedValue;
                dr["BU"] = this.ddlFemaleBU1.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea2.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea2.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding2.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType2.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType22.SelectedValue;
                dr["BU"] = this.ddlFemaleBU2.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea3.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea3.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding3.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType3.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType23.SelectedValue;
                dr["BU"] = this.ddlFemaleBU3.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea4.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea4.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding4.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType4.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType24.SelectedValue;
                dr["BU"] = this.ddlFemaleBU4.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea5.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea5.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding5.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType5.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType25.SelectedValue;
                dr["BU"] = this.ddlFemaleBU5.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea6.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea6.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding6.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType6.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType26.SelectedValue;
                dr["BU"] = this.ddlFemaleBU6.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea7.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea7.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding7.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType7.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType27.SelectedValue;
                dr["BU"] = this.ddlFemaleBU7.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea8.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea8.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding8.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType8.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType28.SelectedValue;
                dr["BU"] = this.ddlFemaleBU8.SelectedValue;
                dt.Rows.Add(dr);
            }
            if (this.ddlFemaleDormArea9.SelectedValue != "0")
            {
                DataRow dr = dt.NewRow();
                dr["Sex"] = "女";
                dr["DormAreaID"] = this.ddlFemaleDormArea9.SelectedValue;
                dr["BuildingID"] = Request.Form[this.ddlFemaleBuilding9.UniqueID.ToString()];
                dr["RoomType"] = this.ddlFemaleRoomType9.SelectedValue;
                //dr["RoomType2"] = this.ddlFemaleRoomType29.SelectedValue;
                dr["BU"] = this.ddlFemaleBU9.SelectedValue;
                dt.Rows.Add(dr);
            }
            ViewState["dtSetupContent"] = dt;
        }
        
        private bool GetIdCardNumber(string sidcard, string sWorkDayNO, out string sIdCardNumber)
        {
            if (string.IsNullOrEmpty(sidcard))
            {
                if (string.IsNullOrEmpty(sWorkDayNO))
                {
                    sIdCardNumber = string.Empty;
                    return false;
                }
                var empInfo = CommonBLL.GetEmployeeInfo(sWorkDayNO);
                if (null == empInfo)
                {
                    sIdCardNumber = string.Empty;
                    return false;
                }
                else
                {
                    sIdCardNumber = empInfo.idCardNum;
                    return true;
                }
            }
            else
            {
                sIdCardNumber = sidcard;
                return true;
            }
        }

        private void ClearWorkIDInput()
        {
            this.txtWorkDayNo.Text = "";
            this.txtWorkDayNo.Focus();
            this.txtScanCardNO.Text = "";
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                var sInputID = this.txtScanCardNO.Text.Trim();
                var sWorkDayNO = this.txtWorkDayNo.Text.Trim();
                string sIdCard = string.Empty;
                GetIdCardNumber(sInputID, sWorkDayNO, out sIdCard);

                DataTable dt = ViewState["dtSetupContent"] as DataTable;
                DataRow[] drArr = null;
                DataTable dtAssignRoom = ViewState["dtAssignedRoom"] as DataTable;
                DataRow[] drAssignRoomArr = null;
                //ServiceReference1.EmployeeInfoSoapClient employee = new ServiceReference1.EmployeeInfoSoapClient();
                //DataTable dtEmployeeInfo = employee.GetEmployee(this.txtScanCardNO.Text.Trim());

                //查询人员信息
                DataTable dtEmployeeInfo = new StaffingBLL().GetTableWithIDL(sWorkDayNO, sIdCard);

                //DataTable dtEmployeeInfo = new DataTable();
                //dtEmployeeInfo.Columns.Add("Segment");
                //dtEmployeeInfo.Columns.Add("EmployeeID");
                //dtEmployeeInfo.Columns.Add("IDCard");
                //dtEmployeeInfo.Columns.Add("ChineseName");
                //dtEmployeeInfo.Columns.Add("Sex");

                //DataRow drNew = dtEmployeeInfo.NewRow();
                //drNew["Segment"] = "BU1";
                //drNew["EmployeeID"] = "2051773";
                //drNew["IDCard"] = "1234567890";
                //drNew["ChineseName"] = "Eric liu";
                //drNew["Sex"] = "男";
                //dtEmployeeInfo.Rows.Add(drNew);
                if (null != dtEmployeeInfo && dtEmployeeInfo.Rows.Count > 0)
                {
                    string condition = " BU='" + dtEmployeeInfo.Rows[0]["SegmentName"] + "' and Sex ='" + dt.Rows[0]["Sex"] + "' ";
                    drArr = dt.Select(condition);
                    drAssignRoomArr = dtAssignRoom.Select("DormAreaID=" + drArr[0]["DormAreaID"] + " and BuildingID=" + drArr[0]["BuildingID"] + " and RoomType=" + drArr[0]["RoomType"] + " and RoomSexType='" + drArr[0]["Sex"] + "'");
                    string sDormAreaName = drAssignRoomArr[0]["DormAreaName"].ToString();
                    string sBuildingName = drAssignRoomArr[0]["BuildingName"].ToString();
                    string sRoomName = drAssignRoomArr[0]["RoomName"].ToString();
                    string sBedName = drAssignRoomArr[0]["BedName"].ToString();
                    if (dtEmployeeInfo.Rows[0]["Sex"].ToString() == RemarkAttribute.GetEnumRemark(TypeManager.Sex.Male))
                    {
                        this.txtEmployeeNo.Text = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString();
                        this.txtCardNo.Text = dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString();
                        this.txtName.Text = dtEmployeeInfo.Rows[0]["ChineseName"].ToString();
                        this.txtBU.Text = dtEmployeeInfo.Rows[0]["SegmentName"].ToString();
                        this.txtDormArea.Text = drAssignRoomArr[0]["DormAreaName"].ToString();
                        this.txtBuilding.Text = drAssignRoomArr[0]["BuildingName"].ToString();
                        this.txtRoom.Text = drAssignRoomArr[0]["RoomName"].ToString();
                        this.txtBed.Text = drAssignRoomArr[0]["BedName"].ToString();
                    }
                    else
                    {
                        //this.rbSex.SelectedValue = Convert.ToString((int)TypeManager.Sex.Female);
                        this.txtFemaleEmployeeNo.Text = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString();
                        this.txtFemaleCardNo.Text = dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString();
                        this.txtFemaleName.Text = dtEmployeeInfo.Rows[0]["ChineseName"].ToString();
                        this.txtFemaleBU.Text = dtEmployeeInfo.Rows[0]["SegmentName"].ToString();
                        this.txtFemaleDormArea.Text = drAssignRoomArr[0]["DormAreaName"].ToString();
                        this.txtFemaleBuilding.Text = drAssignRoomArr[0]["BuildingName"].ToString();
                        this.txtFemaleRoom.Text = drAssignRoomArr[0]["RoomName"].ToString();
                        this.txtFemaleBed.Text = drAssignRoomArr[0]["BedName"].ToString();
                    }

                    //检查是否已经有CheckIn的记录

                    DataTable DtCheck = new AssignRoomBLL().GetAssignedData(dtEmployeeInfo.Rows[0]["IDCardNumber"].ToString(), "");
                    if (DtCheck != null && DtCheck.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('此用户已有分配记录！')", true);
                    }
                    else
                    {
                        TB_EmployeeCheckIn mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
                        mTB_EmployeeCheckIn.RoomID = Convert.ToInt32(drAssignRoomArr[0]["RoomID"]);
                        mTB_EmployeeCheckIn.BedID = int.Parse(drAssignRoomArr[0]["BedID"].ToString());
                        mTB_EmployeeCheckIn.BU = Util.NormalBU(dtEmployeeInfo.Rows[0]["SegmentName"].ToString());
                        mTB_EmployeeCheckIn.CardNo = sIdCard; //this.txtScanCardNO.Text;
                        mTB_EmployeeCheckIn.CheckInDate = DateTime.Now;
                        mTB_EmployeeCheckIn.Company = string.Empty;
                        mTB_EmployeeCheckIn.EmployeeNo = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString();
                        mTB_EmployeeCheckIn.Name = dtEmployeeInfo.Rows[0]["ChineseName"].ToString();
                        mTB_EmployeeCheckIn.Sex = drAssignRoomArr[0]["RoomSexType"].ToString() == "男" ? 1 : 2;
                        mTB_EmployeeCheckIn.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
                        mTB_EmployeeCheckIn.Creator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount);
                        mTB_EmployeeCheckIn.IsActive = (int)TypeManager.IsActive.Invalid;
                        mTB_EmployeeCheckIn.Telephone = dtEmployeeInfo.Rows[0]["Phone"].ToString();
                        var bAssign = new AssignRoomBLL().AssignRoom(mTB_EmployeeCheckIn);
                        if (!bAssign)
                        {
                            var msg = "分配房间失败，请重新分配";
                            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('" + msg + "')", true);
                            return;
                        }
                        dtAssignRoom.Rows.Remove(drAssignRoomArr[0]);
                        ViewState["dtAssignedRoom"] = dtAssignRoom;
                        //this.txtNation.Text = dt.Rows[0]["Nation"].ToString();
                        //this.txtAddress.Text = dt.Rows[0]["Address"].ToString();
                        //this.txtDept.Text = dt.Rows[0]["Segment"].ToString();
                        //this.txtBirthDay.Text = dt.Rows[0]["Birth"].ToString();

                        if (dtEmployeeInfo.Rows[0]["Phone"].ToString() != "")
                        {
                            string sContent = dtEmployeeInfo.Rows[0]["EmployeeID"].ToString() + "亲，以下是你被分配的宿舍信息：" + sDormAreaName + "宿舍 " + sBuildingName + "栋 " + sRoomName + "房间 " + sBedName + "床.  该宿舍的服务热线18926980019,请于3天内前往宿舍区办理入住手续，谢谢！ ";
                            try
                            {
                                SendSMS(dtEmployeeInfo.Rows[0]["Phone"].ToString(), sContent);
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('招聘系统找不到此用户！')", true);
                }
                //this.txtScanCardNO.Text = string.Empty;
                //this.ReadCardTimer.Enabled = true;
                //this.txtScanCardNO.Focus();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "msg", "alert('异常错误，请检查！')", true);
            }
            finally
            {
                ClearWorkIDInput();
            }
        }

        public void SendSMS(string sPhone, string sContent)
        {
            WebReferenceSMS.FlexDevCommonSMS oSMS = new WebReferenceSMS.FlexDevCommonSMS();
            oSMS.SendSMS("55f39cad-b65d-425d-a0d2-b15cc19f423b", sPhone, sContent);
        }

        protected void ReadCardTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //ReadCardInfo();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 身份证读卡
        /// </summary>
        private void ReadCardInfo()
        {
            //RecruitmentSystem.IDcardReader.IDcardInfo IDcard = RecruitmentSystem.IDcardReader.IDcardReader.GetIDcardInfo();
            //if (IDcard != null)
            //{
            //    //this.txtScanCardNO.Text =  IDcard.IDcardNumber;
            //    //ReadCardTimer.Enabled = false;
            //}
            
        }

    }
}