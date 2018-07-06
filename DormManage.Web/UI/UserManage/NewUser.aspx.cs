using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.BLL.UserManage;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;

namespace DormManage.Web.UI.UserManage
{
    public partial class NewUser : BasePage
    {

        private void InitPageData()
        {
            string strUserID = Request.QueryString["id"];
            TB_DormArea mTB_DormArea = new TB_DormArea();
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            TB_User mTB_User = null;
            UserBLL mUserBLL = new UserBLL();
            RoleBLL mRoleBLL = new RoleBLL();
            TB_Role mTB_Role = new TB_Role()
            {
                SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID),
            };
            Pager pager = null;
            this.ddlRole.DataTextField = TB_Role.col_Name;
            this.ddlRole.DataValueField = TB_Role.col_ID;
            this.ddlRole.DataSource = mRoleBLL.GetPagerData(mTB_Role, ref pager);
            this.ddlRole.DataBind();
            this.ddlRole.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });

            mTB_DormArea.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            DataTable dtLeft = mDormAreaBLL.GetTable(mTB_DormArea, ref pager);
            DataTable dtRight = null;
            if (null != strUserID && !string.IsNullOrEmpty(strUserID))
            {
                mTB_User = mUserBLL.Get(Convert.ToInt32(strUserID));
                this.txtADAccount.Text = mTB_User.ADAccount;
                this.txtEmployeeNo.Text = mTB_User.EmployeeNo;
                this.txtCName.Text = mTB_User.CName;
                this.txtEName.Text = mTB_User.EName;
                this.ddlRole.SelectedValue = mTB_User.RoleID.ToString();

                dtRight = mDormAreaBLL.GetTableByUserID(Convert.ToInt32(strUserID));
                DataRow[] drFilter = null;
                for (int i = dtLeft.Rows.Count - 1; i >= 0; i--)
                {
                    drFilter = dtRight.Select("ID=" + dtLeft.Rows[i][TB_DormArea.col_ID] + "");
                    if (drFilter.Length > 0)
                    {
                        dtLeft.Rows.Remove(dtLeft.Rows[i]);
                    }
                }

            }
            ViewState["dtLeft"] = dtLeft;
            gdvLeft.DataSource = dtLeft;
            gdvLeft.DataBind();

            ViewState["dtRight"] = dtRight;
            gdvRight.DataSource = dtRight;
            gdvRight.DataBind();

        }

        private List<int> GetCheckedIDs(ExtendGridView.GridView mGridView)
        {
            List<int> lstID = new List<int>();
            //遍历找出所有被选中的行
            for (int i = 0; i < mGridView.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)mGridView.Rows[i].Cells[0].FindControl("chkLeftSingle");
                //被选中
                if (chk.Checked && chk.Visible == true)
                {
                    lstID.Add(Convert.ToInt32(mGridView.DataKeys[i].Value.ToString()));
                }
            }
            return lstID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.InitPageData();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }

        /// <summary>
        /// 移除宿舍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> lstID = this.GetCheckedIDs(this.gdvRight);
                DataTable dtLeft = ViewState["dtLeft"] as DataTable;
                DataTable dtRight = ViewState["dtRight"] as DataTable;
                if (dtRight != null)
                {
                    if (dtLeft == null) dtLeft = dtRight.Clone();
                    for (int i = dtRight.Rows.Count - 1; i >= 0; i--)
                    {
                        if (lstID.Contains(Convert.ToInt32(dtRight.Rows[i][TB_DormArea.col_ID])))
                        {
                            DataRow drLeft = dtLeft.NewRow();
                            drLeft.ItemArray = dtRight.Rows[i].ItemArray;
                            dtLeft.Rows.Add(drLeft);
                            dtRight.Rows.Remove(dtRight.Rows[i]);
                        }
                    }
                    ViewState["dtLeft"] = dtLeft;
                    gdvLeft.DataSource = dtLeft;
                    gdvLeft.DataBind();
                    ViewState["dtRight"] = dtRight;
                    gdvRight.DataSource = dtRight;
                    gdvRight.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }

        /// <summary>
        /// 添加宿舍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> lstID = this.GetCheckedIDs(this.gdvLeft);
                DataTable dtLeft = ViewState["dtLeft"] as DataTable;
                DataTable dtRight = ViewState["dtRight"] as DataTable;
                if (dtLeft != null)
                {
                    if (null == dtRight) dtRight = dtLeft.Clone();
                    for (int i = dtLeft.Rows.Count - 1; i >= 0; i--)
                    {
                        if (lstID.Contains(Convert.ToInt32(dtLeft.Rows[i][TB_DormArea.col_ID])))
                        {
                            DataRow drRight = dtRight.NewRow();
                            drRight.ItemArray = dtLeft.Rows[i].ItemArray;
                            dtRight.Rows.Add(drRight);
                            dtLeft.Rows.Remove(dtLeft.Rows[i]);
                        }
                    }
                    ViewState["dtLeft"] = dtLeft;
                    gdvLeft.DataSource = dtLeft;
                    gdvLeft.DataBind();
                    ViewState["dtRight"] = dtRight;
                    gdvRight.DataSource = dtRight;
                    gdvRight.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strErrorMsg = string.Empty; ;
                UserBLL mUserBLL = new UserBLL();
                List<int> lstDormAreaID = new List<int>();
                for (int i = 0; i < gdvRight.Rows.Count; i++)
                {
                    lstDormAreaID.Add(Convert.ToInt32(gdvRight.DataKeys[i].Value.ToString()));
                }
                TB_User mTB_User = new TB_User();
                mTB_User.ID = Request.QueryString["id"] == null ? 0 : Convert.ToInt32(Request.QueryString["id"]);
                mTB_User.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
                mTB_User.Creator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount);
                mTB_User.ADAccount = this.txtADAccount.Text.Trim();
                mTB_User.CName = this.txtCName.Text.Trim();
                mTB_User.EName = this.txtEName.Text.Trim();
                mTB_User.EmployeeNo = this.txtEmployeeNo.Text.Trim();
                mTB_User.RoleID = Convert.ToInt32(this.ddlRole.SelectedValue);
                strErrorMsg = mUserBLL.Edit(mTB_User, lstDormAreaID);
                if (string.IsNullOrEmpty(strErrorMsg))
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>saveComplete();</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "message", "<script>alert('" + strErrorMsg + "');</script>");
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message);
            }
        }
    }
}