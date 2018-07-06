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
    public class TB_EmployeeCheckInDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_EmployeeCheckIn info)
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
        public int Create(TB_EmployeeCheckIn info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO TB_EmployeeCheckIn ([RoomID]
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
      ,[Creator]
      ,[SiteID]
      ,[IsActive]
      ,EmployeeTypeName
)
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
      ,@Creator
      ,@SiteID
,@IsActive
,@EmployeeTypeName)";
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
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, info.Creator);
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, info.IsActive);
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
        public int Edit(TB_EmployeeCheckIn info)
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
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int EditTB_EmployeeCheckIn(TB_EmployeeCheckIn info)
        {
            Database db = DBO.CreateDatabase();
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_EmployeeCheckIn SET 
       [BU]=@BU
      ,[Telephone]=@Telephone
      ,[CheckInDate]=@CheckInDate
      ,[UpdateBy]=@UpdateBy
      ,[UpdateDate]=@UpdateDate
      ,EmployeeTypeName=@EmployeeTypeName
        WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, info.BU);
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, info.CardNo);
                db.AddInParameter(dbCommandWrapper, "@Telephone", DbType.String, info.Telephone);
                db.AddInParameter(dbCommandWrapper, "@CheckInDate", DbType.DateTime, info.CheckInDate);
                db.AddInParameter(dbCommandWrapper, "@UpdateBy", DbType.String, info.UpdateBy);
                db.AddInParameter(dbCommandWrapper, "@UpdateDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommandWrapper, "@EmployeeTypeName", DbType.String, info.EmployeeTypeName);
                #endregion
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }


        /// <summary>
        /// 事务更新
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Edit(TB_EmployeeCheckIn info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_EmployeeCheckIn SET [RoomID]=@RoomID
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
      ,[UpdateBy]=@UpdateBy
      ,[UpdateDate]=@UpdateDate
                                                            WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 查询入住记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_EmployeeCheckIn tb_EmployeeCheckIn,ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[ID]
      ,A.[EmployeeNo]
      ,A.[Sex]
      ,A.[BU]
      ,A.EmployeeTypeName
      ,I.[Name] as RoomType
      ,A.[CardNo]
      ,A.[Telephone]
      ,A.[Province]
      ,Convert(varchar(12),A.[CheckInDate],111) as CheckInDate
      ,A.[Name]
      ,A.[SiteID]
	  ,B.Name AS BedName 
      ,C.Name AS RoomName
      ,C.RoomSexType
	  ,D.Name As FloorName
	  ,E.Name As UnitName
	  ,F.Name As BuildingName
	  ,G.Name As DormAreaName
      ,B.KeyCount
FROM  [TB_EmployeeCheckIn] AS A
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
ON C.RoomType=I.ID ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                dbCommandWrapper.CommandTimeout = 800;
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

                strBuilder.AppendLine(" AND A.IsActive = @IsActive");
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, (int)TypeManager.IsActive.Valid);

                if (tb_EmployeeCheckIn.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_EmployeeCheckIn.SiteID);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckIn.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_EmployeeCheckIn.EmployeeNo);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckIn.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_EmployeeCheckIn.Name);
                }

                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.CardNo))
                {
                    strBuilder.AppendLine(" AND A.CardNo = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_EmployeeCheckIn.CardNo);
                }

                if (tb_EmployeeCheckIn.CheckInDateBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CheckInDate as Date) between @CheckInDateBegin and @CheckInDateEnd");
                    db.AddInParameter(dbCommandWrapper, "@CheckInDateBegin", DbType.String, tb_EmployeeCheckIn.CheckInDateBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@CheckInDateEnd", DbType.String, tb_EmployeeCheckIn.CheckInDateEnd.ToString("yyyy/MM/dd"));
                }

                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.BU))
                {
                    var spara = string.Format("%{0}%", tb_EmployeeCheckIn.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
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
        /// 查询入住记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_EmployeeCheckIn tb_EmployeeCheckIn, int iDormAreaID, int iRoomTypeID, string sRoomName, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[ID]
      ,A.[EmployeeNo]
      ,A.[Sex]
      ,A.[BU]
      ,A.EmployeeTypeName
      ,I.[Name] as RoomType
      ,A.[CardNo]
      ,A.[Telephone]
      ,A.[Province]
      ,Convert(varchar(12),A.[CheckInDate],111) as CheckInDate
      ,A.[Name]
      ,A.[SiteID]
	  ,B.Name AS BedName 
      ,C.Name AS RoomName
      ,C.RoomSexType
	  ,D.Name As FloorName
	  ,E.Name As UnitName
	  ,F.Name As BuildingName
	  ,G.Name As DormAreaName
      ,B.KeyCount
FROM  [TB_EmployeeCheckIn] AS A
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
ON C.RoomType=I.ID ");
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

                strBuilder.AppendLine(" AND A.IsActive = @IsActive");
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, (int)TypeManager.IsActive.Valid);

                if (tb_EmployeeCheckIn.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_EmployeeCheckIn.SiteID);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckIn.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_EmployeeCheckIn.EmployeeNo);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckIn.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_EmployeeCheckIn.Name);
                }

                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.CardNo))
                {
                    strBuilder.AppendLine(" AND A.CardNo = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_EmployeeCheckIn.CardNo);
                }
                if (tb_EmployeeCheckIn.BUID>0)
                {
                    strBuilder.AppendLine(" AND F.ID = @BUID");
                    db.AddInParameter(dbCommandWrapper, "@BUID", DbType.String, tb_EmployeeCheckIn.BUID);
                }
                if (iDormAreaID>0)
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
                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.BU))
                {
                    var spara = string.Format("%{0}%", tb_EmployeeCheckIn.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.Telephone))
                {
                    strBuilder.AppendLine(" AND A.Telephone =@Telephone");
                    db.AddInParameter(dbCommandWrapper, "@Telephone", DbType.String, tb_EmployeeCheckIn.Telephone);
                }
                if (tb_EmployeeCheckIn.CheckInDateBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CheckInDate as Date) between @CheckInDateBegin and @CheckInDateEnd");
                    db.AddInParameter(dbCommandWrapper, "@CheckInDateBegin", DbType.String, tb_EmployeeCheckIn.CheckInDateBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@CheckInDateEnd", DbType.String, tb_EmployeeCheckIn.CheckInDateEnd.ToString("yyyy/MM/dd"));
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
        /// 查询入住记录, 用来进行excel导出的
        /// </summary>
        /// <param name="tb_EmployeeCheckIn"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_EmployeeCheckIn tb_EmployeeCheckIn, int iDormAreaID, int iRoomTypeID, string sRoomName)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[EmployeeNo] '工号'
	  ,A.[Name] '姓名'
      ,A.BU  '事业部'
      ,A.EmployeeTypeName '用工类型'
      ,A.[Company] '公司'
      ,A.[CardNo] '身份证号码'
      ,A.[Telephone] '手机号码'
      ,Convert(varchar(12),A.[CheckInDate],111) as '入住日期'
	  ,G.Name As '宿舍区'
	  ,F.Name As '楼栋'
	  ,E.Name As '单元'
	  ,D.Name As '楼层'
      ,C.Name AS '房间号'
	  ,B.Name AS '床位号'
      ,JJ.Name 房间类型
    ,'' AS '扣费内容'
    ,'' AS '扣费金额'
FROM  [TB_EmployeeCheckIn] AS A
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
Left join TB_RoomType as JJ on JJ.ID=C.RoomType");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件

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

                strBuilder.AppendLine(" AND A.IsActive = @IsActive");
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, (int)TypeManager.IsActive.Valid);

                strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_EmployeeCheckIn.SiteID);

                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.CardNo))
                {
                    strBuilder.AppendLine(" AND A.CardNo = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, tb_EmployeeCheckIn.CardNo);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckIn.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_EmployeeCheckIn.EmployeeNo);
                }

                if (!String.IsNullOrEmpty(tb_EmployeeCheckIn.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_EmployeeCheckIn.Name);
                }
                if (tb_EmployeeCheckIn.BUID > 0)
                {
                    strBuilder.AppendLine(" AND F.ID = @BUID");
                    db.AddInParameter(dbCommandWrapper, "@BUID", DbType.String, tb_EmployeeCheckIn.BUID);
                }
                if (iDormAreaID > 0)
                {
                    strBuilder.AppendLine(" AND G.ID = @DormAreaID");
                    db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, iDormAreaID);
                }
                if (iRoomTypeID > 0)
                {
                    strBuilder.AppendLine(" AND JJ.ID = @iRoomTypeID");
                    db.AddInParameter(dbCommandWrapper, "@iRoomTypeID", DbType.Int32, iRoomTypeID);
                }
                if (!string.IsNullOrEmpty(sRoomName))
                {
                    strBuilder.AppendLine(" AND C.Name = @sRoomName");
                    db.AddInParameter(dbCommandWrapper, "@sRoomName", DbType.String, sRoomName);
                }
                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.BU))
                {
                    var spara = string.Format("%{0}%", tb_EmployeeCheckIn.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                if (!string.IsNullOrEmpty(tb_EmployeeCheckIn.Telephone))
                {
                    strBuilder.AppendLine(" AND A.Telephone =@Telephone");
                    db.AddInParameter(dbCommandWrapper, "@Telephone", DbType.String, tb_EmployeeCheckIn.Telephone);
                }
                if (tb_EmployeeCheckIn.CheckInDate != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CheckInDate as Date) =@CheckInDate");
                    db.AddInParameter(dbCommandWrapper, "@CheckInDate", DbType.String, tb_EmployeeCheckIn.CheckInDate.ToString("yyyy/MM/dd"));
                }

                //根据时间范围来查询
                if (tb_EmployeeCheckIn.CheckInDateBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CheckInDate as Date) between @CheckInDateBegin and @CheckInDateEnd");
                    db.AddInParameter(dbCommandWrapper, "@CheckInDateBegin", DbType.String, tb_EmployeeCheckIn.CheckInDateBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@CheckInDateEnd", DbType.String, tb_EmployeeCheckIn.CheckInDateEnd.ToString("yyyy/MM/dd"));
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 根据ID查询入住记录
        /// </summary>
        /// <param name="intID"></param>
        /// <returns></returns>
        public DataTable Get(int intID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"  SELECT A.*
	                                    from [TB_EmployeeCheckIn] as A where A.ID=@ID";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, intID);
                strBuilder.AppendLine(" AND A.IsActive = @IsActive");
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, (int)TypeManager.IsActive.Valid);
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
        
        public DataTable GetByWorkID(string sWorkID, string sIDCard)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                var sCond = new StringBuilder();
                if (!string.IsNullOrEmpty(sWorkID)) {
                    sCond.AppendFormat("a.EmployeeNo='{0}'", sWorkID);
                }

                if (!string.IsNullOrEmpty(sWorkID) && !string.IsNullOrEmpty(sIDCard)) {
                    sCond.Append(" or ");
                    sCond.AppendFormat("a.CardNo='{0}'", sIDCard);
                }
                else if (!string.IsNullOrEmpty(sIDCard))
                {
                    sCond.AppendFormat("a.CardNo='{0}'", sIDCard);
                }                

                string strSQL = string.Format("SELECT A.* from [TB_EmployeeCheckIn] as A where 1=1 and ({0})", 
                                                sCond.ToString());
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
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
        /// 根据ID删除
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Delete(int intID, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"DELETE FROM [TB_EmployeeCheckIn] WHERE [ID]=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 根据ID更新状态
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="?"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Update(int intID, TypeManager.IsActive isActive, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"update [TB_EmployeeCheckIn] set IsActive=@IsActive WHERE ID=@ID";
            StringBuilder strBuilder = new StringBuilder(strUpdateSql);
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, (int)isActive);
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
                    dbCommandWrapper.Dispose();
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 获取到一个site的所有入住人员信息
        /// </summary>
        /// <param name="intSiteID"></param>
        /// <returns></returns>
        public DataTable GetTableBySiteID(int intSiteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @" SELECT A.ID,A.CardNo,A.IsActive,EmployeeNo 
	                                    from [TB_EmployeeCheckIn] as A where A.[SiteID]=@SiteID";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.String, intSiteID);
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
            string strSQL = string.Format(@"DELETE FROM [TB_EmployeeCheckIn]
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
