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
    public class TB_UserConnectDormAreaDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_UserConnectDormArea info)
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
        public int Create(TB_UserConnectDormArea info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO [TB_UserConnectDormArea] ([UserID]
      ,[DormAreaID]) 
VALUES(@UserID
,@DormAreaID
)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, info.UserID);
                db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
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
        /// <param name="intUserID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Delete(int intUserID, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strSql = @"DELETE FROM [TB_UserConnectDormArea] WHERE UserID=@UserID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strSql);
                db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, intUserID);
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

        /// <summary>
        /// 根据宿舍区删除
        /// </summary>
        /// <param name="strDormAreaID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Delete(string strDormAreaID, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strSql = @"DELETE FROM [TB_UserConnectDormArea] WHERE DormAreaID  in (" + strDormAreaID + ")";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strSql);
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

        /// <summary>
        /// 根据宿舍区删除
        /// </summary>
        /// <param name="strID"></param>
        public int Delete(string strDormAreaID)
        {
            try
            {
                Database db = DBO.CreateDatabase();
                return this.Delete(strDormAreaID, (DbTransaction)null, db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
