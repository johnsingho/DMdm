using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

///------------------------------------------------------------------------------
///描  述：AutoTextBox 自适应高度TextBox
///版本号：
///作  者：
///日  期：
///修  改：
///原  因：
///------------------------------------------------------------------------------
namespace AutoTextbox
{
    /// <summary> 
    /// 自适应textbox
    /// </summary> 
    [DefaultProperty("Text"),
        ToolboxData("<{0}:AutoTextArea runat=server></{0}:AutoTextArea>")]
    public class AutoTextArea : System.Web.UI.WebControls.TextBox
    {
        [DefaultValue(200)]
        //MaxHeight <0 时不限制最大行
        // 为空时默认为 200
        public int MaxHeight
        {
            get
            {
                object obj = ViewState["MaxHeight"];
                return obj == null ? 200 : (int)obj;
            }
            set
            {
                ViewState["MaxHeight"] = value;
            }
        }

        [DefaultValue(60)]
        //最小高为空时 默认为 60
        public int MinHeight
        {
            get
            {
                object obj = ViewState["MinHeight"];
                return obj == null ? 60 : (int)obj;
            }
            set
            {
                ViewState["MinHeight"] = value;
            }
        }
        /// <summary>
        /// 描  述：设定属性
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            this.Attributes["minHeight"] = this.MinHeight.ToString();
            if (this.Height == Unit.Empty)
            {
                this.Height = this.MinHeight;
            }
            else
            {
                this.Height = (int)Math.Max(this.MinHeight, this.Height.Value);
            }
            base.OnPreRender(e);
        }

        /// <summary>
        /// 描  述：重写输出方法 去掉滚动条 换行自动增加新行
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            string strCode;
            if (this.MaxHeight <= 0)
            {
                strCode = "this.style.height=Math.max(this.minHeight,this.scrollHeight)+(this.offsetHeight-this.clientHeight)";
            }
            else
            {
                strCode = "this.style.height=(this.scrollHeight>200)?200:Math.max(this.minHeight,this.scrollHeight)+(this.offsetHeight-this.clientHeight)";
            }
            //点击js事件
            base.Attributes["onpropertychange"] = strCode; //内容改变时 触发
            base.Attributes["onfocus"] = "this.height=this.height;this.select();";//得到焦点时 触发 
            //base.Attributes["onkeyup"] = "this.height=this.height"; 
            if (base.Rows == 0)
            {
                base.Rows = 1;
            }
            base.TextMode = TextBoxMode.MultiLine;
            base.Render(output);
        }
    }
}
