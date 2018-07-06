using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Framework.Enum;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DormManage.Data.DAL
{
    public class TB_DormAreaDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_DormArea info)
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
        public int Create(TB_DormArea info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = "INSERT INTO TB_DormArea (SiteID,Name,Creator) VALUES(@SiteID,@Name,@Creator)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Name), info.Name));
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Creator), info.Creator));
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
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Edit(TB_DormArea info)
        {
            try
            {
                Database db = DBO.CreateDatabase();
                return this.Edit(info, (DbTransaction)null, db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 事务更新
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Edit(TB_DormArea info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_DormArea SET Name=@Name,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Name), info.Name));
                db.AddInParameter(dbCommandWrapper, "@UpdateBy", DbType.String, SetNullValue(string.IsNullOrEmpty(info.UpdateBy), info.UpdateBy));
                db.AddInParameter(dbCommandWrapper, "@UpdateDate", DbType.DateTime, DateTime.Now);
                #endregion
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
        /// 根据ID获取对象
        /// </summary>
        /// <param name="intID"></param>
        /// <returns></returns>
        public TB_DormArea Get(int intID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            TB_DormArea mTB_DormArea = null;
            try
            {
                string strSQL = @"select * from tb_DormArea where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND ID = @ID");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, intID);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    mTB_DormArea = new TB_DormArea()
                    {
                        Name = Convert.ToString(dt.Rows[0][TB_DormArea.col_Name]),
                    };
                }
                return mTB_DormArea;
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
        /// 获取分页数据集合
        /// </summary>
        /// <param name="tb_DormArea"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_DormArea tb_DormArea, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select A.*,ISNULL(BB.Count, 0) Count, 
                       case  when A.[IsEnable] is null then '已启用'
                        when A.[IsEnable]='已禁用' then '已禁用'
                        when A.[IsEnable]='已启用' then '已启用'
                        else '已启用' end  as IsEnable1
                             from tb_DormArea as A 
                                  left join (select DormAreaID ,Count(1) Count from TB_AssignDormArea Group by DormAreaID) 
                                 as BB on A.ID=BB.DormAreaID ";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件

                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@"inner join [TB_UserConnectDormArea] AS B
on A.ID=B.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND B.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }
                if (tb_DormArea.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_DormArea.SiteID);
                }
                if (!string.IsNullOrEmpty(tb_DormArea.Name))
                {
                    strBuilder.AppendLine(" AND A.NAME LIKE @NAME");
                    db.AddInParameter(dbCommandWrapper, "@NAME", DbType.String, "%" + tb_DormArea.Name + "%");
                }
                #endregion
                if (pager != null && !pager.IsNull)
                {
                    strSQL = pager.GetPagerSql4Count(strBuilder.ToString());
                    dbCommandWrapper.CommandText = strSQL;
                    dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                    pager.TotalRecord = Convert.ToInt32(dt.Rows[0][0]);
                    dbCommandWrapper.CommandText = pager.GetPagerSql4Data(strBuilder.ToString(), DataBaseTypeEnum.sqlserver);
                }
                else
                {
                    dbCommandWrapper.CommandText = strBuilder.ToString();
                }
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return dt;
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
        /// 
        /// </summary>
        /// <param name="intUserID"></param>
        /// <returns></returns>
        public DataTable GetTable(int intUserID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                //2018-03-14 注意：这里要与 DormAreaBLL.GetTable 的列对应
                string strSQL = @"SELECT A.*,0 Count, '已启用' as IsEnable1
FROM tb_DormArea AS A
INNER JOIN [TB_UserConnectDormArea] AS B
ON A.ID=B.[DormAreaID]
                                        WHERE B.[UserID]=@UserID";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, intUserID);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return dt;
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
        /// 
        /// </summary>
        /// <param name="tb_DormArea"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_DormArea tb_DormArea)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select id from tb_DormArea where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_DormArea.SiteID);
                if (tb_DormArea.ID > 0)
                {
                    strBuilder.AppendLine(" AND ID <> @ID");
                    db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, tb_DormArea.ID);
                }
                strBuilder.AppendLine(" AND NAME = @NAME");
                db.AddInParameter(dbCommandWrapper, "@NAME", DbType.String, tb_DormArea.Name);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return dt;
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
        /// 获取到一个Site的所有宿舍区
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetTableBySite(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select ID
,[SiteID]
      ,[Name]
      ,[Creator] from tb_DormArea where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, siteID);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return dt;
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

        private object SetNullValue(bool isNullValue, object value)
        {
            if (isNullValue)
                return DBNull.Value;
            else
                return value;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        public int Delete(string strID, DbTransaction tran, Database db)
        {
            if (string.IsNullOrEmpty(strID))
                return 0;
            DbCommand dbCommandWrapper = null;
            string strSql = @"DELETE FROM TB_DormArea WHERE ID in ("+strID+")";
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
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        public int Delete(string strID)
        {
            try
            {
                Database db = DBO.CreateDatabase();
                return this.Delete(strID, (DbTransaction)null, db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
