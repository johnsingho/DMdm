using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.DormManage;
using DormManage.BLL.UserManage;
using DormManage.Framework;
using DormManage.Framework.LogManager;
using DormManage.Models;
using DormManage.BLL.DormPersonManage;
using System.Data;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class CheckOutReason : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int checkInID=Convert.ToInt32(Request.Params["checkinID"].ToString());
            ddlBind();
            CheckIsCharging(checkInID);

        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind()
        {
            Pager mPager = null;
            #region 宿舍区
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormArea.DataValueField = TB_DormArea.col_ID;
            this.ddlDormArea.DataTextField = TB_DormArea.col_Name;

            this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager);
            this.ddlDormArea.DataBind();
            
            #endregion
        }

        void CheckIsCharging(int checkinID)
        {
            EmployeeCheckInBLL bll = new EmployeeCheckInBLL();
            DataTable dt= bll.GetCheckInDateByID(checkinID);
            ChargingBLL chargingbll = new ChargingBLL();
            DataTable dtCharging= chargingbll.GetTableMonthByEmployeeNo(dt.Rows[0]["EmployeeNo"].ToString());
            
            if (dtCharging.Rows.Count>0)
            {
                if(Convert.ToInt32(dtCharging.Rows[0]["Money"])>0)
                {
                    ckGLF.Checked = false;
                    ckGLF.Disabled = true;
                    txtMoney.Text = "本月调房已收管理费";
                }
            }
            
        }
    }
}