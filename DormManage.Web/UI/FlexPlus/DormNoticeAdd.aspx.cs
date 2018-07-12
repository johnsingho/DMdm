using DormManage.BLL.FlexPlus;
using DormManage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.FlexPlus
{   
    public partial class DormNoticeAdd : BasePage
    {
        private string key = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.key = base.Request["key"];
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.key))
                {
                    this.InitData();
                }
            }
        }

        private void InitData()
        {
            var bll = new FlexPlusBLL();
            var dt = bll.GetDormNoticeByID(key);
            if (DataTableHelper.IsEmptyDataTable(dt))
            {
                return;
            }
            var dr = dt.Rows[0];
            var sTitle = dr["NoticeTitle"] as string;
            var sContext = dr["NoticeHtml"] as string;
            txtTitle.Text = sTitle;
            txtContext.Value = sContext;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var sTitle = txtTitle.Text.Trim();
            var sContext = txtContext.Value.Trim();
            var sCreator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.EmployeeNo);
            var bll = new FlexPlusBLL();
            if (string.IsNullOrEmpty(key))
            {
                bll.AddDormNotice(sTitle, sContext, sCreator);
            }
            else
            {
                bll.EditDormNotice(key, sTitle, sContext, sCreator);
            }

            RunScript("myscript", "saveComplete();");
        }
    }
}