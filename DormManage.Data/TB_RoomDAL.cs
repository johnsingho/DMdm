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
using System.Data.SqlClient;
using System.Web;

namespace DormManage.Data.DAL
{
    public class TB_RoomDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_Room info)
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
        public int Create(TB_Room info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO TB_Room (SiteID,DormAreaID,BuildingID,UnitID,FloorID,Name,RoomSexType,RoomType,Creator,RoomType2) 
                                    VALUES(@SiteID,@DormAreaID,@BuildingID,@UnitID,@FloorID,@Name,@RoomSexType,@RoomType,@Creator,@RoomType2)";
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
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
                db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
                db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                db.AddInParameter(dbCommandWrapper, "@RoomType2", DbType.Int32, info.RoomType2);
              
         
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
        public int Edit(TB_Room info)
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
        public int Edit(TB_Room info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_Room SET DormAreaID=@DormAreaID
                                                            ,BuildingID=@BuildingID
                                                            ,UnitID=@UnitID
                                                            ,FloorID=@FloorID
                                                            ,RoomSexType=@RoomSexType
                                                            ,RoomType=@RoomType
                                                            ,Name=@Name
                                                            ,UpdateBy=@UpdateBy
                                                            ,UpdateDate=@UpdateDate
                                                            ,RoomType2=@RoomType2
                                                            
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
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
                db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
                db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                db.AddInParameter(dbCommandWrapper, "@RoomType2", DbType.Int32, info.RoomType2);
                db.AddInParameter(dbCommandWrapper, "@UpdateBy", DbType.String, SetNullValue(string.IsNullOrEmpty(info.UpdateBy), info.UpdateBy));
                db.AddInParameter(dbCommandWrapper, "@UpdateDate", DbType.DateTime, DateTime.Now);
                //db.AddInParameter(dbCommandWrapper, "@KeyCount", DbType.String, info.KeyCount);

                
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
        public TB_Room Get(int intID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            TB_Room mTB_Room = null;
            try
            {
                string strSQL = @"select * from TB_Room where 1=1";
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
                    mTB_Room = new TB_Room()
                    {
                        DormAreaID = Convert.ToInt32(dt.Rows[0][TB_Room.col_DormAreaID]),
                        BuildingID = Convert.ToInt32(dt.Rows[0][TB_Room.col_BuildingID]),
                        UnitID = Convert.ToInt32(dt.Rows[0][TB_Room.col_UnitID]),
                        FloorID = Convert.ToInt32(dt.Rows[0][TB_Room.col_FloorID]),
                        RoomSexType = Convert.ToString(dt.Rows[0][TB_Room.col_RoomSexType]),
                        RoomType = Convert.ToInt32(dt.Rows[0][TB_Room.col_RoomType]),
                        RoomType2 = Convert.ToInt32(dt.Rows[0][TB_Room.col_RoomType2]),
                        Name = Convert.ToString(dt.Rows[0][TB_Room.col_Name]),
                        KeyCount = Convert.ToString(dt.Rows[0][TB_Room.col_KeyCount]),
                        
                    };
                }
                return mTB_Room;
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
        public DataTable GetTable(TB_Room info, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"
select A.[ID]
      ,A.[FloorID]
      ,A.[Name]
      ,A.[RoomSexType]
      ,case A.[RoomType2]
	  when 1 then '员工宿舍'
	  when 2 then '家庭房'
	  else ''
	  end
	  as RoomType2
      ,A.[Creator]
      ,A.[CreateDate]
      ,A.[UpdateBy]
      ,A.[UpdateDate]
      ,A.[SiteID]
      ,A.[DormAreaID]
      ,A.[BuildingID]
      ,A.[UnitID]
,A.[KeyCount]
,case  when A.[IsEnable] is null then '已启用'
when A.[IsEnable]='已禁用' then '已禁用'
when A.[IsEnable]='已启用' then '已启用'
else '已启用' end  as IsEnable
	  ,B.Name as DormAreaName 
	  ,C.name as BuildingName
	  ,D.Name As UnitName
	  ,E.Name As FloorName
	  ,F.Name As RoomTypeName
from [TB_Room] as A
left join TB_dormarea As B
on A.DormAreaID=B.ID
left join TB_building as C
on a.buildingid=c.id
left join TB_RoomType AS F
on a.RoomType=F.ID
left join TB_Unit AS D
on a.UnitID=D.ID
left join TB_Floor as E
on a.FloorID=E.ID ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件
                if (null != SessionHelper.Get(HttpContext.Current, TypeManager.User))
                {
                    strBuilder.AppendLine(@"inner join [TB_UserConnectDormArea] AS G
                                            on B.ID=G.[DormAreaID]
                                            where 1=1 ");
                    strBuilder.AppendLine(" AND G.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1 ");
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
                    strBuilder.AppendLine(" AND A.RoomType = @RoomType");
                    db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                }
                if (info.RoomType2 > 0)
                {
                    strBuilder.AppendLine(" AND A.RoomType2 = @RoomType2");
                    db.AddInParameter(dbCommandWrapper, "@RoomType2", DbType.Int32, info.RoomType2);
                }
                if (!String.IsNullOrEmpty(info.RoomSexType))
                {
                    strBuilder.AppendLine(" AND A.RoomSexType = @RoomSexType");
                    db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
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
        public DataTable GetTable(TB_Room info)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select id from TB_Room where 1=1";
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
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetUnLockRoom(TB_Room info, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"
select A.[ID]
      ,A.[FloorID]
      ,A.[Name]
      ,A.[RoomSexType]
      ,case A.[RoomType2]
	  when 1 then '员工宿舍'
	  when 2 then '家庭房'
	  else ''
	  end
	  as RoomType2
      ,A.[Creator]
      ,A.[CreateDate]
      ,A.[UpdateBy]
      ,A.[UpdateDate]
      ,A.[SiteID]
      ,A.[DormAreaID]
      ,A.[BuildingID]
      ,A.[UnitID]
	  ,B.Name as DormAreaName 
	  ,C.name as BuildingName
	  ,D.Name As UnitName
	  ,E.Name As FloorName
	  ,F.Name As RoomTypeName
	  ,count(G.ID) AS FreeBedCount
from [TB_Room] as A
left join TB_dormarea As B
on A.DormAreaID=B.ID
left join TB_building as C
on a.buildingid=c.id
left join TB_RoomType AS F
on a.RoomType=F.ID
left join TB_Unit AS D
on a.UnitID=D.ID
left join TB_Floor as E
on a.FloorID=E.ID
left join [TB_Bed] as G
on a.id=G.roomID and G.[Status]=1 and G.ID not in  (select BedID from [TB_AssignRoom]) AND (G.IsEnable<>'已禁用' OR G.IsEnable is NULL)");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件
                if (null != SessionHelper.Get(HttpContext.Current, TypeManager.User))
                {
                    strBuilder.AppendLine(@"inner join [TB_UserConnectDormArea] AS H
                                            on B.ID=H.[DormAreaID]
                                            where 1=1 ");
                    strBuilder.AppendLine(" AND H.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1 ");
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
                    strBuilder.AppendLine(" AND A.RoomType = @RoomType");
                    db.AddInParameter(dbCommandWrapper, "@RoomType", DbType.Int32, info.RoomType);
                }
                if (info.RoomType2 > 0)
                {
                    strBuilder.AppendLine(" AND A.RoomType2 = @RoomType2");
                    db.AddInParameter(dbCommandWrapper, "@RoomType2", DbType.Int32, info.RoomType2);
                }
                if (!String.IsNullOrEmpty(info.RoomSexType))
                {
                    strBuilder.AppendLine(" AND A.RoomSexType = @RoomSexType");
                    db.AddInParameter(dbCommandWrapper, "@RoomSexType", DbType.String, info.RoomSexType);
                }

                strBuilder.AppendLine(@"group by A.[ID]
      ,A.[FloorID]
      ,A.[Name]
      ,A.[RoomSexType]
	  ,RoomType2
      ,A.[Creator]
      ,A.[CreateDate]
      ,A.[UpdateBy]
      ,A.[UpdateDate]
      ,A.[SiteID]
      ,A.[DormAreaID]
      ,A.[BuildingID]
      ,A.[UnitID]
	  ,B.Name 
	  ,C.name 
	  ,D.Name
	  ,E.Name
	  ,F.Name 
	  having count(G.ID)>0");
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
        /// 获取到DormInfo
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetDormInfoBySiteID(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat(@"select TotalBeds.areaname ,TotalBeds.roomtypt + TotalBeds.[RoomSexType] as 'grade',isnull( TotalBeds.name,'合计') as 'DormNo'	  
	                ,sum(TotalBeds.TotalBedsQty) as 'TotalBedsQty',sum(Occupied.OccupiedQty) as 'OccupiedQty',
	                sum(Vacant.VacantQty) as 'VacantQty',cast(case sum(TotalBeds.TotalBedsQty) when 0 then 0 else sum(Occupied.OccupiedQty)*100/sum(TotalBeds.TotalBedsQty) end   AS VARCHAR)+'%' as 'Occupancyrate'
                    from 
                    (
	                    select B.Name areaname,F.Name roomtypt, A.[RoomSexType] ,C.name ,count(H.ID) AS 'TotalBedsQty'
	                    from [TB_Room] as A	
	                    left join TB_dormarea As B on A.DormAreaID=B.ID
	                    left join TB_building as C on a.buildingid=c.id
	                    left join TB_RoomType AS F on a.RoomType=F.ID
	                    left join [TB_Bed] as H	on a.id=H.roomID AND A.SiteID = {0}
                        WHERE H.IsEnable<>'已禁用' OR H.IsEnable is NULL
	                    group by B.Name,F.Name,A.[RoomSexType],C.name
                    ) TotalBeds
                    LEFT JOIN (
	                    select B.Name areaname,F.Name roomtypt, A.[RoomSexType] ,C.name ,count(H.ID) AS 'OccupiedQty'
	                    from [TB_Room] as A
	                    left join TB_dormarea As B on A.DormAreaID=B.ID
	                    left join TB_building as C on a.buildingid=c.id
	                    left join TB_RoomType AS F on a.RoomType=F.ID
	                    left join [TB_Bed] as H	on a.id=H.roomID and H.[Status]=3 AND A.SiteID = {0}
	                    group by B.Name,F.Name,A.[RoomSexType],C.name
                    ) Occupied on TotalBeds.areaname=Occupied.areaname AND TotalBeds.roomtypt=Occupied.roomtypt AND TotalBeds.RoomSexType=Occupied.RoomSexType AND TotalBeds.Name=Occupied.Name
                    LEFT JOIN (
	                    select B.Name areaname,F.Name roomtypt, A.[RoomSexType],C.name,count(H.ID) AS 'VacantQty'
	                    from [TB_Room] as A
	                    left join TB_dormarea As B on A.DormAreaID=B.ID
	                    left join TB_building as C on a.buildingid=c.id
	                    left join TB_RoomType AS F on a.RoomType=F.ID
	                    left join [TB_Bed] as H	on a.id=H.roomID and H.[Status]<>3 AND A.SiteID = {0}
                        WHERE H.IsEnable<>'已禁用' OR H.IsEnable is NULL
	                    group by B.Name,F.Name,A.[RoomSexType],C.name
                    ) Vacant ON TotalBeds.areaname=Vacant.areaname AND TotalBeds.roomtypt=Vacant.roomtypt AND TotalBeds.RoomSexType=Vacant.RoomSexType AND TotalBeds.Name=Vacant.Name
                    group by ROLLUP  (TotalBeds.areaname ,TotalBeds.roomtypt , TotalBeds.[RoomSexType] ,TotalBeds.name) ",
                    siteID
                );
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                //db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, 2);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                DataRow[] dr = dt.Select("IsNull([grade],   ' ')   <>   ' '", "areaname,grade,DormNo ");
                DataTable dtReturn = dt.Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dtReturn.ImportRow(dr[i]);
                }
                return dtReturn;
            }
            catch (Exception ex)
            {
                return null;
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
        /// 获取日期的连续数据
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetDate(string startDay, string endDay)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"exec Proc_Get_Date "+"'"+ startDay+"'"+ "," +"'"+ endDay+"'");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                //db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, 2);
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
        /// 获取视图数据
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetDataByView(string viewTableName)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select * From "+viewTableName);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                //db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, 2);
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
        /// 获取日期的连续数据
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetALLCheckInByDate(string strDay)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT COUNT(1) COUNT from TB_EmployeeCheckIn where CheckInDate<='"+ strDay+" 23:59:59"+"'");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                //db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, 2);
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
        /// 批量插入数据
        /// </summary>
        /// <param name="constring"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BulkToDB( DataTable dt)
        {
            try
            {
                

                string constring = DBO.GetConStr();

                //声明SqlBulkCopy ,using释放非托管资源
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(constring))
                {
                    //一次批量的插入的数据量
                    sqlBC.BatchSize = 3000;

                    //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                    sqlBC.BulkCopyTimeout = 60;

                    //设置要批量写入的表
                    sqlBC.DestinationTableName = "TB_DayRepotTemp";


                    //将dt的列名改为数据库字段名
                    //for (int i = 0; i < dt.Columns.Count; i++)
                    //{
                    //    dt.Columns[i].ColumnName = dt.Rows[0][i].ToString();
                    //}
                    dt.Columns[0].ColumnName = "宿舍区";
                    dt.Columns[1].ColumnName = "入住级别";
                    dt.Columns[2].ColumnName = "项目";
                    dt.Columns[3].ColumnName = "数量";
                    dt.Columns[4].ColumnName = "CheckInDate";
                    dt.Columns[5].ColumnName = "CreateUser";
                  

                    //自定义的OleDbDataReader和数据库的字段进行对应
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sqlBC.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }

                    //删除数据库字段行
                    //dt.Rows.RemoveAt(0);

                    //批量写入
                    sqlBC.WriteToServer(dt);

                    return true;
                }


            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }

        /// <summary>
        /// 获取到DayDormInfo
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetDayDormInfoBySiteID(int siteID, string startDay, string endDay,string dataType)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string viewTableName = "TB_DayDormReport";
                StringBuilder strBuilder = new StringBuilder(@"exec Proc_GetDayDormInfo '" + viewTableName + "',"+ "N'''" + startDay+ "'''," + "N'''" + endDay+ "''',"+"'"+ dataType + "'");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                //db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, 2);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];

               

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
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
        /// 获取到DayDormInfo
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetMonthDormInfoBySiteID(int siteID, string startDay, string projectName, string dataType)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                startDay = startDay+ "-01 00:00:00";
                StringBuilder strBuilder = new StringBuilder(@"Select Areaname,Roomtypt,'" + projectName + @"' project,cast(AVG(" + dataType + @") as decimal(18,1)) 平均,MAX(" + dataType + @") 最大,MIN(" + dataType + @") 最小 From TB_DayDormReport 
                                            Where YEAR(Createdate)= YEAR('"+ startDay + @"') and Month([Createdate]) = Month('" + startDay + @"') 
                                            GROUP BY Areaname,Roomtypt");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                //db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, 2);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                if (dbCommandWrapper != null)
                {
                    dbCommandWrapper = null;
                }
            }
        }

        public bool DelTepm(string operatorUser)
        {
           
            DbCommand dbCommandWrapper = null;
            try
            {
               
                Database db = DBO.GetInstance();
               

                string strDelTemp = @"delete from TB_DayRepotTemp where CreateUser='" + operatorUser + "'";

                dbCommandWrapper = db.GetSqlStringCommand(strDelTemp);
                db.ExecuteNonQuery(dbCommandWrapper);

                return true;
            }
            catch (Exception ex)
            {
                return false;
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
        /// BegCount
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetBegCountBySiteID(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"exec Proc_GetAllBegCount ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                //db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, 2);
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
        /// 获取到site的所有房间
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetTableBySiteID(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select A.[ID]
      ,A.[Name]
      ,A.[RoomSexType]
      ,A.[RoomType]
      ,A.[RoomType2]
      ,A.[Creator]
      ,A.[SiteID]
      ,A.[DormAreaID]
      ,A.[BuildingID]
,A.[KeyCount]
	  ,B.Name as DormAreaName 
	  ,C.name as BuildingName
from [TB_Room] as A
left join TB_dormarea As B
on A.DormAreaID=B.ID
left join TB_building as C
on a.buildingid=c.id ");
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
            string strSql = @"DELETE FROM [TB_Room] WHERE ID in (" + strID + ")";
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
