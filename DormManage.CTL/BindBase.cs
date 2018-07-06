using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DormManage.Web.UI
{
   public abstract class BindBase
    {
        #region [私有变量]

        private DataTable _dtDataSource = null;//数据源TABLE
        private DataTable _ChildDataSource = null;//当前用户拥有的权限数据源
        private DataSet _dsDataSource = null;//数据源SET

        private string _ColID = "ID";
        private string _ColParent = "PARENT";
        private string _ColUrl = "URL";
        private string _ColTitle = "TITLE";
        private string _ColImageUrl = "IMAGENAME";


        #endregion

        #region [构造函数]
        /// <summary>
        /// 绑定菜单结构
        /// </summary>
        public BindBase()
        {

        }
        #endregion

        #region [公共属性]
        #region [数据源]
        /// <summary>
        /// 菜单结构的数据源
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
        /// 菜单结构的子数据源
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
        /// 菜单结构的数据源
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

       /// <summary>
       /// 返回结果
       /// </summary>
        public object Result { get; set; }

        #endregion
            #region [菜单字段确认]
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
        /// 菜单图片
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
        /// 绑定菜单数据，如果没有设定数据源，请设定DataSourece
        /// </summary>
        public void Bind()
        {
            if (this._dsDataSource == null && this._dtDataSource == null)
            {
                throw new Exception("没有数据源，请设置菜单DataSouce！");
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
                throw new Exception("菜单的父亲字段指定错误！");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColTitle))
            {
                throw new Exception("菜单的标题字段指定错误！");
            }
            else if (!this._dtDataSource.Columns.Contains(this._ColUrl))
            {
                throw new Exception("菜单的链接字段指定错误！");
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
        protected abstract void DoBind();

        #endregion
    }
}
