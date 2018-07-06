using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

namespace DormManage.BLL.UserManage
{
    public class ExtendTreeBase
    {
        #region [私有变量]

        private DataTable _dtDataSource = null;//数据源TABLE
        private DataSet _dsDataSource = null;//数据源SET

        private string _ColID = "ID";
        private string _ColParent = "PARENT";
        private string _ColUrl = "URL";
        private string _ColTitle = "TITLE";

        public TreeView _Tree = null;

        #endregion

        #region [构造函数]
        public ExtendTreeBase()
        {
 
        }
        /// <summary>
        /// 绑定树形结构
        /// </summary>
        public ExtendTreeBase(TreeView tree)
        {
            if (tree == null)
            {
                throw new Exception("设定内容不能为空！");
            }
            else
            {
                //设定当前树
                this._Tree = tree;
            }
        }
        #endregion

        #region [公共属性]
        #region [数据源]
        /// <summary>
        /// 树形结构的数据源
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
        /// 树形结构的数据源
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
            #region [树形字段确认]
        /// <summary>
        /// 设定承接的上层关系。
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
        /// 绑定父节点的字段名称；
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
        /// 绑定标题的字段名称；
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
        /// 绑定url的字段名称；
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

        #region[接口方法]


        /// <summary>
        /// 实行绑定，若未设定数据源，请指定DataSource属性
        /// 数据源格式：“节点ID”、“父亲节点ID”、“URL”、“节点名称”
        /// </summary>
        public virtual void Bind()
        {
            if (this._dsDataSource == null && this._dtDataSource == null)
            {
                throw new Exception("没有数据源，请设置树形DataSouce！");
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
                throw new Exception("重复设数据源，DataSouce属性请确认仅指定一次！");
            }

        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void ReleaseTree()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region[私有方法]
        /// <summary>
        /// 用dataTable绑定数据源
        /// </summary>
        private void BindDataTable()
        {
            if (this._dtDataSource.Columns.Count < 3)
            {
                throw new Exception("数据源字段数量不能够进行绑定，请确认！");
            }
            else if (! this._dtDataSource.Columns.Contains(this._ColParent))
            {
                throw new Exception("树形的父亲字段指定错误！");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColTitle))
            {
                throw new Exception("树形的标题字段指定错误！");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColUrl))
            {
                throw new Exception("树形的链接字段指定错误！");
            }
            else
            {
                //实行绑定
                DoBind();
            }
        }

        /// <summary>
        /// 用dataTable绑定数据源
        /// </summary>
        private void BindDataSet()
        {
            if (this._dsDataSource.Tables.Count == 0)
            {
                throw new Exception("数据源无内容");
            }
            else
            {
                this._dtDataSource = this._dsDataSource.Tables[0];
                //不为空才绑定
                if (this._dtDataSource != null)
                {
                    this.BindDataTable();
                }
            }
        }

        /// <summary>
        /// 实施绑定
        /// </summary>
        private void DoBind()
        {
            addTree(0,(TreeNode)null);
        }

        /// <summary>
        /// 递归调用，设定子节点
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="pNode"></param>
        private void addTree(int ParentID, TreeNode pNode)
        {
            DataView dvTree = new DataView(this._dtDataSource);

            //过滤数据，选择当前父结点下的所有子结点   
            dvTree.RowFilter =string.Format("[{0}] = {1}", this._ColParent, ParentID);

            //循环当前所有子结点   
            foreach (DataRowView Row in dvTree)
            {
                TreeNode node = new TreeNode();


                //如果当前结点为根结点   
                if (pNode == null)
                {
                    //显示结点的文字信息并加入this._Tree中   
                    node.Text = Row[this._ColTitle].ToString();

                    //添加为根结点   
                    this._Tree.Nodes.Add(node);

                    //递归调用，把当前结点作为根结点继续添加所有子结点   
                    addTree(Int32.Parse(Row[this._ColID].ToString()), node);
                }


                //如果当前结点为子结点   
                else
                {
                    //显示结点的文字信息并加入链接地址   
                    node.Text = Row[this._ColTitle].ToString();
                    node.NavigateUrl = Row[this._ColUrl].ToString();

                    //添加为子结点   
                    pNode.ChildNodes.Add(node);

                    //递归调用，把当前结点作为根结点继续添加所有子结点   
                    addTree(Int32.Parse(Row[this._ColID].ToString()), node);
                }
            }
        }   

        #endregion
    }
}

