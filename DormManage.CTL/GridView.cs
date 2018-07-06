using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExtendGridView
{
    /// <summary>
    /// GridView ��չ�ؼ�
    /// @author:jianyi0115@163.com
    /// </summary>
     public class GridView : System.Web.UI.WebControls.GridView
    {       
        private bool _enableEmptyContentRender = true ;
        /// <summary>
        /// �Ƿ�����Ϊ��ʱ��ʾ������
        /// </summary>
        public bool EnableEmptyContentRender
        {
            set { _enableEmptyContentRender = value; }
            get { return _enableEmptyContentRender; }
        }

        private string _EmptyDataCellCssClass ;
        /// <summary>
        /// Ϊ��ʱ��Ϣ��Ԫ����ʽ��
        /// </summary>
        public string EmptyDataCellCssClass
        {
            set { _EmptyDataCellCssClass = value ; }
            get { return _EmptyDataCellCssClass ; }
        }

        /// <summary>
        /// Ϊ��ʱ�������
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderEmptyContent(HtmlTextWriter writer)
        {
            // ԭ��Ϣ�е� ColumnSpan Ϊ this.Columns.Count����Ϊ���������У����� ColumnSpan ����ȷ��
            // �޸�Ϊ ColumnSpan = nVisibleColumnCount
            int nVisibleColumnCount = 0;              // ��¼�ɼ�����
            Table t = new Table(); //create a table
            t.CssClass = this.CssClass; //copy all property
            t.GridLines = this.GridLines;
            t.BorderStyle = this.BorderStyle;
            t.BorderWidth = this.BorderWidth;
            t.CellPadding = this.CellPadding;
            t.CellSpacing = this.CellSpacing;

            t.HorizontalAlign = this.HorizontalAlign;

            t.Width = this.Width;

            t.CopyBaseAttributes(this);

            TableRow row = new TableRow();
            t.Rows.Add(row);

            foreach (DataControlField f in this.Columns) //generate table header
            {
                if( f.Visible == true ){
                    TableCell cell = new TableCell();
                    cell.Text = f.HeaderText;
                    cell.Width = f.HeaderStyle.Width;
                    cell.Height = f.HeaderStyle.Height;
                    cell.CssClass = f.HeaderStyle.CssClass;
                  
                    row.Cells.Add(cell);
                    // �ɼ�������
                    nVisibleColumnCount++;
                }
            }
            TableRow row2 = new TableRow();
            t.Rows.Add(row2);

            TableCell msgCell = new TableCell();
            msgCell.CssClass = this._EmptyDataCellCssClass;

            if (this.EmptyDataTemplate != null) //the second row, use the template
            {
                this.EmptyDataTemplate.InstantiateIn(msgCell);
            }
            else //the second row, use the EmptyDataText
            {
                msgCell.Text = this.EmptyDataText;
            }

            msgCell.HorizontalAlign = HorizontalAlign.Center;
            msgCell.ColumnSpan = nVisibleColumnCount;

            row2.Cells.Add(msgCell);

            t.RenderControl(writer);
       }

        protected override void  Render(HtmlTextWriter writer)
        {
            //foreach (GridViewRow row in this.Rows)
            //{
            //    foreach (TableCell item in row.Cells)
            //    {
            //        if (item.Text.Length>2)
            //        {
            //            string strText = item.Text;
            //            item.Text = strText.Substring(0, 2) + "...";
            //        }
            //    }
            //}
            if ( _enableEmptyContentRender && ( this.Rows.Count == 0 || this.Rows[0].RowType == DataControlRowType.EmptyDataRow) )
            {
                RenderEmptyContent(writer);
            }
            else
            {
                base.Render(writer);
            }
        }    

    }
}
