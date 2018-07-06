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
    public class TB_RoleConnectModuleDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_RoleConnectModule info)
        {
            try
            {
                Database db = DBO.CreateDatabase();
                return this.Create(info, (DbTransaction)null, db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 事务添加
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Create(TB_RoleConnectModule info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO [TB_RoleConnectModule] ([RoleID]
      ,[ModuleID]) 
VALUES(@RoleID
,@ModuleID
)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@RoleID", DbType.Int32, info.RoleID);
                db.AddInParameter(dbCommandWrapper, "@ModuleID", DbType.Int32, info.ModuleID);
                #endregion
                if (tran == null)
                    intId = Convert.ToInt32(db.ExecuteScalar(dbCommandWrapper));
                else
                    intId = Convert.ToInt32(db.ExecuteScalar(dbCommandWrapper, tran));
                return intId;
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
        /// 根据用户ID事务删除
        /// </summary>
        /// <param name="intRoleID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Delete(int intRoleID, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"DELETE FROM [TB_RoleConnectModule] WHERE [RoleID]=@RoleID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);
                db.AddInParameter(dbCommandWrapper, "@RoleID", DbType.Int32, intRoleID);
                if (tran == null)
                    return db.ExecuteNonQuery(dbCommandWrapper);
                else
                    return db.ExecuteNonQuery(dbCommandWrapper, tran);
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
