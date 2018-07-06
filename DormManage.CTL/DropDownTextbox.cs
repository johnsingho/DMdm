using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace DormManage.Web.UI.ExtendTree
{
    /// <summary>
    /// 下拉选择输入框
    /// </summary>
    public class DropDownTextbox : WebControl, INamingContainer
    {
        #region [私有变量]

        private string m_dataValueField = "ID";//值字段名
        private string m_dataTextField = "NAME";//显示字段名
        private string m_toolTipField = "NAME";//title对应的字段名
        private string m_ajaxUrl = "URL";//ajax对应的url
        private Unit m_Top = new Unit(40);     //默认高度
        //private string m_textChange = "URL";//ajax对应的url 
        private Unit m_width = new Unit(160);//宽度
        private TextBox m_text = new TextBox();
        private HiddenField m_value = new HiddenField();
        #endregion

        #region [构造函数]

        /// <summary>
        /// 构造函数
        /// </summary>
        public DropDownTextbox()
        {

        }
        #endregion

        #region [公共属性]

        /// <summary>
        /// 值字段名
        /// </summary>
        public string DataValueField
        {
            get
            {
                return this.m_dataValueField;
            }
            set
            {
                this.m_dataValueField = value;
            }
        }

        /// <summary>
        /// 值字段名
        /// </summary>
        public string DataTextField
        {
            get
            {
                return this.m_dataTextField;
            }
            set
            {
                this.m_dataTextField = value;
            }
        }

        /// <summary>
        /// title对应的字段名
        /// </summary>
        public string ToolTipField
        {
            get
            {
                return this.m_toolTipField;
            }
            set
            {
                this.m_toolTipField = value;
            }
        }

        
        /// <summary>
        /// ajax对应的url
        /// </summary>
        public string AjaxUrl
        {
            get
            {
                return this.m_ajaxUrl;
            }
            set
            {
                this.m_ajaxUrl = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                EnsureChildControls();
                return this.m_text.Text;
            }
            set
            {
                EnsureChildControls();
                this.m_text.Text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get
            {
                EnsureChildControls();
                return this.m_value.Value;
            }
            set
            {
                EnsureChildControls();
                this.m_value.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public new Unit Width
        {
            get
            {
                return m_width;
            }
            set
            {
                this.m_width = value;
            }
        }

        public Unit Tops
        {
            get
            {
                return m_Top;
            }
            set
            {
                this.m_Top = value;
            }
        }


        #endregion

        #region[接口方法]

        protected override void CreateChildControls()
        {
            //清空控件
            Controls.Clear();
            Controls.Add(m_text);

            m_text.ID = "value_" + base.ID;
            Controls.Add(m_value);

            //告诉编译器，控件已经初始化了
             ChildControlsCreated = true;

            base.CreateChildControls();
        }

        #endregion

        #region[私有方法]

        protected override void RenderContents(System.Web.UI.HtmlTextWriter output)
        {
            //加入文本变化激发的js方法
            m_text.Attributes.Add("onkeyup", "ddtb_textchange(this,'" + m_ajaxUrl + "');");
            m_text.Attributes.Add("onkeydown", "ddtb_Next();");
            m_text.Attributes.Add("onclick", "this.focus();this.select()");
            m_text.Attributes.Add("pid", base.ID);
            m_text.Attributes.Add("class", "txt");
            m_text.Style.Add("z-index", "100");
            m_text.CssClass = "txt";
            m_text.AutoCompleteType = AutoCompleteType.Disabled;
            m_text.ID = "txt";// +base.ID;
            m_text.Width = Width;
            m_text.ToolTip = this.ToolTip;
            m_value.ID = "hid";// +base.ID;

            output.Write("<div style='width:" + Width.ToString() + ";z-index:15000;'  class='ddtb' >");
            m_text.RenderControl(output);
            m_value.RenderControl(output);
            output.Write("<div id='ddd_" + base.ID + "' style='width:100%;padding:0px;margin:0px;display:none;z-index:15000;position:absolute;left:0px;top:"+ Tops.ToString()+ ";'>");
            output.Write("</div>");
            output.Write("<div id='ddt_" + base.ID + "' style='padding:0px;margin:0px;display:none;position:absolute;z-index:15000;color:Black;border:solid 1px Black; background-color:#FFFFE1;'>");
            output.Write("</div>");
            output.Write("</div>");
            
            //base.Render(output);
        }
        #endregion
    }
}
