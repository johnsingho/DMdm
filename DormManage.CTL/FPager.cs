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
    public delegate int EventPagingHandler(EventPagingArg e); //����¼�ί������
    /// <summary>
    /// Form��ҳ�ؼ�
    /// </summary>
    /// <remarks>
    /// ------------------------------------------------------------------------------
    /// ��  ����Ϊ�ͻ����ṩ��ҳ�ؼ������PAGER����з�ҳ��ѯ
    /// �汾�ţ�
    /// ��  �ߣ�
    /// ��  �ڣ�
    /// ��  �ģ�
    /// ԭ  ��
    /// ------------------------------------------------------------------------------
    /// </remarks>
    /// <example>
    /// [ʾ������������д��]
    /// </example>
    /// <param name="e"></param>
    /// <returns></returns>
    public partial class FPager : UserControl
    {
        public FPager()
        {
            InitializeComponent();
        }
        public event EventPagingHandler EventPaging;     //ע���¼�
        private Regex _regex = new Regex(@"^[1-9]\d?$"); //��תҳ���Ƿ�Ϊ������֤
        private int _pageSize = 20;                      //ÿҳ��ʾ�ļ�¼��
        private int _nMax = 0;                           //�ܼ�¼
        private int _pageCount = 0;                      //��ҳ��
        private int _pageCurrent = 0;                    //��ǰҳ

        #region ����

        /// <summary>
        /// ÿҳ��ʾ��¼��
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
        /// �ܼ�¼��
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
        /// ҳ��=�ܼ�¼��/ÿҳ��ʾ��¼��
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// ��ǰҳ��
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

        #region �󶨷���

        /// <summary>
        /// ��ҳ�ؼ����ݰ󶨵ķ���
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
            this.lblMaxPage.Text = "��"+this.NMax.ToString()+"����¼";
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

        #region �����¼�

        private void btnFirst_Click(object sender, EventArgs e)
        {
            PageCurrent = 1;
            this.Bind();
        }

        /// <summary>
        /// ����һҳ����ť����¼�
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
        /// ����һҳ����ť����¼�
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
        /// ��βҳ����ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            PageCurrent = PageCount;
            this.Bind();
        }

        /// <summary>
        /// ��GO����ת��ť����¼�
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
                        //Common.MessageProcess.ShowError("�������ָ�ʽ����");
                    }
                }
            }
        }

        /// <summary>
        /// �س���ת�¼�
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
    /// �Զ����¼����ݻ���
    /// </summary>
    /// <remarks>
    /// ------------------------------------------------------------------------------
    /// ��  ����
    /// �汾�ţ�1.0.0.1
    /// ��  �ߣ�
    /// ��  �ڣ�
    /// ��  �ģ�
    /// ԭ  ��
    /// ------------------------------------------------------------------------------
    /// </remarks>
    /// <example>
    /// [ʾ������������д��]
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
