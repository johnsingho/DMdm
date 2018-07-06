using System;
using System.Data;
using System.Web.UI;
using DormManage.BLL.UserManage;
using DormManage.Framework.LogManager;
using DormManage.Models;
using DormManage.Web.UI.ExtendTree;

namespace DormManage.Web
{
    public partial class Index : BasePage
    {
        private void BindTree()
        {
            BindTree mBindTree;
            ModuleBLL mModuleBLL = new ModuleBLL();
            DataTable dtDataSouce = null;
            if (base.UserInfo == null)
            {
                TB_Module mTB_Module = new TB_Module();
                mTB_Module.IsActive = 1;
                mTB_Module.SiteID = base.SystemAdminInfo.SiteID;
                dtDataSouce = mModuleBLL.GetAllModule(mTB_Module);
            }
            else
            {
                dtDataSouce = mModuleBLL.GetUserModule(base.UserInfo.ID);
            }
            mBindTree = new BindTree(this.treModule);
            mBindTree.NodeIDCol = "ID";
            mBindTree.NodeParentCol = "PID";
            mBindTree.NodeTitleCol = "Name";
            mBindTree.NodeUrlCol = "URL";
            mBindTree.DataSouce = dtDataSouce;
            mBindTree.Bind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var tarPage = "HomePage.aspx";
                //var tarPage = "FlexPlusStatic.html";
                var sPage = base.Request["page"];
                if (!string.IsNullOrEmpty(sPage))
                {
                    tarPage = sPage;
                }
                hidTar.Value = tarPage;

                if (!IsPostBack)
                {
                    Session.Timeout = 300;　
                    this.lblUserNameWelcome.Text = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount)
                        +"，您好！";
                    if (base.SystemAdminInfo == null)
                    {
                        this.btnModifyInfo.Enabled = false;
                    }
                    this.BindTree();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }

        protected void lnkExit_Click(object sender, EventArgs e)
        {
            try
            {
                base.Logoff();
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }
    }
}