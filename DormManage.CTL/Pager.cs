using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace ExtendGridView
{
    /// <summary>
    /// �Զ����ҳ�ؼ�
    /// </summary>
    [ToolboxData("<{0}:Pager runat=\"server\"></{0}:Pager>")]
    public class Pager : WebControl, IPostBackEventHandler, INamingContainer
    {

        #region �ص��¼�
        private static readonly object EventCommand = new object();

        public event CommandEventHandler Command
        {
            add { Events.AddHandler(EventCommand, value); }
            remove { Events.RemoveHandler(EventCommand, value); }
        }

        protected virtual void OnCommand(CommandEventArgs e)
        {
            CommandEventHandler clickHandler = (CommandEventHandler)Events[EventCommand];
            if (clickHandler != null) clickHandler(this, e);
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            OnCommand(new CommandEventArgs(this.UniqueID, Convert.ToInt32(eventArgument)));
        }
        #endregion

        #region ��ҳ�������
        private double itemCount; // �ܼ�¼��
        private int pageSize = 15; // ��ҳ��С
        private int pageCount; // ��ҳ����

        [Browsable(false)]
        public double ItemCount
        {
            get { return itemCount; }
            set
            {
                itemCount = value;

                double divide = ItemCount / PageSize;
                double ceiled = System.Math.Ceiling(divide);
                PageCount = Convert.ToInt32(ceiled);
            }
        }

        [Browsable(false)]
        public int CurrentIndex
        {
            get
            {
                if (ViewState["aspnetPagerCurrentIndex"] == null)
                {
                    ViewState["aspnetPagerCurrentIndex"] = 1;
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(ViewState["aspnetPagerCurrentIndex"]);
                }
            }
            set { ViewState["aspnetPagerCurrentIndex"] = value; }
        }

        [Category("��ҳ����"),
         Description("��ȡ�����÷�ҳ��С")]
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        [Browsable(false)]
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }
        #endregion

        #region ��ҳ��ʾ��Ϣ����
        //private string records = "ÿҳ{0}����¼ ��{1}����¼";
        //private string pages = "��{0}ҳ ��{1}ҳ";
        private string records = "The {0} records, Total:{1} records";
        private string pages = "The {0} pages, Total:{1} pages";
        private string first = "<input type=\"button\" name=\"ungoindex\" class=\"ungoindex\" value=\"\"  disabled=\"disabled\"/>";
        private string firstEnable = "<input type=\"button\" name=\"goindex\" class=\"goindex\" value=\"\" />";

        private string last = "<input type=\"button\" name=\"ungolast\" class=\"ungolast\" value=\"\"  disabled=\"disabled\"/>";
        private string lastEnable = "<input type=\"button\" name=\"golast\" class=\"golast\" value=\"\"/>";

        private string previous = "<input type=\"button\" name=\"unprev\" class=\"unprev\" value=\"\" disabled=\"disabled\" />";
        private string previousEnable = "<input type=\"button\" name=\"prev\" class=\"prev\" value=\"\" />";

        private string next = "<input type=\"button\" name=\"unnext\" class=\"unnext\" value=\"\" disabled=\"disabled\" />";
        private string nextEnable = "<input type=\"button\" name=\"next\" class=\"next\" value=\"\"  />";
        [Category("��ҳ����"), Description("�����ҳ�ؼ���ǰҳ����ҳ���������ʽ")]
        public string PageClause
        {
            get { return pages; }
            set { pages = value; }
        }
        [Category("��ҳ����"), Description("�����ҳ�ؼ���ҳ��С���ܼ�¼���������ʽ")]
        public string RecordClause
        {
            get { return records; }
            set { records = value; }
        }

        [Category("��ҳ����"), Description("����βҳ�����ӷ���")]
        public string LastClause
        {
            get { return last; }
            set { last = value; }
        }

        [Category("��ҳ����"), Description("������ҳ�����ӷ���")]
        public string FirstClause
        {
            get { return first; }
            set { first = value; }
        }

        [Category("��ҳ����"), Description("������һҳ�����ӷ���")]
        public string PreviousClause
        {
            get { return previous; }
            set { previous = value; }
        }

        [Category("��ҳ����"), Description("������һҳ�����ӷ���")]
        public string NextClause
        {
            get { return next; }
            set { next = value; }
        }

        #endregion

        #region �������
        // ��ȡ��ҳ��������ʾ��Ϣ
        private string GetAlternativeText(int index)
        {
            return string.Format(" title=\"{0}\"", index);
        }
        //��ҳ����
        private string RenderFirst()
        {
            string templateCell = "<td class=\"PagerOtherPageCells\"><a onclick=\"{0}\">" + firstEnable + "</a></td>";
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, "1"));
        }
        //��ҳ������ʱ���ı�
        private string RenderDisabledFirst()
        {
            string templateCell = "<td>" + first + "</td>";
            return templateCell;
        }
        //βҳ����
        private string RenderLast()
        {
            string templateCell = "<td class=\"PagerOtherPageCells\"><a onclick=\"{0}\">" + lastEnable + "</a></td>";
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, PageCount.ToString()));
        }
        //βҳ������ʱ���ı�
        private string RenderDisabledLast()
        {
            string templateCell = "<td>" + last + "</td>";
            return templateCell;
        }
        //��һҳ����
        private string RenderPrevious()
        {
            string templateCell = "<td class=\"PagerOtherPageCells\"><a onclick=\"{0}\">" + previousEnable + "</a></td>";
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, (CurrentIndex - 1).ToString()));
        }
        //��һҳ������ʱ���ı�
        private string RenderDisabledPrevious()
        {
            string templateCell = "<td>" + previous + "</td>";
            return templateCell;
        }
        //��һҳ����
        private string RenderNext()
        {
            string templateCell = "<td class=\"PagerOtherPageCells\"><a onclick=\"{0}\"> " + nextEnable + "</a></td>";
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, (CurrentIndex + 1).ToString()));
        }
        //��һҳ������ʱ���ı�
        private string RenderDisabledNext()
        {
            string templateCell = "<td>" + next + "</td>";
            return templateCell;
        }
        #endregion

        #region ��д�ؼ��������
        protected override void Render(HtmlTextWriter writer)
        {

            if (Page != null) Page.VerifyRenderingInServerForm(this);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "3");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "PagerContainerTable");

            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "PagerInfoCell");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write(string.Format(RecordClause, PageSize, ItemCount) + " "
                + string.Format(PageClause, CurrentIndex.ToString(), PageCount.ToString()) + " ");
            writer.RenderEndTag();

            if (CurrentIndex != 1)
                writer.Write(RenderFirst());
            else
                writer.Write(RenderDisabledFirst());

            if (CurrentIndex != 1)
                writer.Write(RenderPrevious());
            else
                writer.Write(RenderDisabledPrevious());

            if (CurrentIndex < PageCount)
                writer.Write(RenderNext());
            else
                writer.Write(RenderDisabledNext());

            if (CurrentIndex < PageCount)
                writer.Write(RenderLast());
            else
                writer.Write(RenderDisabledLast());

            writer.RenderEndTag();

            writer.RenderEndTag();

            base.Render(writer);
        }
        #endregion
    }
}