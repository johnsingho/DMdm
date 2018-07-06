using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using DormManage.Common;
using DormManage.Models;

namespace DormManage.BLL.UserManage
{
    public class ExtendOpeTree : ExtendTreeBase
    {
        #region [私有变量]

        private DataTable _dtOperation = null;//详细操作的数据源
        private DataTable _dtDefaultCheck = null;//初始化，默认选中的项
        private TreeNodeSelectAction _Action = TreeNodeSelectAction.None;//默认无刷新
        private string _OpeID = "ID";
        private string _OpeName = "NAME";
        #endregion

        #region [公共属性]

        /// <summary>
        /// 绑定树形结构
        /// </summary>
        public ExtendOpeTree(TreeView tree)
        {
            if (tree == null)
            {
                throw new Exception("设定内容不能为空！");
            }
            else
            {
                //设定当前树
                base._Tree = tree;
            }
        }
        /// <summary>
        /// 操作数据源
        /// 新增属性
        /// </summary>
        public DataTable OperationDataSource
        {
            get
            {
                return this._dtOperation;
            }
            set
            {
                this._dtOperation = value;
            }
        }

        /// <summary>
        /// 初始化，默认选中的数据源
        /// 字段列表如下：
        /// 模块ID,选中操作ID，其他
        /// 前两项必须有
        /// </summary>
        public DataTable DefaultCheckSource
        {
            get
            {
                return this._dtDefaultCheck;
            }
            set
            {
                if (value.Columns.Count < 2)
                {
                    throw new Exception("字段个数必须大于2！且第一个指定为模块ID，第二个指定为选中操作ID");
                }
                else
                {
                    this._dtDefaultCheck = value;
                }
            }
        }
        /// <summary>
        /// 树形是否可以点击触发事件
        /// 新增属性，默认无刷新
        /// </summary>
        public TreeNodeSelectAction SelectActionMine
        {
            get
            {
                return this._Action;
            }
            set
            {
                this._Action = value;
            }
        }

        /// <summary>
        /// 设定操作的字段编号，指定在OperationDataSource中
        /// </summary>
        public string OpeIDCol
        {
            get
            {
                return this._OpeID;
            }
            set
            {
                this._OpeID = value;
            }
        }

        /// <summary>
        /// 设定操作的字段名称，指定在OperationDataSource中
        /// </summary>
        public string OpeNameCol
        {
            get
            {
                return this._OpeName;
            }
            set
            {
                this._OpeName = value;
            }
        }

        #endregion

        #region[接口方法]


        /// <summary>
        /// 实行绑定，若未设定数据源，请指定DataSource属性
        /// </summary>
        public override void Bind()
        {
            if (this.DataSouce == null && this.DataSouce_DS == null)
            {
                throw new Exception("没有数据源，请设置树形DataSouce！");
            }
            else if (this.DataSouce_DS != null && this.DataSouce == null)
            {
                this.BindDataSet();
            }
            else if (this.DataSouce_DS == null && this.DataSouce != null)
            {
                this.BindDataTable();
            }
            else
            {
                throw new Exception("重复设数据源，DataSouce属性请确认仅指定一次！");
            }

        }

        /// <summary>
        /// 获得当前树形被选中的模块对应的所有ID;
        /// 返回一个Table，包含两个字段，字段1对应模块ID,字段2对应操作ID
        /// </summary>
        /// <returns></returns>
        public DataTable SelectedSource()
        {
            try
            {
                DataTable dtReturn = null;

                if (this._Tree != null)
                {
                    dtReturn = new DataTable();
                    DataColumn dcMID = new DataColumn(TypeManager.ModuleID);
                    DataColumn dcOID = new DataColumn(TypeManager.OperationID);
                    dtReturn.Columns.Add(dcMID);
                    dtReturn.Columns.Add(dcOID);
                    DataRow dr = null;

                    //只有树形启用checkbox并且被选中时才返回；
                    if (this._Tree.ShowCheckBoxes != TreeNodeTypes.None && _Tree.CheckedNodes.Count > 0)
                    {
                        foreach (TreeNode tn in _Tree.CheckedNodes)
                        {
                            if (tn.ChildNodes.Count == 0)
                            {
                                dr = dtReturn.NewRow();
                                //模块ID
                                dr[TypeManager.ModuleID] = tn.Parent.Value;
                                //操作ID
                                dr[TypeManager.OperationID] = tn.Value;
                                dtReturn.Rows.Add(dr);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("未指定树形对象！");
                }
                return dtReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void ReleaseTree()
        {
            try
            {
                this.DefaultCheckSource = null;
                this.OperationDataSource = null;
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
            if (this.DataSouce.Columns.Count < 3)
            {
                throw new Exception("数据源字段数量不能够进行绑定，请确认！");
            }
            else if (!this.DataSouce.Columns.Contains(this.NodeParentCol))
            {
                throw new Exception("树形的父亲字段指定错误！");
            }
            else if (!this.DataSouce.Columns.Contains(this.NodeTitleCol))
            {
                throw new Exception("树形的标题字段指定错误！");
            }
            //此处有别于基类，该处只应用是否选中项
            else if (!this.DataSouce.Columns.Contains(this.NodeUrlCol))
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
            if (this.DataSouce_DS.Tables.Count == 0)
            {
                throw new Exception("数据源无内容");
            }
            else
            {
                this.DataSouce = this.DataSouce_DS.Tables[0];
                //不为空才绑定
                if (this.DataSouce != null)
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
            addTree(0, (TreeNode)null);
        }

        /// <summary>
        /// 递归调用，设定子节点
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="pNode"></param>
        private void addTree(int ParentID, TreeNode pNode)
        {
            DataView dvTree = new DataView(this.DataSouce);
            //过滤数据，选择当前父结点下的所有子结点   
            dvTree.RowFilter = string.Format("[{0}] = {1}", this.NodeParentCol, ParentID);
            //循环当前所有子结点   
            foreach (DataRowView Row in dvTree)
            {
                TreeNode node = new TreeNode();
                //如果当前结点为根结点   
                if (pNode == null)
                {
                    //显示结点的文字信息并加入this._Tree中   
                    node.Text = Row[this.NodeTitleCol].ToString();
                    node.Value = Row[this.NodeIDCol].ToString();
                    //添加为根结点   
                    this._Tree.Nodes.Add(node);
                    //递归调用，把当前结点作为根结点继续添加所有子结点   
                    addTree(Int32.Parse(Row[this.NodeIDCol].ToString()), node);
                }
                //如果当前结点为子结点   
                else
                {
                    //显示结点的文字信息并加入链接地址   
                    node.Text = Row[this.NodeTitleCol].ToString();
                    //需要活用，暂不赋值
                    node.Value = Row[this.NodeIDCol].ToString();
                    //是否回发
                    node.SelectAction = this.SelectActionMine;
                    DataView dvDefaultCheck = new DataView(this.DefaultCheckSource);
                    //设定是否选中
                    if (this.DefaultCheckSource != null)
                    {
                        dvDefaultCheck.RowFilter = string.Format("[{0}] = {1}",
                            TB_Module.col_ID, node.Value);
                    }
                    if (dvDefaultCheck.Count == 1)
                    {
                        node.Checked = true;
                    }
                    //添加为子结点   
                    pNode.ChildNodes.Add(node);
                    //递归调用，把当前结点作为根结点继续添加所有子结点   
                    addTree(Int32.Parse(Row[this.NodeIDCol].ToString()), node);
                }
            }
        }

        #endregion
    }
}
