using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DormManage.Data.DAL
{
    public class TB_ModuleDAL
    {

        public DataTable GetUserModule(int intUserID)
        {
                        DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.* FROM [TB_Module] AS A
INNER join [TB_RoleConnectModule] as B
on A.ID=B.[ModuleID]
inner join TB_User AS C
on B.RoleID=C.RoleID
where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND C.ID = @UserID");
                db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, intUserID);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
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

        //TODO 现在FlexPlus相关页面不考虑权限问题
        public DataTable GetUserModule_FlexPlus(int intUserID)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.* FROM [TB_Module_FlexPlus] AS A
                                where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND C.ID = @UserID");
                //db.AddInParameter(dbCommandWrapper, "@UserID", DbType.Int32, intUserID);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
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

        public DataSet GetModule(int intRoleID)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT A.* FROM [TB_Module] AS A
INNER join [TB_RoleConnectModule] as B
on A.ID=B.[ModuleID]
where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND B.RoleID = @RoleID");
                db.AddInParameter(dbCommandWrapper, "@RoleID", DbType.Int32, intRoleID);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                return db.ExecuteDataSet(dbCommandWrapper);
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

        public DataSet GetTable(TB_Module tb_Module)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * FROM [TB_Module] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_Module.SiteID);
                strBuilder.AppendLine(" AND [IsActive] = @IsActive");
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, tb_Module.IsActive);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                return db.ExecuteDataSet(dbCommandWrapper);
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

        public DataSet GetTable_FlexPlus(TB_Module tb_Module)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"SELECT * FROM [TB_Module_FlexPlus] where 1=1";
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND SiteID = @SiteID");
                db.AddInParameter(dbCommandWrapper, "@SiteID", DbType.Int32, tb_Module.SiteID);
                strBuilder.AppendLine(" AND [IsActive] = @IsActive");
                db.AddInParameter(dbCommandWrapper, "@IsActive", DbType.Int32, tb_Module.IsActive);
                dbCommandWrapper.CommandText = strBuilder.ToString();
                return db.ExecuteDataSet(dbCommandWrapper);
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
