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
    public class TB_AssignRoomDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_AssignRoom info)
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
        public int Create(TB_AssignRoom info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO [TB_AssignRoom] ([RoomID]
      ,[isActive],[BedID],[SiteID],[Creator]) VALUES(@RoomID,@isActive,@BedID,@SiteID,@Creator)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@RoomID", DbType.Int32, info.RoomID);
                db.AddInParameter(dbCommandWrapper, "@isActive", DbType.String, (int)TypeManager.IsActive.Valid);
                db.AddInParameter(dbCommandWrapper, "@BedID", DbType.String, info.BedID);
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.String, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, info.Creator);
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
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public DataTable GetTable(string strCardNo, string strStatus)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select * from [TB_EmployeeCheckIn] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;

                if (!string.IsNullOrEmpty(strCardNo))
                {
                    strBuilder.AppendLine(" AND [CardNo] = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, strCardNo);
                }

                strBuilder.AppendLine(" and [IsActive]=1 ");
               
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
        /// 获取到分页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="strCardNo"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public DataTable GetTable(ref Pager pager, string strCardNo, string strStatus)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"SELECT A.[ID]
      ,A.[RoomID]
      ,A.[isActive]
      ,A.[BedID]
      ,A.[SiteID]
	  ,B.Name as BedName
	  ,B.DormAreaID
	  ,B.BuildingID
	  ,C.Name as BuildingName
	  ,D.Name as UnitName
	  ,E.Name as FloorName
	  ,F.Name as DormAreaName
	  ,G.Name as RoomName
	  ,G.[RoomType2] 
	  ,G.[RoomType]
	  ,G.[RoomSexType]
	  ,case G.[RoomType2]
	  when 1 then '员工宿舍'
	  when 2 then '家庭房'
	  else ''
	  end as RoomType2Name
	  ,H.Name as RoomTypeName
	  ,I.[EmployeeNo]
      ,I.[BU]
      ,I.[Company]
      ,I.[CardNo]
	  ,I.Name
      ,I.CheckInDate
	  ,I.ID as EmployeeCheckInID
 ,I.Telephone
  FROM [TB_AssignRoom] AS A
left join TB_Bed As B
on A.[BedID]=B.ID
left join TB_building as C
on B.buildingid=c.id
left join TB_Unit AS D
on B.UnitID=D.ID
left join TB_Floor as E
on B.FloorID=E.ID
left join TB_DormArea AS F
on B.DormAreaID=F.ID
left join TB_Room As G
on B.RoomID=G.ID
left join TB_RoomType as H
on G.RoomType=H.ID
left join [TB_EmployeeCheckIn] as I
on A.[BedID]=I.[BedID] ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                if (null != System.Web.HttpContext.Current.Session[TypeManager.User])
                {
                    strBuilder.AppendLine(@"inner join [TB_UserConnectDormArea] AS J
on F.ID=J.[DormAreaID]
where 1=1");
                    strBuilder.AppendLine(" AND J.[UserID] = @UserID");
                    db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).ID);
                }
                else
                {
                    strBuilder.AppendLine(" where 1=1");
                }
                strBuilder.AppendLine(" AND A.[SiteID] = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, System.Web.HttpContext.Current.Session[TypeManager.Admin] == null ?
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).SiteID :
                ((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).SiteID);
                if (!string.IsNullOrEmpty(strCardNo))
                {
                    strBuilder.AppendLine(" AND I.[CardNo] = @CardNo");
                    db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, strCardNo);
                }
                switch (strStatus)
                {
                    case "0":
                        strBuilder.AppendLine(" AND (I.[CardNo] = '' or I.[CardNo] is null)");
                        break;
                    case "1":
                        strBuilder.AppendLine(" AND I.[CardNo] <> '' and I.[CardNo] is not null and I.[IsActive]=0 ");
                        break;
                    default: break;
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
                    dbCommandWrapper = null;
                }
            }
        }

        public DataTable GetTable(TB_AssignRoom info)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select [ID]
      ,[RoomID]
      ,[isActive]
      ,[BedID]
      ,[SiteID] from [TB_AssignRoom] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
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
        /// 根据ID删除
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Delete(int intID, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strSQL = string.Format(@"DELETE FROM [TB_AssignRoom]
                                            WHERE [ID]={0}", intID);
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

        /// <summary>
        /// 根据床位ID删除
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
            string strSQL = string.Format(@"DELETE FROM [TB_AssignRoom]
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
                    dbCommandWrapper = null;
                }
            }
        }

        public DataTable GetAssignDormInfo(string IDCardNo)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"select * from TB_AssignDormArea where ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" CardNo=@IDCardNo ");
                db.AddInParameter(dbCommandWrapper, "@IDCardNo", DbType.String, IDCardNo);
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

        public int DelAssignDormInfo(string IDCardNo)
        {
           
            DbCommand dbCommandWrapper = null;
            try
            {
                StringBuilder strBuilder = new StringBuilder(@"Delete from TB_AssignDormArea where ");
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" CardNo=@IDCardNo ");
                db.AddInParameter(dbCommandWrapper, "@IDCardNo", DbType.String, IDCardNo);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                int i = db.ExecuteNonQuery(dbCommandWrapper);
                return i;
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

        public int AddAssignDormInfo(TB_AssignDormArea info)
        {
            Database db = DBO.GetInstance();
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO [TB_AssignDormArea] ([DormAreaID]
      ,[CardNo],[EmployeeNo],[CreateUser],[CreateDate]) VALUES(@DormAreaID,@CardNo,@EmployeeNo,@CreateUser,@CreateDate)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@DormAreaID", DbType.Int32, info.DormAreaID);
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, info.CardNo);
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, info.EmployeeNo);
                db.AddInParameter(dbCommandWrapper, "@CreateUser", DbType.String, info.CreateUser);
                db.AddInParameter(dbCommandWrapper, "@CreateDate", DbType.DateTime, info.CreateDate);
                #endregion
                intId = Convert.ToInt32(db.ExecuteScalar(dbCommandWrapper));
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
