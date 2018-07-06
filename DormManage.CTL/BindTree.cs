using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.ExtendTree
{
    public class BindTree
    {
        #region [˽�б���]

        private DataTable _dtDataSource = null;//����ԴTABLE
        private DataSet _dsDataSource = null;//����ԴSET

        private string _ColID = "ID";
        private string _ColParent = "PARENT";
        private string _ColUrl = "URL";
        private string _ColTitle = "TITLE";

        private TreeView _Tree = null;
        private bool _isShowUrl = true;

        #endregion

        #region [���캯��]
        /// <summary>
        /// �����νṹ
        /// </summary>
        public BindTree(TreeView tree)
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

        /// <summary>
        /// ��  ���������νṹ
        /// ��  �ߣ�
        /// ʱ  �䣺
        /// ��  �ģ�
        /// ԭ  ��
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="bolShowUrl">�Ƿ���ʾ������</param>
        public BindTree(TreeView tree, bool bolShowUrl)
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
            this._isShowUrl = bolShowUrl;
        }
        #endregion

        #region [��������]
        #region [����Դ]
        /// <summary>
        /// ���νṹ������Դ
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
        /// ���νṹ������Դ
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
        #region [�����ֶ�ȷ��]
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
        /// ʵ�а󶨣���δ�趨����Դ����ָ��DataSource����
        /// </summary>
        public void Bind()
        {
            if (this._dsDataSource == null && this._dtDataSource == null)
            {
                throw new Exception("û������Դ������������DataSouce��");
            }
            else if (this._dsDataSource != null && this._dtDataSource == null)
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
        public void Bind(int parentID)
        {
            if (this._dsDataSource == null && this._dtDataSource == null)
            {
                throw new Exception("û������Դ������������DataSouce��");
            }
            else if (this._dsDataSource != null && this._dtDataSource == null)
            {
                addMenuTree(parentID, (TreeNode)null);
            }
            else if (this._dsDataSource == null && this._dtDataSource != null)
            {
                addMenuTree(parentID, (TreeNode)null);
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
            else if (!this._dtDataSource.Columns.Contains(this._ColParent))
            {
                throw new Exception("���εĸ����ֶ�ָ������");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColTitle))
            {
                throw new Exception("���εı����ֶ�ָ������");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColUrl))
            {
                throw new Exception("���ε������ֶ�ָ������");
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
            addTree(0, (TreeNode)null);
        }

        /// <summary>
        /// �ݹ���ã��趨�ӽڵ�
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="pNode"></param>
        private void addTree(int ParentID, TreeNode pNode)
        {
            DataView dvTree = new DataView(this._dtDataSource);

            //�������ݣ�ѡ��ǰ������µ������ӽ��   
            dvTree.RowFilter = string.Format("[{0}] = {1}", this._ColParent, ParentID);


            //ѭ����ǰ�����ӽ��   
            foreach (DataRowView Row in dvTree)
            {
                TreeNode node = new TreeNode();


                //�����ǰ���Ϊ�����   
                if (pNode == null)
                {
                    //��ʾ����������Ϣ������this._Tree��   
                    node.Text = Row[this._ColTitle].ToString();
                    node.Value = Row[this._ColID].ToString();
                    //if (!_isShowUrl)
                    //{
                        node.SelectAction = TreeNodeSelectAction.None;
                    //}

                    //���Ϊ�����   
                    this._Tree.Nodes.Add(node);

                    //�ݹ���ã��ѵ�ǰ�����Ϊ����������������ӽ��   
                    addTree(Int32.Parse(Row[this._ColID].ToString()), node);
                }


                //�����ǰ���Ϊ�ӽ��   
                else
                {
                    //��ʾ����������Ϣ���������ӵ�ַ   
                    node.Text = Row[this._ColTitle].ToString();
                    node.Value = Row[this._ColID].ToString();
                    node.NavigateUrl = Row[this._ColUrl].ToString();
                    if (!_isShowUrl)
                    {
                        node.SelectAction = TreeNodeSelectAction.None;
                    }
                    //���Ϊ�ӽ��   
                    pNode.ChildNodes.Add(node);

                    //�ݹ���ã��ѵ�ǰ�����Ϊ����������������ӽ��   
                    addTree(Int32.Parse(Row[this._ColID].ToString()), node);
                }
            }
        }

        /// <summary>
        /// �ݹ���ã��趨�ӽڵ�
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="pNode"></param>
        private void addMenuTree(int ParentID, TreeNode pNode)
        {
             DataView dvTree = new DataView(this._dtDataSource);
             
            //�����ǰ���Ϊ�����   
             if (pNode == null)
             {
                 TreeNode node = new TreeNode();
                 //�������ݣ�ѡ��ǰ������µ������ӽ��   
                 dvTree.RowFilter = string.Format("[{0}] = {1}", this._ColID, ParentID);
                 if (dvTree.Count == 0)
                 {
                     return;
                 }
                 DataRowView Row = dvTree[0];
                 //��ʾ����������Ϣ������this._Tree��   
                 node.Text = Row[this._ColTitle].ToString();
                 node.Value = Row[this._ColID].ToString();//
                 //node.NavigateUrl = Row[this._ColUrl].ToString();
                  node.SelectAction = TreeNodeSelectAction.None;
                 //���Ϊ�����   
                 this._Tree.Nodes.Add(node);

                 //�ݹ���ã��ѵ�ǰ�����Ϊ����������������ӽ��   
                 addMenuTree(Int32.Parse(Row[this._ColID].ToString()), node);
             }
             else
             {
                 dvTree.RowFilter = string.Format("[{0}] = {1}", this._ColParent, ParentID);
                 //ѭ����ǰ�����ӽ��   
                 foreach (DataRowView Row in dvTree)
                 {
                     TreeNode node = new TreeNode();
                     //��ʾ����������Ϣ���������ӵ�ַ   
                     node.Text = Row[this._ColTitle].ToString();
                     node.Value = Row[this._ColID].ToString();
                     node.NavigateUrl = Row[this._ColUrl].ToString();
                     //���Ϊ�ӽ��   
                     pNode.ChildNodes.Add(node);
                 }
             }
        }

        #endregion
    }
}
