using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.UserManage;
using DormManage.Models;

namespace DormManage.Web.UI.UserManage
{
    public partial class NewBU : BasePage
    {

        private void InitPageData()
        {
            int intBUID;
            BUBLL mBUBLL = new BUBLL();
            if (Int32.TryParse(Request.QueryString["id"], out intBUID))
            {
                this.txtBU.Text = mBUBLL.Get(intBUID).Name;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPageData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            TB_BU tb_BU = new TB_BU();
            tb_BU.Name = this.txtBU.Text.Trim();
            tb_BU.ID = Request.QueryString["id"] == null ? 0 : Convert.ToInt32(Request.QueryString["id"]);
            tb_BU.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            try
            {
                new BUBLL().Edit(tb_BU);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "complete", "<script>saveComplete();</script>");
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('名称重复')</script>");
                }
            }
        }
    }
}