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
    public class TB_ChargingDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_Charging info, DbTransaction tran)
        {
            try
            {
                Database db = DBO.CreateDatabase();
                return this.Create(info, tran, db);
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
        public int Create(TB_Charging info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO TB_Charging([EmployeeNo]
      ,[Name]
      ,[CardNo]
      ,[ChargeContent]
      ,[Money]
      ,[AirConditionFee]
      ,[AirConditionFeeMoney]
      ,[RoomKeyFee]
      ,[RoomKeyFeeMoney]
      ,[OtherFee]
      ,[OtherFeeMoney]
      ,[Creator]
      ,[SiteID]
,[BUID]
,[BU]
) VALUES(@EmployeeNo
      ,@Name
      ,@CardNo
      ,@ChargeContent
      ,@Money
      ,@AirConditionFee
      ,@AirConditionFeeMoney
      ,@RoomKeyFee
      ,@RoomKeyFeeMoney
      ,@OtherFee
      ,@OtherFeeMoney
      ,@Creator
      ,@SiteID
,@BUID
,@BU)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, info.EmployeeNo);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, info.CardNo);
                db.AddInParameter(dbCommandWrapper, "@ChargeContent", DbType.String, info.ChargeContent);
                db.AddInParameter(dbCommandWrapper, "@Money", DbType.Decimal, info.Money);
                db.AddInParameter(dbCommandWrapper, "@AirConditionFee", DbType.String, info.AirConditionFee);
                db.AddInParameter(dbCommandWrapper, "@AirConditionFeeMoney", DbType.Decimal, info.AirConditionFeeMoney);
                db.AddInParameter(dbCommandWrapper, "@RoomKeyFee", DbType.String, info.RoomKeyFee);
                db.AddInParameter(dbCommandWrapper, "@RoomKeyFeeMoney", DbType.Decimal, info.RoomKeyFeeMoney);
                db.AddInParameter(dbCommandWrapper, "@OtherFee", DbType.String, info.OtherFee);
                db.AddInParameter(dbCommandWrapper, "@OtherFeeMoney", DbType.Decimal, info.OtherFeeMoney);
                db.AddInParameter(dbCommandWrapper, "@Creator", DbType.String, info.Creator);
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.String, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@BUID", DbType.String, info.BUID);
                db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, info.BU);
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
        public int Edit(TB_Charging info)
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
        public int Edit(TB_Charging info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_Charging SET EmployeeNo=@EmployeeNo
      ,Name=@Name
      ,CardNo=@CardNo
      ,ChargeContent=@ChargeContent
      ,Money=@Money
      ,AirConditionFee=@AirConditionFee
      ,AirConditionFeeMoney=@AirConditionFeeMoney
      ,RoomKeyFee=@RoomKeyFee
      ,RoomKeyFeeMoney=@RoomKeyFeeMoney
      ,OtherFee=@OtherFee
      ,OtherFeeMoney=@OtherFeeMoney
      ,UpdateBy=@UpdateBy 
,UpdateDate=@UpdateDate
WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, info.EmployeeNo);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, info.Name);
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, info.CardNo);
                db.AddInParameter(dbCommandWrapper, "@ChargeContent", DbType.String, info.ChargeContent);
                db.AddInParameter(dbCommandWrapper, "@Money", DbType.Decimal, info.Money);
                db.AddInParameter(dbCommandWrapper, "@AirConditionFee", DbType.String, info.AirConditionFee);
                db.AddInParameter(dbCommandWrapper, "@AirConditionFeeMoney", DbType.Decimal, info.AirConditionFeeMoney);
                db.AddInParameter(dbCommandWrapper, "@RoomKeyFee", DbType.String, info.RoomKeyFee);
                db.AddInParameter(dbCommandWrapper, "@RoomKeyFeeMoney", DbType.Decimal, info.RoomKeyFeeMoney);
                db.AddInParameter(dbCommandWrapper, "@OtherFee", DbType.String, info.OtherFee);
                db.AddInParameter(dbCommandWrapper, "@OtherFeeMoney", DbType.Decimal, info.OtherFeeMoney);
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

        public DataTable GetTable(TB_Charging tb_Charging, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.[ID]
      ,A.[EmployeeNo]
      ,A.[Name]
      ,A.[CardNo]
      ,A.[Money]
      ,A.[AirConditionFeeMoney]
      ,A.[RoomKeyFeeMoney] 
      ,A.[OtherFeeMoney]
      ,ISNULL (Money,0)+ISNULL (AirConditionFeeMoney,0)+ISNULL (RoomKeyFeeMoney,0)+ISNULL (OtherFeeMoney,0) Allfee
      ,Convert(varchar(12),A.[CreateTime],111) as CreateTime
      ,A.[Creator]
      ,A.[UpdateDate]
      ,A.[UpdateBy]
      ,A.[SiteID]
      ,A.[BU] FROM [TB_Charging] AS A
where 1=1 ";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_Charging.SiteID);
                if (!string.IsNullOrEmpty(tb_Charging.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_Charging.EmployeeNo);
                }
                if (!string.IsNullOrEmpty(tb_Charging.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_Charging.Name);
                }
                if (!string.IsNullOrEmpty(tb_Charging.BU))
                {
                    var spara = string.Format("%{0}%", tb_Charging.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                if (tb_Charging.CreateTimeBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CreateTime as Date) between @CreateTimeBegin and @CreateTimeEnd");
                    db.AddInParameter(dbCommandWrapper, "@CreateTimeBegin", DbType.String, tb_Charging.CreateTimeBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@CreateTimeEnd", DbType.String, tb_Charging.CreateTimeEnd.ToString("yyyy/MM/dd"));
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

       
        public DataTable GetTable(TB_Charging tb_Charging)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.[EmployeeNo] as '工号'
      ,A.[Name] as '姓名'
      --,A.[CardNo] as '身份证号码'
      ,A.[BU] as '事业部'
      ,A.[Money]  as  '管理费'
,A.[AirConditionFeeMoney]  as  '空调费'
,A.[RoomKeyFeeMoney]  as  '钥匙费'
,A.[OtherFeeMoney]  as  '其他费'
,ISNULL (Money,0)+ISNULL (AirConditionFeeMoney,0)+ISNULL (RoomKeyFeeMoney,0)+ISNULL (OtherFeeMoney,0) 总金额
      ,Convert(varchar(12),A.[CreateTime],111) as '扣费日期'
FROM [TB_Charging] AS A
where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_Charging.SiteID);
                if (!string.IsNullOrEmpty(tb_Charging.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_Charging.EmployeeNo);
                }
                if (!string.IsNullOrEmpty(tb_Charging.Name))
                {
                    strBuilder.AppendLine(" AND A.Name = @Name");
                    db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, tb_Charging.Name);
                }
                if (!string.IsNullOrEmpty(tb_Charging.BU))
                {
                    var spara = string.Format("%{0}%", tb_Charging.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                //if (tb_Charging.CreateTime != default(DateTime))
                //{
                //    strBuilder.AppendLine(" AND CAST(CreateTime as Date)=@coDate");
                //    db.AddInParameter(dbCommandWrapper, "@coDate", DbType.String, tb_Charging.CreateTime.ToString("yyyy-MM-dd"));
                //}
                if (tb_Charging.CreateTimeBegin != default(DateTime))
                {
                    strBuilder.AppendLine(" AND CAST(CreateTime as Date) between @CreateTimeBegin and @CreateTimeEnd");
                    db.AddInParameter(dbCommandWrapper, "@CreateTimeBegin", DbType.String, tb_Charging.CreateTimeBegin.ToString("yyyy/MM/dd"));
                    db.AddInParameter(dbCommandWrapper, "@CreateTimeEnd", DbType.String, tb_Charging.CreateTimeEnd.ToString("yyyy/MM/dd"));
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

        public DataTable GetTableMonthByEmployeeNo(string EmployeeNo)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * from [TB_Charging] as A where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo And DATEDIFF(MM,A.CreateTime,GETDATE())=0");
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, EmployeeNo);
               
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
    }
}
