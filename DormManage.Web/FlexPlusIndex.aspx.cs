using DormManage.BLL.UserManage;
using DormManage.Framework.LogManager;
using DormManage.Models;
using DormManage.Web.UI.ExtendTree;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web
{
    public partial class FlexPlusIndex : BasePage
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
                dtDataSouce = mModuleBLL.GetAllModule_FlexPlus(mTB_Module);
            }
            else
            {
                dtDataSouce = mModuleBLL.GetUserModule_FlexPlus(base.UserInfo.ID);
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
                var tarPage = "FlexPlusStatic.html";
                var sPage = base.Request["page"];
                if (!string.IsNullOrEmpty(sPage))
                {
                    tarPage = sPage;
                }
                hidTar.Value = tarPage;

                if (!IsPostBack)
                {
                    this.BindTree();
                }                
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }
    }
}