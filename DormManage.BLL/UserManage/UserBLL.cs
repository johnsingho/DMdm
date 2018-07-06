using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Framework.Entrypt;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DormManage.BLL.UserManage
{
    public class UserBLL
    {
        private CommonManager _mCommonManager = null;
        private TB_UserDAL _mTB_UserDAL = null;
        private TB_SystemAdminDAL _mTB_SystemAdminDAL = null;
        private TB_UserConnectDormAreaDAL _mTB_UserConnectDormAreaDAL = null;
        private Database _db;
        private DbConnection _connection;
        private DbTransaction _tran;

        public UserBLL()
        {
            _mCommonManager = new CommonManager();
            _mTB_UserDAL = new TB_UserDAL();
            _mTB_SystemAdminDAL = new TB_SystemAdminDAL();
            _mTB_UserConnectDormAreaDAL = new TB_UserConnectDormAreaDAL();
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="tb_User"></param>
        /// <param name="lstDormAreaID"></param>
        /// <returns></returns>
        private int Add(TB_User tb_User, List<int> lstDormAreaID)
        {
            TB_UserConnectDormArea mTB_UserConnectDormArea = null;
            int intUserID = 0;
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                //添加用户信息
                intUserID = _mTB_UserDAL.Create(tb_User, _tran, _db);
                //添加用户关联宿舍区信息
                foreach (var item in lstDormAreaID)
                {
                    mTB_UserConnectDormArea = new TB_UserConnectDormArea()
                    {
                        UserID = intUserID,
                        DormAreaID = item,
                    };
                    _mTB_UserConnectDormAreaDAL.Create(mTB_UserConnectDormArea, _tran, _db);
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
            return intUserID;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="tb_User"></param>
        /// <param name="lstDormAreaID"></param>
        private void Update(TB_User tb_User, List<int> lstDormAreaID)
        {
            TB_UserConnectDormArea mTB_UserConnectDormArea = null;
            //启用事务
            _db = DBO.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _tran = _connection.BeginTransaction();
            try
            {
                //更新用户信息
                _mTB_UserDAL.Edit(tb_User, _tran, _db);
                //删除用户关联宿舍区信息
                _mTB_UserConnectDormAreaDAL.Delete(tb_User.ID, _tran, _db);
                //添加用户关联宿舍区信息
                foreach (var item in lstDormAreaID)
                {
                    mTB_UserConnectDormArea = new TB_UserConnectDormArea()
                    {
                        UserID = tb_User.ID,
                        DormAreaID = item,
                    };
                    _mTB_UserConnectDormAreaDAL.Create(mTB_UserConnectDormArea, _tran, _db);
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
        /// 编辑用户信息
        /// </summary>
        /// <param name="tb_User"></param>
        /// <param name="lstDormAreaID"></param>
        /// <returns></returns>
        public string Edit(TB_User tb_User, List<int> lstDormAreaID)
        {
            string strErrMsg = string.Empty;
            TB_User mTB_User = null;
            mTB_User = _mTB_UserDAL.GetUserInfo(tb_User.ADAccount);
            //编辑
            if (tb_User.ID > 0)
            {
                if (mTB_User != null && mTB_User.ID != tb_User.ID)
                {
                    strErrMsg = "该用户添加！";
                    return strErrMsg;
                }
                this.Update(tb_User, lstDormAreaID);
            }
            //添加
            else
            {
                if (mTB_User != null)
                {
                    strErrMsg = "该用户添加！";
                    return strErrMsg;
                }
                this.Add(tb_User, lstDormAreaID);
            }
            return strErrMsg;
        }

        /// <summary>
        /// 根据用户ID获取到用户信息
        /// </summary>
        /// <param name="intUserID"></param>
        /// <returns></returns>
        public TB_User Get(int intUserID)
        {
            return _mTB_UserDAL.GetUserInfo(intUserID);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_User"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_User tb_User, ref Pager pager)
        {
            DataTable dt = _mTB_UserDAL.GetTable(tb_User, ref pager);
            //DataTable dtNew = dt.Clone();
            //DataRow[] drFilter = null;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    DataRow drNew = dtNew.NewRow();
            //    drFilter = dtNew.Select("[ADAccount]='" + dr[TB_User.col_ADAccount] + "'");
            //    if (drFilter.Length > 0)
            //    {
            //        drFilter[0]["DormName"] = drFilter[0]["DormName"] + "，" + dr["DormName"];
            //    }
            //    else
            //    {
            //        drNew.ItemArray = dr.ItemArray;
            //        dtNew.Rows.Add(drNew);
            //    }
            //}
            //if (dtNew.Rows.Count>0)
            //{
            //    pager.TotalRecord = dtNew.Rows.Count;
            //}
            //else
            //{
            //    pager.TotalRecord = 0;
            //}
            return dt;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="intAdminID">管理员ID</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public string ModifyPassword(int intAdminID, string oldPassword, string newPassword)
        {
            string strErrorMsg = string.Empty;
            //加密
            oldPassword = MD5.EncryptStr(oldPassword);
            newPassword = MD5.EncryptStr(newPassword);
            if (_mTB_SystemAdminDAL.GetUserInfo(intAdminID).PassWord != oldPassword)
            {
                strErrorMsg = "Sorry,原密码错误！";
                return strErrorMsg;
            }
            if (_mTB_SystemAdminDAL.Edit(intAdminID, newPassword) <= 0)
            {
                strErrorMsg = "Sorry,修改密码失败！";
                return strErrorMsg;
            }
            return strErrorMsg;
        }

        /// <summary>
        /// 用户登陆验证
        /// </summary>
        /// <param name="strAccout">AD账号</param>
        /// <param name="strPassword">密码</param>
        /// <param name="isAdmin">是否超级管理员</param>
        /// <returns></returns>
        public string UserLogin(string strAccout, string strPassword, bool isAdmin)
        {
            string strErrorMsg = string.Empty;
            TB_User mTB_User = null;
            TB_SystemAdmin mTB_SystemAdmin = null;
            if (!isAdmin)
            {
                mTB_User = _mTB_UserDAL.GetUserInfo(strAccout);
                if (null == mTB_User)
                {
                    strErrorMsg = "Sorry,您未被授权访问本系统，请联系系统管理员！";
                    return strErrorMsg;
                }
                HttpContext.Current.Session[TypeManager.User] = mTB_User;
            }
            else
            {
                mTB_SystemAdmin = _mTB_SystemAdminDAL.GetUserInfo(strAccout, MD5.EncryptStr(strPassword));
                if (null == mTB_SystemAdmin)
                {
                    strErrorMsg = "Sorry,密码错误!";
                    return strErrorMsg;
                }
                else
                {
                    HttpContext.Current.Session[TypeManager.Admin] = mTB_SystemAdmin;
                    return strErrorMsg;
                }
            }
            if (!_mCommonManager.DomainAuthenticateLogin(strAccout, strPassword))
            {
                strErrorMsg = "Sorry,密码错误!";
                return strErrorMsg;
            }
            return strErrorMsg;
        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        public void Remove(string strID)
        {
            Database db = DBO.GetInstance();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction tran = connection.BeginTransaction();
            try
            {
                new TB_UserDAL().DeleteByUserID(strID, tran, db);
                //this.Remove(strID, tran, db);
                tran.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
