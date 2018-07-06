using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DormManage.BLL.UserManage
{
    public class RoleBLL
    {
        private TB_RoleDAL _mTB_RoleDAL=null;
        private TB_RoleConnectModuleDAL _mTB_RoleConnectModuleDAL = null;
        private Database _db;
        private DbConnection _connection;
        private DbTransaction _tran;

        public RoleBLL()
        {
            _mTB_RoleDAL = new TB_RoleDAL();
            _mTB_RoleConnectModuleDAL = new TB_RoleConnectModuleDAL();
        }


        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="tb_Role"></param>
        /// <param name="lstModuleID"></param>
        /// <returns></returns>
        private int Add(TB_Role tb_Role, List<int> lstModuleID)
        {
            TB_RoleConnectModule mTB_RoleConnectModule = null;
            int intRoleID = 0;
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                //添加角色信息
                intRoleID = _mTB_RoleDAL.Create(tb_Role, _tran, _db);
                //添加角色关联模块信息
                foreach (var item in lstModuleID)
                {
                    mTB_RoleConnectModule = new TB_RoleConnectModule()
                    {
                        RoleID = intRoleID,
                        ModuleID = item,
                    };
                    _mTB_RoleConnectModuleDAL.Create(mTB_RoleConnectModule, _tran, _db);
                }
                //提交事务
                _tran.Commit();
            }
            catch
            {
                //回滚事务
                _tran.Rollback();
            }
            finally
            {
                //关闭连接
                _connection.Close();
            }
            return intRoleID;
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="tb_Role"></param>
        /// <param name="lstModuleID"></param>
        private void Update(TB_Role tb_Role, List<int> lstModuleID)
        {
            TB_RoleConnectModule mTB_RoleConnectModule = null;
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                //更新角色信息
                _mTB_RoleDAL.Edit(tb_Role, _tran, _db);
                //删除角色关联模块信息
                _mTB_RoleConnectModuleDAL.Delete(tb_Role.ID, _tran, _db);
                //添加角色关联模块信息
                foreach (var item in lstModuleID)
                {
                    mTB_RoleConnectModule = new TB_RoleConnectModule()
                    {
                        RoleID = tb_Role.ID,
                        ModuleID = item,
                    };
                    _mTB_RoleConnectModuleDAL.Create(mTB_RoleConnectModule, _tran, _db);
                }
                //提交事务
                _tran.Commit();
            }
            catch
            {
                //回滚事务
                _tran.Rollback();
            }
            finally
            {
                //关闭连接
                _connection.Close();
            }
        }


        /// <summary>
        /// 编辑角色信息
        /// </summary>
        /// <param name="tb_Role"></param>
        /// <param name="lstDormAreaID"></param>
        /// <returns></returns>
        public string Edit(TB_Role tb_Role, List<int> lstModuleID)
        {
            string strErrMsg = string.Empty;
            TB_Role mTB_Role = null;
            mTB_Role = _mTB_RoleDAL.GetTable(tb_Role.Name,tb_Role.SiteID);
            //编辑
            if (tb_Role.ID > 0)
            {
                if (mTB_Role != null && mTB_Role.ID != tb_Role.ID)
                {
                    strErrMsg = "该角色已存在！";
                    return strErrMsg;
                }
                this.Update(tb_Role, lstModuleID);
            }
            //添加
            else
            {
                if (mTB_Role != null)
                {
                    strErrMsg = "该角色已存在！";
                    return strErrMsg;
                }
                this.Add(tb_Role, lstModuleID);
            }
            return strErrMsg;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Role"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_Role tb_Role,ref Pager pager)
        {
            return _mTB_RoleDAL.GetTable(tb_Role,ref pager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intRoleID"></param>
        /// <returns></returns>
        public TB_Role GetByID(int intRoleID)
        {
            return _mTB_RoleDAL.Get(intRoleID);
        }
    }
}
