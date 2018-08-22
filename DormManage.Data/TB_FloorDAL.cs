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
using System.Web;

namespace DormManage.Data.DAL
{
    public class TB_FloorDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_Floor info)
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
        public int Create(TB_Floor info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO TB_Floor (SiteID,DormAreaID,BuildingID,UnitID,Name,Creator) 
                                    VALUES(@SiteID,@DormAreaID,@BuildingID,@UnitID,@Name,@Creator)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                db.AddInParameter(dbCommandWrapper, "@BuildingID", DbType.Int32, info.BuildingID);
                db.AddInParameter(dbCommandWrapper, "@UnitID", DbType.Int32, info.UnitID);
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
        public int Edit(TB_Floor info)
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
        public int Edit(TB_Floor info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_Floor SET DormAreaID=@DormAreaID
                                                            ,BuildingID=@BuildingID
                                                            ,UnitID=@UnitID
                                                            ,Name=@Name
                                                            ,UpdateBy=@UpdateBy
                                                            ,UpdateDate=@UpdateDate 
                                                            WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                db.AddInParameter(dbCommandWrapper, "@BuildingID", DbType.Int32, info.BuildingID);
                db.AddInParameter(dbCommandWrapper, "@UnitID", DbType.Int32, info.UnitID);
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
        public TB_Floor Get(int intID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            TB_Floor mTB_Floor = null;
            try
            {
                string strSQL = @"select * from TB_Floor where 1=1";
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
                    mTB_Floor = new TB_Floor()
                    {
                        DormAreaID = Convert.ToInt32(dt.Rows[0][TB_Floor.col_DormAreaID]),
                        BuildingID=Convert.ToInt32(dt.Rows[0][TB_Floor.col_BuildingID]),
                        UnitID = Convert.ToInt32(dt.Rows[0][TB_Floor.col_UnitID]),
                        Name = Convert.ToString(dt.Rows[0][TB_Floor.col_Name]),
                    };
                }
                return mTB_Floor;
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
        /// <param name="info"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_Floor info, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select A.*
                                        ,B.Name as DormAreaName 
                                        ,C.name as BuildingName
                                        ,D.Name As UnitName
	                                from [TB_Floor] as A
	                                inner join TB_dormarea As B
	                                on A.DormAreaID=B.ID
	                                inner join TB_building as C
	                                on a.buildingid=c.id
	                                inner join TB_Unit AS D
	                                on a.UnitID=D.ID ";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件

                if (null != SessionHelper.Get(HttpContext.Current, TypeManager.User))
                {
                    strBuilder.AppendLine(@"inner join [TB_UserConnectDormArea] AS E
                                            on B.ID=E.[DormAreaID]
                                            where 1=1");
                    strBuilder.AppendLine(" AND E.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }

                if (info.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                }
                if (!string.IsNullOrEmpty(info.Name))
                {
                    strBuilder.AppendLine(" AND A.NAME LIKE @NAME");
                    db.AddInParameter(dbCommandWrapper, "@NAME", DbType.String, "%" + info.Name + "%");
                }
                if (info.DormAreaID > 0)
                {
                    strBuilder.AppendLine(" AND A.DormAreaID = @DormAreaID");
                    db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                }
                if (info.BuildingID > 0)
                {
                    strBuilder.AppendLine(" AND A.BuildingID = @BuildingID");
                    db.AddInParameter(dbCommandWrapper, "@BuildingID", DbType.Int32, info.BuildingID);
                }
                if (info.UnitID > 0)
                {
                    strBuilder.AppendLine(" AND A.UnitID = @UnitID");
                    db.AddInParameter(dbCommandWrapper, "@UnitID", DbType.Int32, info.UnitID);
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
        /// <param name="info"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_Floor info)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select * from TB_Floor where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                if (info.ID > 0)
                {
                    strBuilder.AppendLine(" AND ID <> @ID");
                    db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                }
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                strBuilder.AppendLine(" AND NAME = @NAME");
                db.AddInParameter(dbCommandWrapper, "@NAME", DbType.String, info.Name);
                strBuilder.AppendLine(" AND DormAreaID = @DormAreaID");
                db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                strBuilder.AppendLine(" AND BuildingID = @BuildingID");
                db.AddInParameter(dbCommandWrapper, "@BuildingID", DbType.Int32, info.BuildingID);
                strBuilder.AppendLine(" AND UnitID = @UnitID");
                db.AddInParameter(dbCommandWrapper, "@UnitID", DbType.Int32, info.UnitID);
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
        /// 获取site的所有楼层信息
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetTableBySite(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select A.*
                                        ,B.Name as DormAreaName 
                                        ,C.name as BuildingName
                                        ,D.Name As UnitName
	                                from [TB_Floor] as A
	                                inner join TB_dormarea As B
	                                on A.DormAreaID=B.ID
	                                inner join TB_building as C
	                                on a.buildingid=c.id
	                                inner join TB_Unit AS D
	                                on a.UnitID=D.ID");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND A.SiteID = @SiteID");
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        public int Delete(string strID, DbTransaction tran, Database db)
        {
            if (string.IsNullOrEmpty(strID))
                return 0;
            DbCommand dbCommandWrapper = null;
            string strSql = @"DELETE FROM [TB_Floor] WHERE ID in (" + strID + ")";
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
