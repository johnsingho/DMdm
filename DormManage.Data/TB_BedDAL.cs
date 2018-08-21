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
    public class TB_BedDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_Bed info)
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
        public int Create(TB_Bed info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO TB_Bed (SiteID,DormAreaID,BuildingID,UnitID,FloorID,RoomID,Name,[Status],Creator,KeyCount)
                                    VALUES(@SiteID,@DormAreaID,@BuildingID,@UnitID,@FloorID,@RoomID,@Name,@Status,@Creator,@KeyCount)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);                
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                db.AddInParameter(dbCommandWrapper, "@BuildingID", DbType.Int32, info.BuildingID);
                db.AddInParameter(dbCommandWrapper, "@UnitID", DbType.Int32, info.UnitID);
                db.AddInParameter(dbCommandWrapper, "@FloorID", DbType.Int32, info.FloorID);
                db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
                db.AddInParameter(dbCommandWrapper, "@Status", DbType.Int32, info.Status);
                //db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
                //db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Creator), info.Creator));
                db.AddInParameter(dbCommandWrapper, "@KeyCount", DbType.Int32, info.KeyCount);
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
        public int Edit(TB_Bed info)
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
        public int Edit(TB_Bed info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_Bed SET DormAreaID=@DormAreaID
                                                            ,BuildingID=@BuildingID
                                                            ,UnitID=@UnitID
                                                            ,FloorID=@FloorID
                                                            ,RoomID=@RoomID
                                                            ,Name=@Name
                                                            ,UpdateBy=@UpdateBy
                                                            ,UpdateDate=@UpdateDate 
                                                            ,KeyCount=@KeyCount 
                                                            WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                db.AddInParameter(dbCommandWrapper, "@BuildingID", DbType.Int32, info.BuildingID);
                db.AddInParameter(dbCommandWrapper, "@UnitID", DbType.Int32, info.UnitID);
                db.AddInParameter(dbCommandWrapper, "@FloorID", DbType.Int32, info.FloorID);
                db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
                //db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
                //db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                db.AddInParameter(dbCommandWrapper, "@UpdateBy", DbType.String, SetNullValue(string.IsNullOrEmpty(info.UpdateBy), info.UpdateBy));
                db.AddInParameter(dbCommandWrapper, "@UpdateDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommandWrapper, "@KeyCount", DbType.Int32, info.KeyCount);
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
        public TB_Bed Get(int intID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            TB_Bed mTB_Bed = null;
            try
            {
                string strSQL = @"select * from TB_Bed where 1=1";
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
                    mTB_Bed = new TB_Bed()
                    {
                        DormAreaID = Convert.ToInt32(dt.Rows[0][TB_Bed.col_DormAreaID]),
                        BuildingID = Convert.ToInt32(dt.Rows[0][TB_Bed.col_BuildingID]),
                        UnitID = Convert.ToInt32(dt.Rows[0][TB_Bed.col_UnitID]),
                        FloorID = Convert.ToInt32(dt.Rows[0][TB_Bed.col_FloorID]),
                        RoomID = Convert.ToInt32(dt.Rows[0][TB_Bed.col_RoomID]),
                        //RoomSexType = Convert.ToString(dt.Rows[0][TB_Room.col_RoomSexType]),
                        //RoomType = Convert.ToInt32(dt.Rows[0][TB_Room.col_RoomType]),
                        Name = Convert.ToString(dt.Rows[0][TB_Bed.col_Name]),
                        KeyCount = Convert.ToInt32(dt.Rows[0][TB_Bed.col_KeyCount]),
                    };
                }
                return mTB_Bed;
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
        public DataTable GetTable(TB_Bed info, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select A.[ID]
      ,A.[RoomID]
      ,A.[Name]
      ,A.[Creator]
      ,A.[CreateDate]
      ,A.[UpdateBy]
      ,A.[UpdateDate]
      ,A.[SiteID]
      ,A.[DormAreaID]
      ,A.[BuildingID]
      ,A.[UnitID]
      ,A.[FloorID]
      ,A.[Status]
      ,A.[KeyCount]
      ,case  when A.[IsEnable] is null then '已启用'
when A.[IsEnable]='已禁用' then '已禁用'
when A.[IsEnable]='已启用' then '已启用'
else '已启用' end  as IsEnable
      ,B.Name as DormAreaName 
      ,C.name as BuildingName
      ,D.Name As UnitName
      ,E.Name As FloorName
      ,F.Name As RoomName
      ,F.RoomSexType
      ,GG.Name AS RoomTypeName
from [TB_Bed] as A
left join TB_dormarea As B
on A.DormAreaID=B.ID
left join TB_building as C
on a.buildingid=c.id 
left join TB_Unit AS D
on a.UnitID=D.ID
left join TB_Floor as E
on a.FloorID=E.ID
left join TB_Room AS F
on a.RoomID=F.ID 
LEFT JOIN TB_RoomType AS GG
ON F.RoomType=GG.ID ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@"inner join [TB_UserConnectDormArea] AS G
on B.ID=G.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND G.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1 ");
                }
                if (info.ID > 0)
                {
                    strBuilder.AppendLine(" AND A.ID = @ID");
                    db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
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
                if (info.FloorID > 0)
                {
                    strBuilder.AppendLine(" AND A.FloorID = @FloorID");
                    db.AddInParameter(dbCommandWrapper, "@FloorID", DbType.Int32, info.FloorID);
                }
                if (info.RoomType > 0)
                {
                    strBuilder.AppendLine(" AND F.RoomType = @RoomType");
                    db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                }
                if (!String.IsNullOrEmpty(info.RoomSexType))
                {
                    strBuilder.AppendLine(" AND F.RoomSexType = @RoomSexType");
                    db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
                }
                if (info.RoomID > 0)
                {
                    strBuilder.AppendLine(" AND A.RoomID = @RoomID");
                    db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                }
                if (info.Status > 0)
                {
                    strBuilder.AppendLine(" AND A.Status = @Status");
                    db.AddInParameter(dbCommandWrapper, "@Status", DbType.Int32, info.Status);
                }
                #endregion
                if (pager != null && !pager.IsNull)
                {
                    dbCommandWrapper.CommandText = pager.GetPagerSql4Count(strBuilder.ToString());
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
        /// 获取分页数据集合
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTableByEnableStatus(TB_Bed info, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select A.[ID]
      ,A.[RoomID]
      ,A.[Name]
      ,A.[Creator]
      ,A.[CreateDate]
      ,A.[UpdateBy]
      ,A.[UpdateDate]
      ,A.[SiteID]
      ,A.[DormAreaID]
      ,A.[BuildingID]
      ,A.[UnitID]
      ,A.[FloorID]
      ,A.[Status]
      ,A.[KeyCount]
      ,case  when A.[IsEnable] is null then '已启用'
when A.[IsEnable]='已禁用' then '已禁用'
when A.[IsEnable]='已启用' then '已启用'
else '已启用' end  as IsEnable
      ,B.Name as DormAreaName 
      ,C.name as BuildingName
      ,D.Name As UnitName
      ,E.Name As FloorName
      ,F.Name As RoomName
      ,F.RoomSexType
      ,GG.Name AS RoomTypeName
from [TB_Bed] as A
left join TB_dormarea As B
on A.DormAreaID=B.ID
left join TB_building as C
on a.buildingid=c.id 
left join TB_Unit AS D
on a.UnitID=D.ID
left join TB_Floor as E
on a.FloorID=E.ID
left join TB_Room AS F
on a.RoomID=F.ID 
LEFT JOIN TB_RoomType AS GG
ON F.RoomType=GG.ID ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@"inner join [TB_UserConnectDormArea] AS G
on B.ID=G.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND G.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1 And (A.IsEnable<>'已禁用' OR A.IsEnable is NULL)");
                }
                if (info.ID > 0)
                {
                    strBuilder.AppendLine(" AND A.ID = @ID");
                    db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
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
                if (info.FloorID > 0)
                {
                    strBuilder.AppendLine(" AND A.FloorID = @FloorID");
                    db.AddInParameter(dbCommandWrapper, "@FloorID", DbType.Int32, info.FloorID);
                }
                if (info.RoomType > 0)
                {
                    strBuilder.AppendLine(" AND F.RoomType = @RoomType");
                    db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                }
                if (!String.IsNullOrEmpty(info.RoomSexType))
                {
                    strBuilder.AppendLine(" AND F.RoomSexType = @RoomSexType");
                    db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
                }
                if (info.RoomID > 0)
                {
                    strBuilder.AppendLine(" AND A.RoomID = @RoomID");
                    db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                }
                if (info.Status > 0)
                {
                    strBuilder.AppendLine(" AND A.Status = @Status");
                    db.AddInParameter(dbCommandWrapper, "@Status", DbType.Int32, info.Status);
                }
                #endregion
                if (pager != null && !pager.IsNull)
                {
                    dbCommandWrapper.CommandText = pager.GetPagerSql4Count(strBuilder.ToString());
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
        public DataTable GetTable(TB_Bed info)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select id,[Status] from TB_Bed where 1=1";
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
                if (!string.IsNullOrEmpty(info.Name))
                {
                    strBuilder.AppendLine(" AND NAME = @NAME");
                    db.AddInParameter(dbCommandWrapper, "@NAME", DbType.String, info.Name);
                }
                if (info.DormAreaID > 0)
                {
                    strBuilder.AppendLine(" AND DormAreaID = @DormAreaID");
                    db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                }
                if (info.BuildingID > 0)
                {
                    strBuilder.AppendLine(" AND BuildingID = @BuildingID");
                    db.AddInParameter(dbCommandWrapper, "@BuildingID", DbType.Int32, info.BuildingID);
                }
                if (info.UnitID > 0)
                {
                    strBuilder.AppendLine(" AND UnitID = @UnitID");
                    db.AddInParameter(dbCommandWrapper, "@UnitID", DbType.Int32, info.UnitID);
                }
                if (info.FloorID > 0)
                {
                    strBuilder.AppendLine(" AND FloorID = @FloorID");
                    db.AddInParameter(dbCommandWrapper, "@FloorID", DbType.Int32, info.FloorID);
                }
                if (info.RoomID > 0)
                {
                    strBuilder.AppendLine(" AND RoomID = @RoomID");
                    db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                }
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
        /// 更新床位状态
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <param name="bedStatus"></param>
        /// <returns></returns>
        public int Update(int intID, DbTransaction tran, Database db, TypeManager.BedStatus bedStatus)
        {

            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"update [TB_Bed] set [Status]=@Status WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);
                db.AddInParameter(dbCommandWrapper, "@Status", DbType.Int32, (int)bedStatus);
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, intID);
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

        private object SetNullValue(bool isNullValue, object value)
        {
            if (isNullValue)
                return DBNull.Value;
            else
                return value;
        }

        /// <summary>
        /// 获取site的所有床位信息
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetTableBySite(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select A.[ID]
      ,A.[Name]
      ,A.[Creator]
      ,A.[SiteID]
      ,A.[DormAreaID]
      ,A.[BuildingID]
,A.[RoomID]
,A.[Status]
,A.[KeyCount]
      ,B.Name as DormAreaName 
      ,C.name as BuildingName
      ,D.Name As RoomName
from [TB_Bed] as A
left join TB_dormarea As B
on A.DormAreaID=B.ID
left join TB_building as C
on a.buildingid=c.id 
left join TB_Room AS D
on a.RoomID=D.ID 
where 1=1");
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
        /// 获取site的所有未入住床位信息
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetTableBySiteCheckIn(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select A.[ID]
                                                              ,A.[Name]
                                                              ,A.[Creator]
                                                              ,A.[SiteID]
                                                              ,A.[DormAreaID]
                                                              ,A.[BuildingID]
                                                              ,A.[RoomID]
                                                              ,A.[Status]
                                                              ,A.[KeyCount]
                                                              ,A.[IsEnable]
                                                              ,B.Name as DormAreaName 
                                                              ,C.name as BuildingName
                                                              ,D.Name As RoomName
                                                        from [TB_Bed] as A
                                                        left join TB_dormarea As B
                                                        on A.DormAreaID=B.ID
                                                        left join TB_building as C
                                                        on a.buildingid=c.id 
                                                        left join TB_Room AS D
                                                        on a.RoomID=D.ID 
                                                        where 1=1");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND A.SiteID = @SiteID and A.status=1 ");
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
        /// 批量更新床位状态
        /// </summary>
        /// <param name="idStr">包含多个床位ID，用逗号分隔</param>
        /// <param name="bedStatus"></param>
        /// <returns></returns>
        public int Update(string idStr, TypeManager.BedStatus bedStatus)
        {

            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"update [TB_Bed] set [Status]=@Status WHERE ID in (" + idStr + ")";
            try
            {
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);
                db.AddInParameter(dbCommandWrapper, "@Status", DbType.Int32, (int)bedStatus);
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
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        public int Delete(string strID, DbTransaction tran, Database db)
        {
            if (string.IsNullOrEmpty(strID))
                return 0;
            DbCommand dbCommandWrapper = null;
            string strSql = @"DELETE FROM [TB_Bed] WHERE ID in ("+strID+")";
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

        /// <summary>
        /// 删除分配信息
        /// </summary>
        /// <param name="strID"></param>
        public int DeleteAssignDormArea(string strCardNo, DbTransaction tran, Database db)
        {
            //Database db = DBO.GetInstance();
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"delete from TB_AssignDormArea where CardNo=@CardNo";
         
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql);
                #region Add parameters             
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, strCardNo);
                #endregion
                if (tran == null)
                    intId = Convert.ToInt32(db.ExecuteNonQuery(dbCommandWrapper));
                else
                    intId = Convert.ToInt32(db.ExecuteNonQuery(dbCommandWrapper, tran));
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


    }
}
