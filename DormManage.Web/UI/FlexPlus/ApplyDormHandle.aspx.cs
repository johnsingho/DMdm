using DormManage.BLL.DormManage;
using DormManage.BLL.FlexPlus;
using DormManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class ApplyDormHandle : BasePage
    {
        private static readonly string KEY_PARAMS = "DORM_APPLY2018";

        protected void Page_Load(object sender, EventArgs e)
        {
            var keys = Request.Params["keys"].ToString();
            if (!IsPostBack)
            {
                BindSelect();

                if (!string.IsNullOrEmpty(keys))
                {
                    var mKeys = keys.Split(',');
                    SessionHelper.Set(KEY_PARAMS, mKeys);
                    InitData(mKeys);
                }                
            }
        }

        private void InitData(string[] mKeys)
        {            
            if (mKeys.Length > 1)
            {
                ddlDormArea.Enabled = false;
                //txtReply.Disabled = true;
            }
            else
            {
                ddlDormArea.SelectedValue = mKeys[0];
            }

            ddlHandle_SelectedIndexChanged(null, null);
        }

        private void BindSelect()
        {
            var lst = new List<TNameVal>();
            lst.Add(new TNameVal("批准", "1"));
            lst.Add(new TNameVal("拒绝", "2"));
            ddlHandle.DataSource = lst;
            ddlHandle.DataValueField = "Value";
            ddlHandle.DataTextField = "Name";
            ddlHandle.DataBind();
            ddlHandle.SelectedIndex = 0;

            //
            var mTB_DormArea = new TB_DormArea();
            var mDormAreaBLL = new DormAreaBLL();
            Framework.Pager page = null;
            var dt = mDormAreaBLL.GetTable(mTB_DormArea, ref page);
            ddlDormArea.DataSource = dt;
            ddlDormArea.DataValueField="ID";
            ddlDormArea.DataTextField="Name";
            ddlDormArea.DataBind();
        }

        protected void ddlHandle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mKeys = GetKeys();
            var sSel = ddlHandle.SelectedValue;
            if("1" == sSel)
            {
                var sDormArea = ddlDormArea.SelectedItem.Text;
                var sResponse = string.Empty;
                if (mKeys.Length > 1)
                {
                    sResponse=string.Format("您的住宿申请成功，请于三天内到你申请的宿舍区办理入住。");
                }
                else
                {
                    sResponse=string.Format("您的住宿申请成功，请于三天内到【{0}】宿舍区办理入住。", sDormArea);
                }
                txtReply.Value = sResponse;
            }
            else
            {
                var sResponse = string.Format("很抱歉，你的住宿申请未获批准。");
                txtReply.Value = sResponse;
            }
        }

        private static string[] GetKeys()
        {
            var oKeys = SessionHelper.Get(KEY_PARAMS);
            if (null == oKeys)
            {
                return null;
            }
            var mKeys = (string[])oKeys;
            return mKeys;
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var mKeys = GetKeys();
            if(null==mKeys || 0 == mKeys.Length)
            {
                return;
            }

            var bll = new FlexPlusBLL();
            var sDormAreaID = ddlDormArea.SelectedValue;
            var sHandle = ddlHandle.SelectedValue;
            var sMsg = txtReply.Value.Trim();
            var lst = new List<string>(mKeys);
            var sHandlerWorkdayNo = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.EmployeeNo);
            bll.ApplyDorm(lst, sHandlerWorkdayNo, sDormAreaID, sHandle, sMsg);

            RunScript(this, "myscript", "<script>saveComplete();</script>");
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>saveComplete();</script>");

        }

    }
}