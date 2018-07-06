using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DormManage.Framework;
using DormManage.Framework.Enum;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DormManage.Data.DAL
{
    public class TB_RoleDAL
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_Role info)
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
        public int Create(TB_Role info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = "INSERT INTO TB_Role(SiteID,Name) VALUES(@SiteID,@Name)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
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
        public int Edit(TB_Role info)
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
        public int Edit(TB_Role info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_Role SET Name=@Name WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
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
        /// 将datatable转换成list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<TB_Role> ConvertTableToList(DataTable dt)
        {
            TB_Role mTB_Role = null;
            List<TB_Role> lstTB_Role = new List<TB_Role>();
            foreach (DataRow dr in dt.Rows)
            {
                mTB_Role = new TB_Role()
                {
                    ID = Convert.ToInt32(dr[TB_Role.col_ID]),
                    Name = dr[TB_Role.col_Name].ToString(),
                    SiteID = Convert.ToInt32(dr[TB_Role.col_SiteID]),
                };
                lstTB_Role.Add(mTB_Role);
            }
            return lstTB_Role;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intRoleID"></param>
        /// <returns></returns>
        public TB_Role Get(int intRoleID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * FROM [TB_Role] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND ID = @ID");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, intRoleID);
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

        public DataTable GetTable(TB_Role tb_Role, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * FROM [TB_Role] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_Role.SiteID);
                if (!string.IsNullOrEmpty(tb_Role.Name))
                {
                    strBuilder.AppendLine(" AND Name like @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, "%" + tb_Role.Name + "%");
                }
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
        /// <param name="intRoleName"></param>
        /// <returns></returns>
        public TB_Role GetTable(string intRoleName, int intSiteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * FROM [TB_Role] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, intSiteID);
                strBuilder.AppendLine(" AND Name = @Name");
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, intRoleName);
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
