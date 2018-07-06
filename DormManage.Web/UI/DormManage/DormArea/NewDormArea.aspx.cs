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
    public partial class NewDormArea : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strID = Request.QueryString["id"];
                int intID;
                TB_DormArea mTB_DormArea = null;
                DormAreaBLL mDormAreaBLL=new DormAreaBLL();
                if (Int32.TryParse(strID,out intID))
                {
                    mTB_DormArea = mDormAreaBLL.Get(intID);
                    this.txtDormAreaName.Text = mTB_DormArea.Name;
                }
            }
        }
    }
}