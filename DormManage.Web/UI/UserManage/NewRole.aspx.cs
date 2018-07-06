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
    public partial class NewRole : BasePage
    {

        private void InitPageData()
        {
            int intRoleID;
            ModuleBLL mModuleBLL = new ModuleBLL();
            RoleBLL mRoleBLL = new RoleBLL();
            TB_Role tbRole = null;
            Int32.TryParse(Request.QueryString["id"], out intRoleID);
            mModuleBLL.LoadTreeModule(this.treAuthority, intRoleID);
            tbRole = mRoleBLL.GetByID(intRoleID);
            if (null != tbRole)
            {
                this.txtRoleName.Text = tbRole.Name;
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
            List<int> lstModuleID = new List<int>();
            RoleBLL mRoleBLL = new RoleBLL();
            string strErrorMsg = string.Empty;
            TB_Role mTB_Role = new TB_Role()
            {
                ID = Request.QueryString["id"] == null ? 0 : Convert.ToInt32(Request.QueryString["id"]),
                SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID),
                Name = this.txtRoleName.Text,
            };
            foreach (TreeNode pNode in this.treAuthority.CheckedNodes)
            {
                if (pNode.Parent != null)
                {
                    if (!lstModuleID.Contains(Convert.ToInt32(pNode.Parent.Value)))
                    {
                        lstModuleID.Add(Convert.ToInt32(pNode.Parent.Value));
                        lstModuleID.Add(Convert.ToInt32(pNode.Value));
                    }
                    else
                    {
                        lstModuleID.Add(Convert.ToInt32(pNode.Value));
                    }
                }
                else
                {
                    lstModuleID.Add(Convert.ToInt32(pNode.Value));
                }
            }
            strErrorMsg = mRoleBLL.Edit(mTB_Role, lstModuleID);
            if (string.IsNullOrEmpty(strErrorMsg))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "complete", "<script>saveComplete();</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "complete", "<script>alert('" + strErrorMsg + "')</script>");
        }

        protected void treAuthority_SelectedNodeChanged(object sender, EventArgs e)
        {
            //TreeView tree = (TreeView)sender;
            //foreach (TreeNode pNode in tree.Nodes)
            //{
            //    if (pNode == tree.SelectedNode)
            //    {
            //        foreach (TreeNode childNode in pNode.ChildNodes)
            //        {
            //            childNode.Checked = true;
            //        }
            //    }
            //}
        }
    }
}