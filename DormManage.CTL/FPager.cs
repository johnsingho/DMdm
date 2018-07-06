using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DormManage.CTL
{
    public delegate int EventPagingHandler(EventPagingArg e); //点击事件委托声明
    /// <summary>
    /// Form分页控件
    /// </summary>
    /// <remarks>
    /// ------------------------------------------------------------------------------
    /// 描  述：为客户端提供分页控件，结合PAGER类进行分页查询
    /// 版本号：
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </remarks>
    /// <example>
    /// [示例代码在这里写入]
    /// </example>
    /// <param name="e"></param>
    /// <returns></returns>
    public partial class FPager : UserControl
    {
        public FPager()
        {
            InitializeComponent();
        }
        public event EventPagingHandler EventPaging;     //注册事件
        private Regex _regex = new Regex(@"^[1-9]\d?$"); //跳转页数是否为数字验证
        private int _pageSize = 20;                      //每页显示的记录数
        private int _nMax = 0;                           //总记录
        private int _pageCount = 0;                      //总页数
        private int _pageCurrent = 0;                    //当前页

        #region 属性

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                GetPageCount();
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int NMax
        {
            get { return _nMax; }
            set
            {
                _nMax = value;
                GetPageCount();
            }
        }

        /// <summary>
        /// 页数=总记录数/每页显示记录数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageCurrent
        {
            get { return _pageCurrent; }
            set { _pageCurrent = value; }
        }

        public BindingNavigator ToolBar
        {
            get { return this.bindingNavigator; }
        }

        private void GetPageCount()
        {
            if (this.NMax > 0)
            {
                this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.NMax) / Convert.ToDouble(this.PageSize)));
            }
            else
            {
                this.PageCount = 0;
            }
        }

        #endregion

        #region 绑定方法

        /// <summary>
        /// 翻页控件数据绑定的方法
        /// </summary>
        public void Bind()
        {
            if (this.EventPaging != null)
            {
                this.NMax = this.EventPaging(new EventPagingArg(this.PageCurrent));
            }

            if (this.PageCurrent > this.PageCount)
            {
                this.PageCurrent = this.PageCount;
            }
            if (this.PageCount == 1)
            {
                this.PageCurrent = 1;
            }
            lblPageCount.Text = this.PageCount.ToString();
            this.lblMaxPage.Text = "共"+this.NMax.ToString()+"条记录";
            this.txtCurrentPage.Text = this.PageCurrent.ToString();

            if (this.PageCurrent == 1)
            {
                this.btnPrev.Enabled = false;
                this.btnFirst.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
                btnFirst.Enabled = true;
            }

            if (this.PageCurrent == this.PageCount)
            {
                this.btnLast.Enabled = false;
                this.btnNext.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (this.NMax == 0)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnFirst.Enabled = false;
                btnPrev.Enabled = false;
            }
        }

        #endregion

        #region 交互事件

        private void btnFirst_Click(object sender, EventArgs e)
        {
            PageCurrent = 1;
            this.Bind();
        }

        /// <summary>
        /// “上一页”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            PageCurrent -= 1;
            if (PageCurrent <= 0)
            {
                PageCurrent = 1;
            }
            this.Bind();
        }

        /// <summary>
        /// “下一页”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.PageCurrent += 1;
            if (PageCurrent > PageCount)
            {
                PageCurrent = PageCount;
            }
            this.Bind();
        }

        /// <summary>
        /// “尾页”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            PageCurrent = PageCount;
            this.Bind();
        }

        /// <summary>
        /// “GO”跳转按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (this.txtCurrentPage.Text != null && txtCurrentPage.Text != "")
            {
                if (_regex.Match(txtCurrentPage.Text).Success)
                {
                    if (Int32.TryParse(txtCurrentPage.Text, out _pageCurrent))
                    {
                        this.Bind();
                    }
                    else
                    {
                        //Common.MessageProcess.ShowError("输入数字格式错误！");
                    }
                }
            }
        }

        /// <summary>
        /// 回车跳转事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
                {
                    if (_regex.Match(txtCurrentPage.Text).Success)
                    {
                        if (Int32.TryParse(txtCurrentPage.Text, out _pageCurrent))
                        {
                            this.Bind();
                        }
                    }
                }
        }
        #endregion

    }
    

    /// <summary>
    /// 自定义事件数据基类
    /// </summary>
    /// <remarks>
    /// ------------------------------------------------------------------------------
    /// 描  述：
    /// 版本号：1.0.0.1
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </remarks>
    /// <example>
    /// [示例代码在这里写入]
    /// </example>
    public class EventPagingArg : EventArgs
    {
        private int _intPageIndex;
        public EventPagingArg(int PageIndex)
        {
            _intPageIndex = PageIndex;
        }
    }
   
}
