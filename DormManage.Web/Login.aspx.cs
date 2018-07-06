using System;
using System.Web.UI;
using DormManage.BLL.UserManage;
using DormManage.Framework.LogManager;

namespace DormManage.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.txtUserName.Text = "admin";
                //this.txtPassWord.Attributes.Add("value","nsc");
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserBLL mUserBLL = new UserBLL();
                string strErrorMsg = string.Empty;//登陆失败错误信息
                bool bolIsAdmin = false;
                if (this.txtUserName.Text.Trim().Equals("admin")|| this.txtUserName.Text.Trim().Equals("admin1"))
                {
                    bolIsAdmin = true;
                }
                //登陆验证
                strErrorMsg = mUserBLL.UserLogin(this.txtUserName.Text.Trim(), this.txtPassWord.Text.Trim(), bolIsAdmin);
                //登陆成功
                if (string.IsNullOrEmpty(strErrorMsg))
                {
                    Response.Redirect("Index.aspx",false);
                }
                //登陆失败
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Login", "<script>alert('" + strErrorMsg + "')</script>");
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message,ex);
            }
        }
    }
}
