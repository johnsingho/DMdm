using System;
using System.Data;
using System.Data.SqlClient;

namespace DormManage.Common
{
    public class DataTableHelper
    {
        public static string TryGet(DataRow dr, string colName)
        {
            var sRet = string.Empty;
            try
            {
                sRet = dr[colName].ToString().Trim();
            }
            catch (System.Exception ex)
            {
                //LogManager.GetInstance().ErrorLog("DataTableHelper::TryGet", ex);
            }
            
            return sRet;
        }

        public static void CopyDataRow(DataTable tarDt, DataRow srcDataRow)
        {
            var dr = tarDt.Rows.Add();
            foreach (DataColumn col in tarDt.Columns)
            {
                dr[col.ColumnName] = srcDataRow[col.ColumnName];
            }
        }

        public static bool IsEmptyDataTable(DataTable dt)
        {
            return (null == dt || 0 == dt.Rows.Count);
        }
        public static bool IsEmptyDataSet(DataSet ds)
        {
            if(null==ds || 0 == ds.Tables.Count)
            {
                return true;
            }
            return IsEmptyDataTable(ds.Tables[0]);
        }
        public static DataRow GetDataSet_Row0(DataSet ds)
        {
            var dt = ds.Tables[0];
            return dt.Rows[0];
        }

        public static string BulkToDB(string constring, DataTable dt, string tarTble)
        {
            var sErr = string.Empty;
            try
            {
                //声明SqlBulkCopy ,using释放非托管资源
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(constring))
                {
                    //一次批量的插入的数据量
                    sqlBC.BatchSize = 3000;
                    //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                    sqlBC.BulkCopyTimeout = 180;
                    //设置要批量写入的表
                    sqlBC.DestinationTableName = tarTble;
                    
                    //自定义的OleDbDataReader和数据库的字段进行对应
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sqlBC.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }
                    
                    //批量写入
                    sqlBC.WriteToServer(dt);
                    return sErr;
                }
            }
            catch (System.Exception ex)
            {
                sErr = ex.Message;
                return sErr;
            }
        }
        
    }
}
