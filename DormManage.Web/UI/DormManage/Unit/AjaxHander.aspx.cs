using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.Models;

namespace DormManage.Web.UI.DormManage.Unit
{
    public partial class AjaxHander : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strDormAreaID = Request.Params["DormAreaID"];
            string strBuildingID = Request.Params["BuildingID"];
            string strID = Request.Params["ID"];
            string strUnitName = Request.Params["UnitName"];
            int intID;
            UnitBLL mUnitBLL = new UnitBLL();
            Int32.TryParse(strID, out intID);
            TB_Unit mTB_DormArea = new TB_Unit()
            {
                Creator = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount),
                UpdateBy=(base.UserInfo==null?base.SystemAdminInfo.Account:base.UserInfo.ADAccount),
                ID = intID,
                BuildingID=Convert.ToInt32(strBuildingID),
                DormAreaID = Convert.ToInt32(strDormAreaID),
                SiteID=(base.UserInfo==null?base.SystemAdminInfo.SiteID:base.UserInfo.SiteID),
                Name = strUnitName.Trim(),
            };
            mUnitBLL.Edit(mTB_DormArea);
            Response.Write(mUnitBLL.ErrMessage);
            mUnitBLL.ErrMessage = string.Empty;
            Response.End();
        }
    }
}