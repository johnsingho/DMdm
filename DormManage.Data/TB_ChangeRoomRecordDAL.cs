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
    public class TB_ChangeRoomRecordDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_ChangeRoomRecord info)
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
        /// 事务添加,已经添加BUID
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Create(TB_ChangeRoomRecord info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO [TB_ChangeRoomRecord] 
      ([SiteID]
      ,[EmployeeNo]
      ,[Name]
      ,[Sex]
      ,[BUID]
      ,[BU]
      ,[Company]
      ,[CardNo]
      ,[Telephone]
      ,[Province]
      ,[IsSmoking]
      ,[CheckInDate]
      ,[ChangeRoomDate]
      ,[Creator]
      ,[OldRoomID]
      ,[OldBedID]
      ,[NewRoomID]
      ,[NewBedID]
,EmployeeTypeName)
                                    VALUES
      (@SiteID
      ,@EmployeeNo
      ,@Name
      ,@Sex
      ,@BUID
      ,@BU
      ,@Company
      ,@CardNo
      ,@Telephone
      ,@Province
      ,@IsSmoking
      ,@CheckInDate
      ,@ChangeRoomDate
      ,@Creator
      ,@OldRoomID
      ,@OldBedID
      ,@NewRoomID
      ,@NewBedID
,@EmployeeTypeName) ";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, info.EmployeeNo);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
                db.AddInParameter(dbCommandWrapper, "@Sex", DbType.Int32, info.Sex);
                db.AddInParameter(dbCommandWrapper, "@BUID", DbType.Int32, info.BUID);
                db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, info.BU);
                db.AddInParameter(dbCommandWrapper, "@Company", DbType.String, info.Company);
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, info.CardNo);
                db.AddInParameter(dbCommandWrapper, "@Telephone", DbType.String, info.Telephone);
                db.AddInParameter(dbCommandWrapper, "@Province", DbType.String, info.Province);
                db.AddInParameter(dbCommandWrapper, "@IsSmoking", DbType.Boolean, info.IsSmoking);
                db.AddInParameter(dbCommandWrapper, "@CheckInDate", DbType.DateTime, info.CheckInDate);
                db.AddInParameter(dbCommandWrapper, "@ChangeRoomDate", DbType.DateTime, info.ChangeRoomDate);
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, info.Creator);
                db.AddInParameter(dbCommandWrapper, "@OldRoomID", DbType.Int32, info.OldRoomID);
                db.AddInParameter(dbCommandWrapper, "@OldBedID", DbType.Int32, info.OldBedID);
                db.AddInParameter(dbCommandWrapper, "@NewRoomID", DbType.Int32, info.NewRoomID);
                db.AddInParameter(dbCommandWrapper, "@NewBedID", DbType.Int32, info.NewBedID);
                db.AddInParameter(dbCommandWrapper, "@EmployeeTypeName", DbType.String, info.EmployeeTypeName);
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
        /// 获取到换房记录分页数据,已经添加BUID
        /// </summary>
        /// <param name="tb_ChangeRoomRecord"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_ChangeRoomRecord tb_ChangeRoomRecord, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                //TODO 2018-02-07 
                //由于 EHR.[Segment] 的ID 与 DormManage.[TB_BU]根本不对应
                //因此，ON A.BUID=N.ID 是不会成立的
                //现在事业部直接取 TB_ChangeRoomRecord.[BU]
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[ID]
      ,A.[EmployeeNo]
      ,A.[Sex]
      ,A.[BU] AS BU
      ,A.[Company]
      ,A.EmployeeTypeName
      , A.[CardNo]
      ,A.[Telephone]
      ,A.[Province]
      ,Convert(varchar(12),A.[CheckInDate],111) as CheckInDate
      ,Convert(varchar(12),A.[ChangeRoomDate],111) as ChangeRoomDate
      ,A.[Name]
      ,A.[SiteID]
	  ,B.Name+'->'+'<span style=color:red>'+H.Name+'</span>' AS BedName 
      ,C.Name+'->'+'<span style=color:red>'+I.Name+'</span>' AS RoomName
      ,OldRoomType.Name+'->'+'<span style=color:red>'+OldRoomType.Name+'</span>' AS RoomType
	  ,D.Name+'->'+'<span style=color:red>'+J.Name+'</span>' As FloorName
	  ,E.Name+'->'+'<span style=color:red>'+K.Name+'</span>' As UnitName
	  ,F.Name+'->'+'<span style=color:red>'+L.Name+'</span>' As BuildingName
	  ,G.Name+'->'+'<span style=color:red>'+M.Name+'</span>' As DormAreaName
FROM [TB_ChangeRoomRecord] AS A
LEFT JOIN [TB_Bed] AS B
ON A.[OldBedID]=B.ID
LEFT JOIN [TB_Room] AS C
ON B.[RoomID]=C.ID
LEFT JOIN [TB_Floor] AS D
ON B.FloorID=D.ID
LEFT JOIN [TB_Unit] AS E
ON B.UnitID=E.ID
LEFT JOIN [TB_Building] AS F
on B.BuildingID=F.ID
LEFT JOIN [TB_DormArea] AS G
ON B.DormAreaID=G.ID
LEFT JOIN [TB_Bed] AS H
ON A.[NewBedID]=H.ID
LEFT JOIN [TB_Room] AS I
ON H.[RoomID]=I.ID
LEFT JOIN [TB_Floor] AS J
ON H.FloorID=J.ID
LEFT JOIN [TB_Unit] AS K
ON H.UnitID=K.ID
LEFT JOIN [TB_Building] AS L
on H.BuildingID=L.ID
LEFT JOIN [TB_DormArea] AS M
ON H.DormAreaID=M.ID
LEFT JOIN TB_BU AS N
ON A.BUID=N.ID
LEFT JOIN TB_RoomType AS OldRoomType
ON C.RoomType=OldRoomType.ID
LEFT JOIN TB_RoomType AS NewRoomType
ON I.RoomType=NewRoomType.ID ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@" LEFT JOIN [TB_UserConnectDormArea] AS O
on G.ID=O.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND O.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }
                strBuilder.AppendLine(" AND A.[SiteID] = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_ChangeRoomRecord.SiteID);
                if (!string.IsNullOrEmpty(tb_ChangeRoomRecord.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.[EmployeeNo] = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_ChangeRoomRecord.EmployeeNo);

                }
                if (!string.IsNullOrEmpty(tb_ChangeRoomRecord.CardNo))
                {
                    strBuilder.AppendLine(" AND A.[CardNo] = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_ChangeRoomRecord.CardNo);
                }
                if (tb_ChangeRoomRecord.ChangeRoomDateBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(ChangeRoomDate as Date) between @ChangeRoomDateBegin and @ChangeRoomDateEnd");
                    db.AddInParameter(dbCommandWrapper, "@ChangeRoomDateBegin", DbType.String, tb_ChangeRoomRecord.ChangeRoomDateBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@ChangeRoomDateEnd", DbType.String, tb_ChangeRoomRecord.ChangeRoomDateEnd.ToString("yyyy/MM/dd"));
                }
                if (!string.IsNullOrEmpty(tb_ChangeRoomRecord.BU))
                {
                    var spara = string.Format("%{0}%", tb_ChangeRoomRecord.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
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
        /// 获取到所有的换房记录
        /// </summary>
        /// <param name="tb_ChangeRoomRecord"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_ChangeRoomRecord tb_ChangeRoomRecord)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                //TODO 2018-02-07 
                //由于 EHR.[Segment] 的ID 与 DormManage.[TB_BU]根本不对应
                //因此，ON A.BUID=N.ID 是不会成立的
                //现在事业部直接取 TB_ChangeRoomRecord.[BU]
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[EmployeeNo] as '工号'
      ,A.[Name]  as '姓名'
      ,A.[BU] AS '事业部'
      ,A.[Company] as '公司'
      ,A.[CardNo] as '身份证号码'
      ,A.[Telephone] as '电话号码'
      ,Convert(varchar(12),A.[CheckInDate],111) as  '入住日期'
      ,Convert(varchar(12),A.[ChangeRoomDate],111) as  '换房日期'
	  ,G.Name As '原宿舍区'
	  ,F.Name As '原楼栋'
	  ,E.Name As '原单元'
	  ,D.Name As '原楼层'
      ,C.Name AS '原房间号'
	  ,B.Name AS  '原床位号'
	  ,M.Name As '新宿舍区'
	  ,L.Name As '新楼栋'
	  ,K.Name As '新单元'
	  ,J.Name As '新楼层'
      ,I.Name AS '新房间号'
	  ,H.Name AS  '新床位号'
FROM [TB_ChangeRoomRecord] AS A
LEFT JOIN [TB_Bed] AS B
ON A.[OldBedID]=B.ID
LEFT JOIN [TB_Room] AS C
ON B.[RoomID]=C.ID
LEFT JOIN [TB_Floor] AS D
ON B.FloorID=D.ID
LEFT JOIN [TB_Unit] AS E
ON B.UnitID=E.ID
LEFT JOIN [TB_Building] AS F
on B.BuildingID=F.ID
LEFT JOIN [TB_DormArea] AS G
ON B.DormAreaID=G.ID
LEFT JOIN [TB_Bed] AS H
ON A.[NewBedID]=H.ID
LEFT JOIN [TB_Room] AS I
ON H.[RoomID]=I.ID
LEFT JOIN [TB_Floor] AS J
ON H.FloorID=J.ID
LEFT JOIN [TB_Unit] AS K
ON H.UnitID=K.ID
LEFT JOIN [TB_Building] AS L
on H.BuildingID=L.ID
LEFT JOIN [TB_DormArea] AS M
ON H.DormAreaID=M.ID
LEFT JOIN TB_BU AS N
ON A.BUID=N.ID");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@" LEFT JOIN [TB_UserConnectDormArea] AS O
on G.ID=O.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND O.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }
                strBuilder.AppendLine(" AND A.[SiteID] = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_ChangeRoomRecord.SiteID);
                if (!string.IsNullOrEmpty(tb_ChangeRoomRecord.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.[EmployeeNo] = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_ChangeRoomRecord.EmployeeNo);

                }
                if (!string.IsNullOrEmpty(tb_ChangeRoomRecord.CardNo))
                {
                    strBuilder.AppendLine(" AND A.[CardNo] = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_ChangeRoomRecord.CardNo);
                }
                if (tb_ChangeRoomRecord.ChangeRoomDateBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(ChangeRoomDate as Date) between @ChangeRoomDateBegin and @ChangeRoomDateEnd");
                    db.AddInParameter(dbCommandWrapper, "@ChangeRoomDateBegin", DbType.String, tb_ChangeRoomRecord.ChangeRoomDateBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@ChangeRoomDateEnd", DbType.String, tb_ChangeRoomRecord.ChangeRoomDateEnd.ToString("yyyy/MM/dd"));
                }

                if (!string.IsNullOrEmpty(tb_ChangeRoomRecord.BU))
                {
                    var spara = string.Format("%{0}%", tb_ChangeRoomRecord.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                #endregion
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
        /// <param name="strBedID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteByBedID(string strBedID, DbTransaction tran, Database db)
        {
            if (String.IsNullOrEmpty(strBedID))
                return 0;
            DbCommand dbCommandWrapper = null;
            string strSQL = string.Format(@"DELETE FROM [TB_ChangeRoomRecord]
                                            WHERE [OldBedID] in ({0}) or [NewBedID] in ({0})", strBedID);
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
