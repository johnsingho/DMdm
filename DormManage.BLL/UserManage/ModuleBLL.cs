using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Models;

namespace DormManage.BLL.UserManage
{
    public class ModuleBLL
    {
        private TB_ModuleDAL _mTB_ModuleDAL = null;

        public ModuleBLL()
        {
            _mTB_ModuleDAL = new TB_ModuleDAL();
        }

        /// <summary>
        /// 获取用户拥有的模块
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserModule(int intUserID)
        {
            return _mTB_ModuleDAL.GetUserModule(intUserID);
        }

        /// <summary>
        /// 获取到所有模块
        /// </summary>
        /// <param name="tb_Module"></param>
        /// <returns></returns>
        public DataTable GetAllModule(TB_Module tb_Module)
        {
            return _mTB_ModuleDAL.GetTable(tb_Module).Tables[0];
        }

        public void LoadTreeModule(TreeView tree, int intRoleID)
        {
            ExtendOpeTree mExtendOpeTree = new ExtendOpeTree(tree);
            DataSet ds = new DataSet();
            TB_Module tb_Module = new TB_Module()
            {
                IsActive=1,
                SiteID=System.Web.HttpContext.Current.Session[TypeManager.User]==null?((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).SiteID:
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).SiteID,
            };
            ds = _mTB_ModuleDAL.GetTable(tb_Module);
            //设定关键字段值
            mExtendOpeTree.NodeIDCol = TB_Module.col_ID;
            mExtendOpeTree.NodeParentCol = TB_Module.col_PID;
            mExtendOpeTree.NodeTitleCol = TB_Module.col_Name;
            mExtendOpeTree.NodeUrlCol = TB_Module.col_URL;
            DataSet dsRoleOper = _mTB_ModuleDAL.GetModule(intRoleID);
            if (dsRoleOper != null && dsRoleOper.Tables[0].Rows.Count > 0)
            {
                mExtendOpeTree.DefaultCheckSource = dsRoleOper.Tables[0];
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                mExtendOpeTree.DataSouce = ds.Tables[0];
                mExtendOpeTree.Bind();
            }
            dsRoleOper.Dispose();
        }

    }
}
