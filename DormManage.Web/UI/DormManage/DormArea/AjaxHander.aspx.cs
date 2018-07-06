using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.DormArea
{
    public partial class AjaxHander : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strDormAreaName = Request.Params["DormAreaName"];
            string strID = Request.Params["ID"];
            int intID;
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            Int32.TryParse(strID, out intID);
            TB_DormArea mTB_DormArea = new TB_DormArea()
            {
                SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID),
                ID = intID,
                Name = strDormAreaName.Trim(),
                UpdateBy = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount),
                Creator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount),
            };
            mDormAreaBLL.Edit(mTB_DormArea);
            Response.Write(mDormAreaBLL.ErrMessage);
            mDormAreaBLL.ErrMessage = string.Empty;
            Response.End();
        }
    }
}