using DormManage.BLL;
using System;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class CheckOutReasonChange : BasePage
    {
        //for test
        // http://localhost:8898/UI/DormPersonManage/CheckOutReasonChange.aspx?edi=43690&oldReason=调房&oldCanLeave=1

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int eid = Convert.ToInt32(Request.Params["eid"].ToString());
                string soldReason = System.Web.HttpUtility.UrlDecode(Request.Params["oldReason"]);
                string soldCanLeave = Request.Params["oldCanLeave"];
                ddlReason.SelectedValue = soldReason;
                var bCheck = Convert.ToByte(soldCanLeave) > 0;
                chkSignExit.Checked = bCheck;
                chkSignExit.Enabled = bCheck;
                
                ClientScript.RegisterStartupScript(this.GetType(), "reSetChkSign", "<script>ReSetChkSign();</script>");
            }
            
        }

    }
}