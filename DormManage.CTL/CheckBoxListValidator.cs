using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DormManage.SingleSafe.CTL
{
    /// <summary>
    /// 描  述：多选列表验证
    /// 作  者：
    /// 时  间：
    /// 修  改：
    /// 原  因：
    /// </summary>
    public class CheckBoxListValidator : BaseValidator
    {
        protected override bool ControlPropertiesValid()
        {
            return true;
        }

        protected override bool EvaluateIsValid()
        {
            return EvaluateIsChecked();
        }

        protected bool EvaluateIsChecked()
        {
            CheckBoxList _cbl = ((CheckBoxList)FindControl(ControlToValidate));
            foreach (ListItem li in _cbl.Items)
            {
                if (li.Selected == true)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (EnableClientScript)
            {
                ClientScript();
            }
            base.OnPreRender(e);
        }

        protected void ClientScript()
        {
            Attributes["evaluationfunction"] = "cb_vefify";

            StringBuilder sb_Script = new StringBuilder();
            sb_Script.Append("<script language=\"javascript\">");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("function cb_vefify(val) {");
            sb_Script.Append("\r");
            sb_Script.Append("var val = document.all[document.all[\"");
            sb_Script.Append(this.ClientID);
            sb_Script.Append("\"].controltovalidate];");
            sb_Script.Append("\r");
            sb_Script.Append("var col = val.all;");
            sb_Script.Append("\r");
            sb_Script.Append("if ( col != null ) {");
            sb_Script.Append("\r");
            sb_Script.Append("for ( i = 0; i < col.length; i++ ) {");
            sb_Script.Append("\r");
            sb_Script.Append("if (col.item(i).tagName == \"INPUT\") {");
            sb_Script.Append("\r");
            sb_Script.Append("if ( col.item(i).checked ) {");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("return true;");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("return false;");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("</script>");

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "RBLScript", sb_Script.ToString(), false);
        }
    }
}
