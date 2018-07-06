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
    public partial class EditCheckIn : BasePage
    {
        private void BindBU()
        {
            txtEmployeeNo.Text= Request.QueryString["employeeNo"].ToString();
            txtEmployeeNo.ReadOnly = true;
            txtName.Text = Request.QueryString["name"].ToString();
            txtName.ReadOnly = true;
            txtBU.Text= Request.QueryString["BU"].ToString();
            txtEmployeeType.Text = Request.QueryString["EmployeeTypeName"].ToString();
            txtCheckinDate.Text = Request.QueryString["CheckInDate"].ToString();
            txtPhone.Text = Request.QueryString["Telephone"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                this.BindBU();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TB_EmployeeCheckIn mTB_EmployeeCheckIn = new TB_EmployeeCheckIn();
                EmployeeCheckInBLL mEmployeeCheckInBLL = new EmployeeCheckInBLL();
                mTB_EmployeeCheckIn.ID =Convert.ToInt32(Request.QueryString["id"].ToString()); 
                mTB_EmployeeCheckIn.BU = txtBU.Text;
                mTB_EmployeeCheckIn.EmployeeTypeName = txtEmployeeType.Text;
                mTB_EmployeeCheckIn.CheckInDate =Convert.ToDateTime(txtCheckinDate.Text);
                mTB_EmployeeCheckIn.Telephone = txtPhone.Text;
                mEmployeeCheckInBLL.EditTB_EmployeeCheckIn(mTB_EmployeeCheckIn);

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>saveComplete();</script>");
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }
    }
}