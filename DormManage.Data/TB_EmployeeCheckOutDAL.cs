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
using DormManage.Framework.LogManager;

namespace DormManage.Data.DAL
{
    public class TB_EmployeeCheckOutDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_EmployeeCheckOut info)
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
        public int Create(TB_EmployeeCheckOut info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO TB_EmployeeCheckOut ([RoomID]
      ,[BedID]
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
      ,[CheckOutDate]
      ,[Creator]
      ,[SiteID]
,[Reason]
,[Remark]
,[EmployeeTypeName])
                                    VALUES(@RoomID
      ,@BedID
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
      ,@CheckOutDate
      ,@Creator
      ,@SiteID
,@Reason
,@Remark
,@EmployeeTypeName) ";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                db.AddInParameter(dbCommandWrapper, "@BedID", DbType.Int32, info.BedID);
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
                db.AddInParameter(dbCommandWrapper, "@CheckOutDate", DbType.DateTime, info.CheckOutDate);
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, info.Creator);
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@Reason", DbType.String, info.Reason);
                db.AddInParameter(dbCommandWrapper, "@Remark", DbType.String, info.Remark);
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Edit(TB_EmployeeCheckOut info)
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
        public int Edit(TB_EmployeeCheckOut info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_EmployeeCheckOut SET [RoomID]=@RoomID
      ,[BedID]=@BedID
      ,[EmployeeNo]=@EmployeeNo
      ,[Name]=@Name
      ,[Sex]=@Sex
      ,[BUID]=@BUID
      ,[BU]=@BU
      ,[Company]=@Company
      ,[CardNo]=@CardNo
      ,[Telephone]=@Telephone
      ,[Province]=@Province
      ,[IsSmoking]=@IsSmoking
      ,[CheckInDate]=@CheckInDate
      ,[CheckOutDate]=@CheckOutDate
      ,[UpdateBy]=@UpdateBy
      ,[UpdateDate]=@UpdateDate
      ,[Reason]=@Reason
 ,[Remark]=@Remark
                                                            WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                db.AddInParameter(dbCommandWrapper, "@BedID", DbType.Int32, info.BedID);
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.Int32, info.EmployeeNo);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.Int32, info.Name);
                db.AddInParameter(dbCommandWrapper, "@Sex", DbType.Int32, info.Sex);
                db.AddInParameter(dbCommandWrapper, "@BUID", DbType.Int32, info.BUID);
                db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, info.BU);
                db.AddInParameter(dbCommandWrapper, "@Company", DbType.String, info.Company);
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, info.CardNo);
                db.AddInParameter(dbCommandWrapper, "@Telephone", DbType.String, info.Telephone);
                db.AddInParameter(dbCommandWrapper, "@Province", DbType.String, info.Province);
                db.AddInParameter(dbCommandWrapper, "@IsSmoking", DbType.Boolean, info.IsSmoking);
                db.AddInParameter(dbCommandWrapper, "@CheckInDate", DbType.DateTime, info.CheckInDate);
                db.AddInParameter(dbCommandWrapper, "@CheckOutDate", DbType.DateTime, info.CheckOutDate);
                db.AddInParameter(dbCommandWrapper, "@UpdateBy", DbType.String, info.UpdateBy);
                db.AddInParameter(dbCommandWrapper, "@UpdateDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommandWrapper, "@Reason", DbType.String, info.Reason);
                db.AddInParameter(dbCommandWrapper, "@Remark", DbType.String, info.Remark);
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 获取退房记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckOut"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_EmployeeCheckOut tb_EmployeeCheckOut, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[ID]
      ,A.[EmployeeNo]
      ,A.[Sex]
      ,I.Name  [RoomType]
      ,A.[Company]
      ,A.EmployeeTypeName
      ,A.[BU]
      ,A.[CardNo]
      ,A.[Telephone]
      ,A.[Province]
      ,Convert(varchar(12),A.[CheckInDate],111) as CheckInDate
	  ,Convert(varchar(12),A.[CheckOutDate],111) as CheckOutDate
      ,A.[Name]
      ,A.[SiteID]
      ,A.[Reason]
      ,A.[Remark]
	  ,B.Name AS BedName 
      ,C.Name AS RoomName
	  ,D.Name As FloorName
	  ,E.Name As UnitName
	  ,F.Name As BuildingName
	  ,G.Name As DormAreaName
FROM  [TB_EmployeeCheckOut] AS A
LEFT JOIN [TB_Bed] AS B
ON A.[BedID]=B.ID
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
LEFT JOIN TB_RoomType AS I
ON C.RoomType=I.ID");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@" inner join [TB_UserConnectDormArea] AS H
on G.ID=H.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND H.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }

                if (tb_EmployeeCheckOut.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_EmployeeCheckOut.SiteID);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_EmployeeCheckOut.EmployeeNo);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_EmployeeCheckOut.Name);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.CardNo))
                {
                    strBuilder.AppendLine(" AND A.CardNo = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_EmployeeCheckOut.CardNo);
                }

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
                dbCommandWrapper.CommandTimeout = 60; // johnsing he 2018-04-27 临时措施
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        public int ChangeCheckOutReason(int id, string sReason, int nCanLeave, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_EmployeeCheckOut SET Reason=@Reason, "
                                  + "CanLeave=@CanLeave, UpdateDate=@UpdateDate"
                                  + " WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters                
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, id);
                db.AddInParameter(dbCommandWrapper, "@Reason", DbType.String, sReason);
                db.AddInParameter(dbCommandWrapper, "@CanLeave", DbType.Int32, nCanLeave);
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 获取退房记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckOut"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_EmployeeCheckOut tb_EmployeeCheckOut, int iDormAreaID, int iRoomTypeID, string sRoomName, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[ID]
      ,A.[EmployeeNo]
      ,A.[Sex]
      ,I.Name  [RoomType]
      ,A.[Company]
      ,A.EmployeeTypeName
      ,A.[BU]
      ,A.[CardNo]
      ,A.[Telephone]
      ,A.[Province]
      ,Convert(varchar(12),A.[CheckInDate],111) as CheckInDate
	  ,Convert(varchar(12),A.[CheckOutDate],111) as CheckOutDate
      ,A.[Name]
      ,A.[SiteID]
      ,A.[Reason]
      ,A.[Remark]
      ,case when A.[CanLeave] IS NULL then 1 else A.[CanLeave] end as CanLeave
	  ,B.Name AS BedName 
      ,C.Name AS RoomName
	  ,D.Name As FloorName
	  ,E.Name As UnitName
	  ,F.Name As BuildingName
	  ,G.Name As DormAreaName
FROM  [TB_EmployeeCheckOut] AS A
LEFT JOIN [TB_Bed] AS B
ON A.[BedID]=B.ID
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
LEFT JOIN TB_RoomType AS I
ON C.RoomType=I.ID");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@" inner join [TB_UserConnectDormArea] AS H
on G.ID=H.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND H.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }

                if (tb_EmployeeCheckOut.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_EmployeeCheckOut.SiteID);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_EmployeeCheckOut.EmployeeNo);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_EmployeeCheckOut.Name);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.CardNo))
                {
                    strBuilder.AppendLine(" AND A.CardNo = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_EmployeeCheckOut.CardNo);
                }

                if (tb_EmployeeCheckOut.BUID > 0)
                {
                    strBuilder.AppendLine(" AND F.ID = @BUID");
                    db.AddInParameter(dbCommandWrapper, "@BUID", DbType.String, tb_EmployeeCheckOut.BUID);
                }
                if (iDormAreaID > 0)
                {
                    strBuilder.AppendLine(" AND G.ID = @DormAreaID");
                    db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, iDormAreaID);
                }

                if (iRoomTypeID > 0)
                {
                    strBuilder.AppendLine(" AND I.ID = @iRoomTypeID");
                    db.AddInParameter(dbCommandWrapper, "@iRoomTypeID", DbType.Int32, iRoomTypeID);
                }
                if (!string.IsNullOrEmpty(sRoomName))
                {
                    strBuilder.AppendLine(" AND C.Name = @sRoomName");
                    db.AddInParameter(dbCommandWrapper, "@sRoomName", DbType.String, sRoomName);
                }
                if (!string.IsNullOrEmpty(tb_EmployeeCheckOut.BU))
                {
                    var spara = string.Format("%{0}%", tb_EmployeeCheckOut.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                //if (tb_EmployeeCheckOut.CheckOutDate != default(DateTime))
                //{
                //    strBuilder.AppendLine(" AND CAST(CheckOutDate as Date)=@coDate");
                //    db.AddInParameter(dbCommandWrapper, "@coDate", DbType.String, tb_EmployeeCheckOut.CheckOutDate.ToString("yyyy-MM-dd"));
                //}
                if (tb_EmployeeCheckOut.CheckOutDateBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CheckOutDate as Date) between @CheckOutDateBegin and @CheckOutDateEnd");
                    db.AddInParameter(dbCommandWrapper, "@CheckOutDateBegin", DbType.String, tb_EmployeeCheckOut.CheckOutDateBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@CheckOutDateEnd", DbType.String, tb_EmployeeCheckOut.CheckOutDateEnd.ToString("yyyy/MM/dd"));
                }

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
                dbCommandWrapper.CommandTimeout = 60; // Johnsing 2018-04-27 临时措施
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("TB_EmployeeCheckOutDAL::GetTable", ex);
                throw ex;
            }
            finally
            {
                if (dbCommandWrapper != null)
                {
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 获取退房记录, 用于EXCEL导出
        /// </summary>
        /// <param name="tb_EmployeeCheckOut"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_EmployeeCheckOut tb_EmployeeCheckOut,int iDormAreaID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[EmployeeNo] '工号'
      ,A.[Name] '姓名'
      ,A.[BU] '事业部'
      ,A.[Company] '公司'
      ,A.[CardNo] '身份证号码'
      ,A.[Telephone] '手机号码'
      ,Convert(varchar(12),A.[CheckInDate],111) as '入住日期'
	  ,Convert(varchar(12),A.[CheckOutDate],111) as '退房日期'
      ,A.[Reason] as '原因'
      ,A.[Remark] as '备注'
      ,case when (A.[CanLeave] IS NULL OR A.[CanLeave]>0) then '是' else '否' end AS '同步签退离职系统'
	  ,G.Name As '宿舍区'
	  ,F.Name As '楼栋'
	  ,E.Name As '单元'
	  ,D.Name As '楼层'
      ,C.Name AS '房间号'
	  ,B.Name AS '床位号' 
      ,J.Name as '房间类型'
FROM  [TB_EmployeeCheckOut] AS A
LEFT JOIN [TB_Bed] AS B
ON A.[BedID]=B.ID
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
LEFT JOIN TB_BU AS I
ON A.BUID=I.ID 
LEFT JOIN TB_RoomType AS J
ON C.RoomType=J.ID
");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@" inner join [TB_UserConnectDormArea] AS H
on G.ID=H.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND H.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }

                if (tb_EmployeeCheckOut.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_EmployeeCheckOut.SiteID);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_EmployeeCheckOut.EmployeeNo);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_EmployeeCheckOut.Name);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckOut.CardNo))
                {
                    strBuilder.AppendLine(" AND A.CardNo = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_EmployeeCheckOut.CardNo);
                }
                if (tb_EmployeeCheckOut.BUID > 0)
                {
                    strBuilder.AppendLine(" AND F.ID = @BUID");
                    db.AddInParameter(dbCommandWrapper, "@BUID", DbType.String, tb_EmployeeCheckOut.BUID);
                }
                if (iDormAreaID > 0)
                {
                    strBuilder.AppendLine(" AND G.ID = @DormAreaID");
                    db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, iDormAreaID);
                }

                if (tb_EmployeeCheckOut.RoomID > 0)
                {
                    strBuilder.AppendLine(" AND J.ID = @iRoomTypeID");
                    db.AddInParameter(dbCommandWrapper, "@iRoomTypeID", DbType.Int32, tb_EmployeeCheckOut.RoomID);
                }

                if (!string.IsNullOrEmpty(tb_EmployeeCheckOut.BU))
                {
                    var spara = string.Format("%{0}%", tb_EmployeeCheckOut.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                //if (tb_EmployeeCheckOut.CheckOutDate != default(DateTime))
                //{
                //    strBuilder.AppendLine(" AND CAST(CheckOutDate as Date)=@coDate");
                //    db.AddInParameter(dbCommandWrapper, "@coDate", DbType.String, tb_EmployeeCheckOut.CheckOutDate.ToString("yyyy-MM-dd"));
                //}
                if (tb_EmployeeCheckOut.CheckOutDateBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CheckOutDate as Date) between @CheckOutDateBegin and @CheckOutDateEnd");
                    db.AddInParameter(dbCommandWrapper, "@CheckOutDateBegin", DbType.String, tb_EmployeeCheckOut.CheckOutDateBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@CheckOutDateEnd", DbType.String, tb_EmployeeCheckOut.CheckOutDateEnd.ToString("yyyy/MM/dd"));
                }

                dbCommandWrapper.CommandTimeout = 60; // Johnsing 2018-04-27 临时措施
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
                    dbCommandWrapper.Dispose();
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
            string strSQL = string.Format(@"DELETE FROM [TB_EmployeeCheckOut]
                                            WHERE [BedID] in ({0})", strBedID);
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }
    }
}
