using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormManage.Framework;
using DormManage.Models;
using System.Data;
using System.Data.Common;
using DormManage.Framework.Enum;
using DormManage.Framework.LogManager;

namespace DormManage.Data.DAL
{
    public class FlexPlusBAL
    {
        public DataTable GetApplyDorms(TB_DormAreaApply mVal, Pager pager)
        {
            var sb = new StringBuilder("select * from V_TB_DormAreaApply ");
            var db = DBO.GetInstance();
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            dbCommandWrapper = db.DbProviderFactory.CreateCommand();
            dbCommandWrapper.CommandType = CommandType.Text;
            sb.Append("where 1=1 ");

            if (mVal.ID > 0)
            {
                sb.AppendFormat("and id={0} ", mVal.ID);
            }
            if (!string.IsNullOrEmpty(mVal.CName))
            {
                sb.AppendFormat("and CName='{0}' ", mVal.CName);
            }
            if (!string.IsNullOrEmpty(mVal.CardNo))
            {
                sb.AppendFormat("and CardNo='{0}' ", mVal.CardNo);
            }
            if (!string.IsNullOrEmpty(mVal.EmployeeNo))
            {
                sb.AppendFormat("and EmployeeNo='{0}' ", mVal.EmployeeNo);
            }
            if (!string.IsNullOrEmpty(mVal.MobileNo))
            {
                sb.AppendFormat("and MobileNo='{0}' ", mVal.MobileNo);
            }
            if (mVal.RequireType > 0)
            {
                sb.AppendFormat("and RequireType={0} ", mVal.RequireType);
            }
            if (mVal.Status > -1)
            {
                sb.AppendFormat("and Status={0} ", mVal.Status);
            }

            if (pager != null && !pager.IsNull)
            {
                dbCommandWrapper.CommandText = pager.GetPagerSql4Count(sb.ToString());
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                pager.TotalRecord = Convert.ToInt32(dt.Rows[0][0]);
                dbCommandWrapper.CommandText = pager.GetPagerSql4Data(sb.ToString(), DataBaseTypeEnum.sqlserver);
            }
            else
            {
                dbCommandWrapper.CommandText = sb.ToString();
            }
            dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            return dt;
        }

        public DataTable GetApplyDormByID(string id)
        {
            var sb = new StringBuilder("select * from TB_DormAreaApply ");
            sb.AppendFormat(" where id={0}", id);

            var db = DBO.GetInstance();
            DbCommand dbCommandWrapper = null;
            dbCommandWrapper = db.DbProviderFactory.CreateCommand();
            dbCommandWrapper.CommandType = CommandType.Text;
            return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
        }

        public void HandleApplyDorm(string id, string sHandle, string sMsg)
        {
            //TODO TB_DormAreaApply
            var db = DBO.GetInstance();
            DbCommand dbCommandWrapper = null;
            string sSql = @"update [TB_DormAreaApply] 
                            SET Status=@Handle, Response=@Msg, UpdateDate=GetDate() 
                            where id=@id
                            ";

            dbCommandWrapper = db.GetSqlStringCommand(sSql);
            #region Add parameters
            db.AddInParameter(dbCommandWrapper, "@Handle", DbType.String, sHandle);
            db.AddInParameter(dbCommandWrapper, "@Msg", DbType.String, sMsg);
            db.AddInParameter(dbCommandWrapper, "@id", DbType.String, id);
            #endregion
            db.ExecuteNonQuery(dbCommandWrapper);
        }
    }
}
