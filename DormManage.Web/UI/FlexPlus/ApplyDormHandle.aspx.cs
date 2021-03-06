﻿using DormManage.BLL.DormManage;
using DormManage.BLL.FlexPlus;
using DormManage.Common;
using DormManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                ddlHandle_SelectedIndexChanged(null, null);
            }
            else
            {
                //加载旧数据，限制提交
                var bll = new FlexPlusBLL();
                var mTB_DormAreaApply = new TB_DormAreaApply();
                int nID = 0;
                int.TryParse(mKeys[0], out nID);
                mTB_DormAreaApply.ID = nID;
                Framework.Pager pager = null;
                var dt = bll.GetApplyDorms(mTB_DormAreaApply, ref pager);
                if(!DataTableHelper.IsEmptyDataTable(dt))
                {
                    var dr = dt.Rows[0];
                    int nStatus = 0;
                    nStatus = Convert.ToInt32(dr["Status"]);
                    if(nStatus != 0)
                    {
                        var sLastResp = dr["Response"].ToString();
                        txtReply.Value = sLastResp;

                        ddlDormArea.Enabled = false;
                        ddlHandle.Enabled = false;
                        txtReply.Disabled = true;
                        btnSave.Visible = false;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        ddlHandle_SelectedIndexChanged(null, null);
                        txtReply.Disabled = false;
                        txtReply.Focus();
                    }
                }
            }
            
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
            var sEnName = (base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.EName);
            bll.ApplyDorm(lst, sHandlerWorkdayNo, sEnName, sDormAreaID, sHandle, sMsg);

            RunScript(this, "myscript", "saveComplete();");

        }

        protected void ddlDormArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sReply = txtReply.Value;
            var sDormAera = ddlDormArea.SelectedItem.Text;

            var sPat = @"【(.+)】";
            var reg = new Regex(sPat);
            var sRepl = string.Format("【{0}】", sDormAera);
            var sNew = reg.Replace(sReply, sRepl);
            txtReply.Value = sNew;
        }
    }
}