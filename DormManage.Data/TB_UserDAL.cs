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
    public class TB_UserDAL
    {
        /// <summary>
        /// 将datatable转换成list对象
        /// </summary>
        /// <param name="dtUser"></param>
        /// <returns></returns>
        private List<TB_User> ConvertTableToList(DataTable dtUser)
        {
            List<TB_User> lstTB_User = new List<TB_User>();
            TB_User mTB_User = null;
            foreach (DataRow dr in dtUser.Rows)
            {
                mTB_User = new TB_User();
                mTB_User.ID = Convert.ToInt32(dr[TB_User.col_ID]);
                mTB_User.RoleID = Convert.ToInt32(dr[TB_User.col_RoleID]);
                mTB_User.SiteID = Convert.ToInt32(dr[TB_User.col_SiteID]);
                mTB_User.UpdateBy = dr[TB_User.col_UpdateBy] is DBNull ? string.Empty : dr[TB_User.col_UpdateBy].ToString();
                mTB_User.UpdateDate = dr[TB_User.col_UpdateDate] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[TB_User.col_UpdateDate]);
                mTB_User.EName = dr[TB_User.col_EName] == null ? string.Empty : dr[TB_User.col_EName].ToString();
                mTB_User.CName = dr[TB_User.col_CName] == null ? string.Empty : dr[TB_User.col_CName].ToString();
                mTB_User.ADAccount = dr[TB_User.col_ADAccount].ToString();
                mTB_User.EmployeeNo = dr[TB_User.col_EmployeeNo] == null ? string.Empty : dr[TB_User.col_EmployeeNo].ToString();
                mTB_User.Creator = dr[TB_User.col_Creator].ToString();
                mTB_User.CreateDate = Convert.ToDateTime(dr[TB_User.col_CreateDate]);
                lstTB_User.Add(mTB_User);
            }
            return lstTB_User;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_User info)
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
        public int Create(TB_User info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO [TB_User] ([SiteID]
      ,[ADAccount]
      ,[EmployeeNo]
      ,[EName]
      ,[Creator]
      ,[CName]
      ,[RoleID]) 
VALUES(@SiteID
,@ADAccount
,@EmployeeNo
,@EName
,@Creator
,@CName
,@RoleID
)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@ADAccount", DbType.String, info.ADAccount);
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, info.EmployeeNo);
                db.AddInParameter(dbCommandWrapper, "@EName", DbType.String, info.EName);
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, info.Creator);
                db.AddInParameter(dbCommandWrapper, "@CName", DbType.String, info.CName);
                db.AddInParameter(dbCommandWrapper, "@RoleID", DbType.Int32, info.RoleID);
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
        public int Edit(TB_User info)
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
        public int Edit(TB_User info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE [TB_User] 
SET ADAccount=@ADAccount
,EmployeeNo=@EmployeeNo
,EName=@EName
,CName=@CName
,RoleID=@RoleID
,UpdateBy=@UpdateBy
,UpdateDate=@UpdateDate
WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@ADAccount", DbType.String, info.ADAccount);
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, info.EmployeeNo);
                db.AddInParameter(dbCommandWrapper, "@EName", DbType.String, info.EName);
                db.AddInParameter(dbCommandWrapper, "@CName", DbType.String, info.CName);
                db.AddInParameter(dbCommandWrapper, "@RoleID", DbType.Int32, info.RoleID);
                db.AddInParameter(dbCommandWrapper, "@UpdateBy", DbType.String, info.UpdateBy);
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
        /// 根据AD账号获取到用户信息
        /// </summary>
        /// <param name="strADAccount"></param>
        /// <returns></returns>
        public TB_User GetUserInfo(string strADAccount)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * FROM [TB_User] WHERE 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND ADAccount = @ADAccount");
                db.AddInParameter(dbCommandWrapper, "@ADAccount", DbType.String, strADAccount);
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
        /// 根据用户ID获取到用户信息
        /// </summary>
        /// <param name="intUserID"></param>
        /// <returns></returns>
        public TB_User GetUserInfo(int intUserID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * FROM [TB_User] WHERE 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND ID = @ID");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, intUserID);
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
        /// 
        /// </summary>
        /// <param name="tb_User"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_User tb_User, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.* ,C.Name as DormName FROM [TB_User] AS A
LEFT JOIN [TB_UserConnectDormArea] AS B
ON A.ID=B.[UserID]
LEFT JOIN [TB_DormArea] AS C
ON B.[DormAreaID]=C.ID
WHERE 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;

                if (tb_User.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.String, tb_User.SiteID);
                }

                if (!String.IsNullOrEmpty(tb_User.ADAccount))
                {
                    strBuilder.AppendLine(" AND A.ADAccount like @ADAccount");
                    db.AddInParameter(dbCommandWrapper, "@ADAccount", DbType.String, "%" + tb_User.ADAccount + "%");
                }

                if (!String.IsNullOrEmpty(tb_User.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo like @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, "%" + tb_User.EmployeeNo + "%");
                }

                if (!String.IsNullOrEmpty(tb_User.CName))
                {
                    strBuilder.AppendLine(" AND A.CName like @CName");
                    db.AddInParameter(dbCommandWrapper, "@CName", DbType.String, "%" + tb_User.CName + "%");
                }

                if (!String.IsNullOrEmpty(tb_User.EName))
                {
                    strBuilder.AppendLine(" AND A.EName like @EName");
                    db.AddInParameter(dbCommandWrapper, "@EName", DbType.String, "%" + tb_User.EName + "%");
                }

                if (pager != null && !pager.IsNull)
                {
                    strSQL = pager.GetPagerSql4Count(strBuilder.ToString());
                    dbCommandWrapper.CommandText = strSQL;
                    dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                    pager.TotalRecord = Convert.ToInt32(dt.Rows[0][0]);
                    //dbCommandWrapper.CommandText = pager.GetPagerSql4Data(strBuilder.ToString(), DataBaseTypeEnum.sqlserver);
                    dbCommandWrapper.CommandText = strBuilder.ToString();
                }
                else
                {
                    dbCommandWrapper.CommandText = strBuilder.ToString();
                }

                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];

                DataTable dtNew = dt.Clone();
                DataRow[] drFilter = null;
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNew = dtNew.NewRow();
                    drFilter = dtNew.Select("[ADAccount]='" + dr[TB_User.col_ADAccount] + "'");
                    if (drFilter.Length > 0)
                    {
                        drFilter[0]["DormName"] = drFilter[0]["DormName"] + "，" + dr["DormName"];
                    }
                    else
                    {
                        drNew.ItemArray = dr.ItemArray;
                        dtNew.Rows.Add(drNew);
                    }
                }

                if (dtNew.Rows.Count > 0)
                {
                    pager.TotalRecord = dtNew.Rows.Count;
                }
                else
                {
                    pager.TotalRecord = 0;
                }

                return dtNew;
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
        /// 根据UserID删除
        /// </summary>
        /// <param name="strBedID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteByUserID(string strUserID, DbTransaction tran, Database db)
        {
            if (String.IsNullOrEmpty(strUserID))
                return 0;
            DbCommand dbCommandWrapper = null;
            string strSQL = string.Format(@"DELETE FROM [TB_User]
                                            WHERE [ID] in ({0})", strUserID);
            try
            {
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                dbCommandWrapper.CommandText = strSQL;
                if (null != tran)
                {
                    return db.ExecuteNonQuery(dbCommandWrapper, tran);
                }
                else
                {
                    return db.ExecuteNonQuery(dbCommandWrapper);
                }
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
