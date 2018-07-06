using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.Building
{
    public partial class AjaxHander : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strDormAreaID = Request.Params["DormAreaID"];
            string strID = Request.Params["ID"];
            string strBuildingName = Request.Params["BuildingName"];
            int intID;
            BuildingBLL mBuildingBLL = new BuildingBLL();
            Int32.TryParse(strID, out intID);
            TB_Building mTB_DormArea = new TB_Building()
            {
                Creator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount),
                UpdateBy=(base.UserInfo==null?base.SystemAdminInfo.Account:base.UserInfo.ADAccount),
                ID = intID,
                DormAreaID=Convert.ToInt32(strDormAreaID),
                SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID),
                Name = strBuildingName.Trim(),
            };
            mBuildingBLL.Edit(mTB_DormArea);
            Response.Write(mBuildingBLL.ErrMessage);
            mBuildingBLL.ErrMessage = string.Empty;
            Response.End();
        }
    }
}