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
using DormManage.Model;
using DormManage.Model.Allowance;

namespace DormManage.Data.DAL
{
    public class TB_AllowanceApplyCancelDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Create(TB_AllowanceApplyCancel info)
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
        public int Create(TB_AllowanceApplyCancel info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            int intId;
            string strInsertSql = @"INSERT INTO TB_AllowanceApplyCancel (EmployeeNo,Name,CardNo,Sex,Company,BU,Grade,AlloWanceApplyID,EmployeeTypeName,BZ,CreateDate,CreateUser,SiteID,Hire_Date,Effective_Date) 
                                     VALUES(@EmployeeNo,@Name,@CardNo,@Sex,@Company,@BU,@Grade,@AlloWanceApplyID,@EmployeeTypeName,@BZ,@CreateDate,@CreateUser,@SiteID,@Hire_Date,@Effective_Date)";
            string strSelectIdSql = ";SELECT SCOPE_IDENTITY()";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strInsertSql + strSelectIdSql);
                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, SetNullValue(string.IsNullOrEmpty(info.EmployeeNo), info.EmployeeNo));
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Name), info.Name));
                db.AddInParameter(dbCommandWrapper, "@CardNo", DbType.String, SetNullValue(string.IsNullOrEmpty(info.CardNo), info.CardNo));
                db.AddInParameter(dbCommandWrapper, "@Sex", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Sex), info.Sex));
                db.AddInParameter(dbCommandWrapper, "@Company", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Company), info.Company));
                db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, SetNullValue(string.IsNullOrEmpty(info.BU), info.BU));
                db.AddInParameter(dbCommandWrapper, "@Grade", DbType.Int32,  info.Grade);
                db.AddInParameter(dbCommandWrapper, "@AlloWanceApplyID", DbType.Int32, info.AlloWanceApplyID);
                db.AddInParameter(dbCommandWrapper, "@EmployeeTypeName", DbType.String, SetNullValue(string.IsNullOrEmpty(info.EmployeeTypeName), info.EmployeeTypeName));
                db.AddInParameter(dbCommandWrapper, "@BZ", DbType.String, SetNullValue(string.IsNullOrEmpty(info.BZ), info.BZ));
                db.AddInParameter(dbCommandWrapper, "@CreateDate", DbType.DateTime, info.CreateDate);
                db.AddInParameter(dbCommandWrapper, "@CreateUser", DbType.String, SetNullValue(string.IsNullOrEmpty(info.CreateUser), info.CreateUser));
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, info.SiteID);
                db.AddInParameter(dbCommandWrapper, "@Hire_Date", DbType.DateTime, info.Hire_Date);
                db.AddInParameter(dbCommandWrapper, "@Effective_Date", DbType.DateTime, info.Effective_Date);

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
        public int Edit(TB_AllowanceApplyCancel info)
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
        public int Edit(TB_AllowanceApplyCancel info, DbTransaction tran, Database db)
        {
            DbCommand dbCommandWrapper = null;
            string strUpdateSql = @"UPDATE TB_AllowanceApplyCancel SET Name=@Name,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate WHERE ID=@ID";
            try
            {
                dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);

                #region Add parameters
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, info.ID);
                db.AddInParameter(dbCommandWrapper, "@Name", DbType.String, SetNullValue(string.IsNullOrEmpty(info.Name), info.Name));
                db.AddInParameter(dbCommandWrapper, "@UpdateBy", DbType.String, SetNullValue(string.IsNullOrEmpty(info.CreateUser), info.CreateUser));
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
        public TB_AllowanceApplyCancel Get(int intID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            TB_AllowanceApplyCancel mTB_AllowanceApplyCancel = null;
            try
            {
                string strSQL = @"select * from TB_AllowanceApplyCancel where 1=1";
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
                    mTB_AllowanceApplyCancel = new TB_AllowanceApplyCancel()
                    {
                        Name = Convert.ToString(dt.Rows[0]["Name"]),
                    };
                }
                return mTB_AllowanceApplyCancel;
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
        /// 获取所有的用工类型
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllEmployeeTypes()
        {
            DataTable dt = null;
            string strSQL = @"select distinct EmployeeTypeName from TB_AllowanceApplyCancel order by EmployeeTypeName";
            StringBuilder strBuilder = new StringBuilder(strSQL);
            Database db = DBO.GetInstance();
            using (var dbCommandWrapper = db.DbProviderFactory.CreateCommand())
            {
                dbCommandWrapper.CommandType = CommandType.Text;
                dbCommandWrapper.CommandText = strSQL;
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                return dt;
            }
        }

        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <param name="tb_allowanceApplyCancel"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_AllowanceApplyCancel tb_allowanceApplyCancel, ref Pager pager)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select a.*,CONVERT(varchar(100), A.Hire_Date, 23) HireDate,CONVERT(varchar(100), A.CreateDate, 23) AllowanceCancelDate,CONVERT(varchar(100), B.CreateDate, 23) AllowanceApplyDate,CONVERT(varchar(100), A.Effective_Date, 23) EffectiveDate from TB_AllowanceApplyCancel AS A left join TB_AllowanceApply as B on A.AlloWanceApplyID=B.ID ";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                #region 拼接条件

                strBuilder.AppendLine(" where 1=1");
                if (tb_allowanceApplyCancel.SiteID > 0)
                {
                    strBuilder.AppendLine(" AND A.SiteID = @SiteID");
                    db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_allowanceApplyCancel.SiteID);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND A.EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_allowanceApplyCancel.EmployeeNo);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.CardNo))
                {
                    strBuilder.AppendLine(" AND A.CardNo = @cardNo");
                    db.AddInParameter(dbCommandWrapper, "@cardNo", DbType.String, tb_allowanceApplyCancel.CardNo);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.BU))
                {
                    var spara = string.Format("%{0}%", tb_allowanceApplyCancel.BU);
                    strBuilder.AppendLine(" AND A.BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                if (tb_allowanceApplyCancel.Hire_Date != null)
                {
                    strBuilder.AppendLine(" AND CAST(A.Hire_Date as Date)=@hireDate");
                    var sday = tb_allowanceApplyCancel.Hire_Date.Value.ToString("yyyy-MM-dd");
                    db.AddInParameter(dbCommandWrapper, "@hireDate", DbType.String, sday);
                }
                if (tb_allowanceApplyCancel.Effective_Date != null)
                {
                    strBuilder.AppendLine(" AND CAST(A.Effective_Date as Date)=@effDate");
                    var sday = tb_allowanceApplyCancel.Effective_Date.Value.ToString("yyyy-MM-dd");
                    db.AddInParameter(dbCommandWrapper, "@effDate", DbType.String, sday);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.EmployeeTypeName))
                {
                    strBuilder.AppendLine(" AND A.EmployeeTypeName = @empType");
                    db.AddInParameter(dbCommandWrapper, "@empType", DbType.String, tb_allowanceApplyCancel.EmployeeTypeName);
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
        /// <param name="intUserID"></param>
        /// <returns></returns>
        public DataTable GetTable(string EmployeeNo)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.* 
FROM TB_AllowanceApplyCancel AS A

                                        WHERE A.[EmployeeNo]=@EmployeeNo";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intUserID"></param>
        /// <returns></returns>
        public DataTable GetTable(int intUserID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.* 
FROM TB_AllowanceApplyCancel AS A
INNER JOIN [TB_UserConnectDormArea] AS B
ON A.ID=B.[DormAreaID]
                                        WHERE B.[UserID]=@UserID";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, intUserID);
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
        /// <param name="tb_allowanceApplyCancel"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_AllowanceApplyCancel tb_allowanceApplyCancel)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select Company 公司
,BU 事业部
,Name 姓名
,Sex 性别
,EmployeeNo 工号
,CONVERT(varchar(100), CreateDate, 23)  申请时间
,Grade 级别
,CONVERT(varchar(100), Hire_Date, 23) 入职日期
,EmployeeTypeName 用工类型 
,CONVERT(varchar(100), Effective_Date, 23) 生效日期
,BZ 备注 
 from TB_AllowanceApplyCancel where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_allowanceApplyCancel.SiteID);
                if (tb_allowanceApplyCancel.ID > 0)
                {
                    strBuilder.AppendLine(" AND ID <> @ID");
                    db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, tb_allowanceApplyCancel.ID);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.EmployeeNo))
                {
                    strBuilder.AppendLine(" AND EmployeeNo = @EmployeeNo");
                    db.AddInParameter(dbCommandWrapper, "@EmployeeNo", DbType.String, tb_allowanceApplyCancel.EmployeeNo);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.CardNo))
                {
                    strBuilder.AppendLine(" AND CardNo = @cardNo");
                    db.AddInParameter(dbCommandWrapper, "@cardNo", DbType.String, tb_allowanceApplyCancel.CardNo);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.BU))
                {
                    var spara = string.Format("%{0}%", tb_allowanceApplyCancel.BU);
                    strBuilder.AppendLine(" AND BU like @BU");
                    db.AddInParameter(dbCommandWrapper, "@BU", DbType.String, spara);
                }
                if (tb_allowanceApplyCancel.Hire_Date != null)
                {
                    strBuilder.AppendLine(" AND CAST(Hire_Date as Date)=@hireDate");
                    var sday = tb_allowanceApplyCancel.Hire_Date.Value.ToString("yyyy-MM-dd");
                    db.AddInParameter(dbCommandWrapper, "@hireDate", DbType.String, sday);
                }
                if (tb_allowanceApplyCancel.Effective_Date != null)
                {
                    strBuilder.AppendLine(" AND CAST(Effective_Date as Date)=@effDate");
                    var sday = tb_allowanceApplyCancel.Effective_Date.Value.ToString("yyyy-MM-dd");
                    db.AddInParameter(dbCommandWrapper, "@effDate", DbType.String, sday);
                }
                if (!string.IsNullOrEmpty(tb_allowanceApplyCancel.EmployeeTypeName))
                {
                    strBuilder.AppendLine(" AND EmployeeTypeName = @empType");
                    db.AddInParameter(dbCommandWrapper, "@empType", DbType.String, tb_allowanceApplyCancel.EmployeeTypeName);
                }
                //strBuilder.AppendLine(" AND NAME = @NAME");
                //db.AddInParameter(dbCommandWrapper, "@NAME", DbType.String, TB_AllowanceApplyCancel.Name);
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
        /// 获取到一个Site的所有宿舍区
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetTableBySite(int siteID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select ID
                                ,[SiteID]
                                ,[Name]
                                ,[Creator] from TB_AllowanceApplyCancel where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
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

        private object SetNullValue(bool isNullValue, object value)
        {
            if (isNullValue)
                return DBNull.Value;
            else
                return value;
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
            string strSql = @"DELETE FROM TB_AllowanceApplyCancel WHERE ID in (" + strID + ")";
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
