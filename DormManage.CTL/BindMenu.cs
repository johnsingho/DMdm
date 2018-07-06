using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.ExtendTree
{
    public class BindMenu
    {
        #region [˽�б���]

        private DataTable _dtDataSource = null;//����ԴTABLE
        private DataTable _ChildDataSource = null;//��ǰ�û�ӵ�е�Ȩ������Դ
        private DataSet _dsDataSource = null;//����ԴSET

        private string _ColID = "ID";
        private string _ColParent = "PARENT";
        private string _ColUrl = "URL";
        private string _ColTitle = "TITLE";
        private string _ColImageUrl = "IMAGENAME";

        private Menu _Tree = null;

        #endregion

        #region [���캯��]
        /// <summary>
        /// �󶨲˵��ṹ
        /// </summary>
        public BindMenu(Menu tree)
        {
            if (tree == null)
            {
                throw new Exception("�趨���ݲ���Ϊ�գ�");
            }
            else
            {
                //�趨��ǰ��
                this._Tree = tree;
            }
        }
        #endregion

        #region [��������]
        #region [����Դ]
        /// <summary>
        /// �˵��ṹ������Դ
        /// </summary>
        public DataTable DataSouce
        {
            get
            {
                return this._dtDataSource;
            }
            set
            {
                this._dtDataSource = value;
            }
        }

        /// <summary>
        /// �˵��ṹ��������Դ
        /// </summary>
        public DataTable ChildDataSouce
        {
            get
            {
                return this._ChildDataSource;
            }
            set
            {
                this._ChildDataSource = value;
            }
        }

        /// <summary>
        /// �˵��ṹ������Դ
        /// </summary>
        public DataSet DataSouce_DS
        {
            get
            {
                return this._dsDataSource;
            }
            set
            {
                this._dsDataSource = value;
            }
        }
        #endregion
            #region [�˵��ֶ�ȷ��]
        /// <summary>
        /// �趨�нӵ��ϲ��ϵ��
        /// </summary>
        public String NodeIDCol
        {
            get 
            {
                return this._ColID;
            }
            set
            {
                this._ColID = value;
            }
        }

        /// <summary>
        /// �˵�ͼƬ
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return _ColImageUrl;
            }
            set
            {
                _ColImageUrl = value;
            }
        }

        /// <summary>
        /// �󶨸��ڵ���ֶ����ƣ�
        /// </summary>
        public string NodeParentCol
        {
            get
            {
                return this._ColParent;
            }
            set 
            {
                this._ColParent = value;
            }
        }

        /// <summary>
        /// �󶨱�����ֶ����ƣ�
        /// </summary>
        public string NodeTitleCol
        {
            get
            {
                return this._ColTitle;
            }
            set
            {
                this._ColTitle = value;
            }
        }

        /// <summary>
        /// ��url���ֶ����ƣ�
        /// </summary>
        public string NodeUrlCol
        {
            get
            {
                return this._ColUrl;
            }
            set
            {
                this._ColUrl = value;
            }
        }
            #endregion
        #endregion

        #region[�ӿڷ���]


        /// <summary>
        /// �󶨲˵����ݣ����û���趨����Դ�����趨DataSourece
        /// </summary>
        public void Bind()
        {
            if (this._dsDataSource == null && this._dtDataSource == null)
            {
                throw new Exception("û������Դ�������ò˵�DataSouce��");
            }
            else if (this._dsDataSource != null && this._dtDataSource ==null)
            {
                this.BindDataSet();
            }
            else if (this._dsDataSource == null && this._dtDataSource != null)
            {
                this.BindDataTable();
            }
            else
            {
                throw new Exception("�ظ�������Դ��DataSouce������ȷ�Ͻ�ָ��һ�Σ�");
            }

        }

        #endregion

        #region[˽�з���]
        /// <summary>
        /// ��dataTable������Դ
        /// </summary>
        private void BindDataTable()
        {
            if (this._dtDataSource.Columns.Count < 3)
            {
                throw new Exception("����Դ�ֶ��������ܹ����а󶨣���ȷ�ϣ�");
            }
            else if (! this._dtDataSource.Columns.Contains(this._ColParent))
            {
                throw new Exception("�˵��ĸ����ֶ�ָ������");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColTitle))
            {
                throw new Exception("�˵��ı����ֶ�ָ������");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColUrl))
            {
                throw new Exception("�˵��������ֶ�ָ������");
            }
            else
            {
                //ʵ�а�
                DoBind();
            }
        }

        /// <summary>
        /// ��dataTable������Դ
        /// </summary>
        private void BindDataSet()
        {
            if (this._dsDataSource.Tables.Count == 0)
            {
                throw new Exception("����Դ������");
            }
            else
            {
                this._dtDataSource = this._dsDataSource.Tables[0];
                //��Ϊ�ղŰ�
                if (this._dtDataSource != null)
                {
                    this.BindDataTable();
                }
            }
        }

        /// <summary>
        /// ʵʩ��
        /// </summary>
        private void DoBind()
        {
            addTree(0, (MenuItem)null);
        }

        /// <summary>
        /// �ݹ���ã��趨�ӽڵ�
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="pNode"></param>
        private void addTree(int ParentID, MenuItem pNode)
        {
            DataView dvTree = new DataView(this._dtDataSource);

            //�������ݣ�ѡ��ǰ������µ������ӽ��   
            dvTree.RowFilter =string.Format("[{0}] = {1}", this._ColParent, ParentID);

            if (this.ChildDataSouce != null && dvTree.Count == 0)
            {
                DataView dvTree1 = new DataView(this.ChildDataSouce);
                //�������ݣ�ѡ��ǰ������µ������ӽ��   
                dvTree1.RowFilter = string.Format("[{0}] = {1}", this._ColID, ParentID);
                if (dvTree1.Count == 0)
                {
                    //��ǰ�û���ӵ�и�ģ�����Ȩ��ʱ������ʾ��ģ��
                    this._Tree.Items.Remove(pNode);
                }
            }

            //ѭ����ǰ�����ӽ��   
            foreach (DataRowView Row in dvTree)
            {
                MenuItem node = new MenuItem();


                //�����ǰ���Ϊ�����   
                if (pNode == null)
                {
                    //��ʾ����������Ϣ������this._Tree��   
                    node.Text = Row[this._ColTitle].ToString();

                    //���Ϊ�����   
                    this._Tree.Items.Add(node);

                    //�ݹ���ã��ѵ�ǰ�����Ϊ����������������ӽ��   
                    addTree(Int32.Parse(Row[this._ColID].ToString()), node);
                }


                //�����ǰ���Ϊ�ӽ��   
                else
                {
                    //��ʾ����������Ϣ���������ӵ�ַ   
                    node.Text = Row[this._ColTitle].ToString();
                    node.NavigateUrl = Row[this._ColUrl].ToString();
                    //���Ӳ˵�ͼƬ��ַ
                    if (this.DataSouce.Columns.Contains(this.ImageUrl))
                    {
                        node.ImageUrl =  Convert.ToString( Row[this.ImageUrl]);
                    }

                    //���Ϊ�ӽ��   
                    pNode.ChildItems.Add(node);

                    //�ݹ���ã��ѵ�ǰ�����Ϊ����������������ӽ��   
                    addTree(Int32.Parse(Row[this._ColID].ToString()), node);
                }
            }
        }   

        #endregion
    }
}
