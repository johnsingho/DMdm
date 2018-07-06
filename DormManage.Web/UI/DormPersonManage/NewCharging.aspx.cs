using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormPersonManage;
using DormManage.BLL.UserManage;
using DormManage.Framework.LogManager;
using DormManage.Models;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class NewCharging : BasePage
    {
        private void BindBU()
        {
           

            if (Request.QueryString["edit"].ToString()=="true")
            {
                txtEmployeeNo.Text = Request.QueryString["employeeNo"].ToString();
                txtEmployeeNo.ReadOnly = true;
                txtName.Text = Request.QueryString["name"].ToString();
                txtName.ReadOnly = true;
                //txtChargeContent.Text = Request.QueryString["ChargeContent"].ToString();
                txtMoney.Text = Request.QueryString["Money"].ToString();
                //txtAirConditionFee.Text = Request.QueryString["AirConditionFee"].ToString();
                txtAirConditionFeeMoney.Text = Request.QueryString["AirConditionFeeMoney"].ToString();
                //txtRoomKeyFee.Text = Request.QueryString["RoomKeyFee"].ToString();
                txtRoomKeyFeeMoney.Text = Request.QueryString["RoomKeyFeeMoney"].ToString();
                //txtOtherFee.Text = Request.QueryString["OtherFee"].ToString();
                txtOtherFeeMoney.Text = Request.QueryString["OtherFeeMoney"].ToString();
                this.ddlBU.Items.Insert(0, new ListItem() { Value = Request.QueryString["BU"].ToString(), Text = Request.QueryString["BU"].ToString() });
                
                ddlBU.Enabled = false;
            }
            else
            {
                this.ddlBU.DataTextField = "Name";
                this.ddlBU.DataValueField = "Name";
                this.ddlBU.DataSource = new BUBLL().GetTable(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
                this.ddlBU.DataBind();
                this.ddlBU.Items.Insert(0, new ListItem() { Value = "", Text = "--请选择--" });
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormPersonManageAjaxServices));
            if (!IsPostBack)
            {
                this.BindBU();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["edit"].ToString() == "true")
                {
                    ChargingBLL mChargingBLL = new ChargingBLL();
                    TB_Charging mTB_Charging = new TB_Charging();
                    mTB_Charging.ID =Convert.ToInt32(Request.QueryString["id"].ToString());
                    mTB_Charging.Name = this.txtName.Text;
                    mTB_Charging.EmployeeNo = this.txtEmployeeNo.Text;
                    mTB_Charging.ChargeContent = this.txtChargeContent.Text;
                    mTB_Charging.Money = this.txtMoney.Text.Length > 0 ? Convert.ToDecimal(txtMoney.Text) : 0;
                    mTB_Charging.AirConditionFee = this.txtAirConditionFee.Text;
                    mTB_Charging.AirConditionFeeMoney = this.txtAirConditionFeeMoney.Text.Length > 0 ? Convert.ToDecimal(this.txtAirConditionFeeMoney.Text) : 0;
                    mTB_Charging.RoomKeyFee = this.txtRoomKeyFee.Text;
                    mTB_Charging.RoomKeyFeeMoney = this.txtRoomKeyFeeMoney.Text.Length > 0 ? Convert.ToDecimal(this.txtRoomKeyFeeMoney.Text) : 0;
                    mTB_Charging.OtherFee = this.txtOtherFee.Text;
                    mTB_Charging.OtherFeeMoney = this.txtOtherFeeMoney.Text.Length > 0 ? Convert.ToDecimal(this.txtOtherFeeMoney.Text) : 0;
                    mTB_Charging.SiteID = base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID;
                    mTB_Charging.UpdateBy = base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount;
                    mTB_Charging.BU = this.ddlBU.SelectedValue;
                    mChargingBLL.Edit(mTB_Charging);
                }
                else
                {
                    ChargingBLL mChargingBLL = new ChargingBLL();
                    TB_Charging mTB_Charging = new TB_Charging();
                    mTB_Charging.Name = this.txtName.Text;
                    mTB_Charging.EmployeeNo = this.txtEmployeeNo.Text;
                    mTB_Charging.ChargeContent = this.txtChargeContent.Text;
                    mTB_Charging.Money = this.txtMoney.Text.Length > 0 ? Convert.ToDecimal(txtMoney.Text) : 0;
                    mTB_Charging.AirConditionFee = this.txtAirConditionFee.Text;
                    mTB_Charging.AirConditionFeeMoney =this.txtAirConditionFeeMoney.Text.Length > 0 ? Convert.ToDecimal(this.txtAirConditionFeeMoney.Text) : 0; 
                    mTB_Charging.RoomKeyFee = this.txtRoomKeyFee.Text;
                    mTB_Charging.RoomKeyFeeMoney = this.txtRoomKeyFeeMoney.Text.Length > 0 ? Convert.ToDecimal(this.txtRoomKeyFeeMoney.Text) : 0; 
                    mTB_Charging.OtherFee = this.txtOtherFee.Text;
                    mTB_Charging.OtherFeeMoney = this.txtOtherFeeMoney.Text.Length > 0 ? Convert.ToDecimal(this.txtOtherFeeMoney.Text) : 0;
                    mTB_Charging.SiteID = base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID;
                    mTB_Charging.Creator = base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount;
                    mTB_Charging.BU = this.ddlBU.SelectedValue;
                    mChargingBLL.Add(mTB_Charging, null);
                }
              
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>saveComplete();</script>");
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }
    }
}