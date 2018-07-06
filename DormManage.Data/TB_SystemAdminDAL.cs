using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DormManage.Data.DAL
{
    public class TB_SystemAdminDAL
    {

        /// <summary>
        /// 将datatable转换成list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<TB_SystemAdmin> ConvertTableToList(DataTable dt)
        {
            List<TB_SystemAdmin> lstSystemAdmin = new List<TB_SystemAdmin>();
            TB_SystemAdmin mTB_SystemAdmin = null;
            foreach (DataRow dr in dt.Rows)
            {
                mTB_SystemAdmin = new TB_SystemAdmin();
                mTB_SystemAdmin.ID = Convert.ToInt32(dr[TB_SystemAdmin.col_ID]);
                mTB_SystemAdmin.Account = dr[TB_SystemAdmin.col_Account].ToString();
                mTB_SystemAdmin.SiteID = Convert.ToInt32(dr[TB_SystemAdmin.col_SiteID]);
                mTB_SystemAdmin.PassWord = dr[TB_SystemAdmin.col_PassWord].ToString();
                lstSystemAdmin.Add(mTB_SystemAdmin);
            }
            return lstSystemAdmin;
        }

        /// <summary>
        /// 根据ID获取到超级管理员信息
        /// </summary>
        /// <param name="intAdminID"></param>
        /// <returns></returns>
        public TB_SystemAdmin GetUserInfo(int intAdminID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select * from [TB_SystemAdmin] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND ID = @ID");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.String, intAdminID);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return this.ConvertTableToList(dt).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommandWrapper != null)
                {
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="intAdminID"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public int Edit(int intAdminID, string strPassword)
        {
            DbCommand dbCommandWrapper = null;
            string strSQL = @"UPDATE [TB_SystemAdmin] SET [PassWord]=@PassWord
                                                            WHERE [ID]=@ID";
            StringBuilder strBuilder = new StringBuilder(strSQL);
            try
            {
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, intAdminID);
                db.AddInParameter(dbCommandWrapper, "@PassWord", DbType.String, strPassword);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                return db.ExecuteNonQuery(dbCommandWrapper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommandWrapper != null)
                {
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 获取到超级管理员信息
        /// </summary>
        /// <param name="strAccount"></param>
        /// <param name="strPassWord"></param>
        /// <returns></returns>
        public TB_SystemAdmin GetUserInfo(string strAccount, string strPassWord)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select * from [TB_SystemAdmin] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND Account = @Account");
                db.AddInParameter(dbCommandWrapper, "@Account", DbType.String, strAccount);
                strBuilder.AppendLine(" AND PassWord = @PassWord");
                db.AddInParameter(dbCommandWrapper, "@PassWord", DbType.String, strPassWord);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return this.ConvertTableToList(dt).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommandWrapper != null)
                {
                    dbCommandWrapper = null;
                }
            }
        }
    }
}
