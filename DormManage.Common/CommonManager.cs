using System;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;

namespace DormManage.Common
{
    public class CommonManager
    {
        /// <summary>
        /// 域验证
        /// </summary>
        /// <param name="strADAccount"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public bool DomainAuthenticateLogin(string strADAccount, string strPassword)
        {
            string _filterAttribute = string.Empty;
            string _path = string.Empty;
            DirectoryEntry entry = new DirectoryEntry(string.Empty, strADAccount, strPassword);
            try
            {
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + strADAccount + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    _path = String.Empty;
                    _filterAttribute = String.Empty;
                }
                else
                {
                    _path = result.Path;
                    _filterAttribute = (String)result.Properties["cn"][0];
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 一次性将datatable数据导入数据表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="table">表名</param>
        /// <param name="colsString">表列名</param>
        /// <param name="connectString">数据库连接字符串</param>
        /// <returns></returns>
        public Int32 DTToDB(DataTable dt, string table, string[] colsString, string connectString)
        {
            Int32 ret = 0;
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(connectString);
                sqlBulkCopy.DestinationTableName = table;

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string sclname = dt.Columns[i].ColumnName;
                        // Set up the column mappings by name.
                        System.Data.SqlClient.SqlBulkCopyColumnMapping mapID = new System.Data.SqlClient.SqlBulkCopyColumnMapping(colsString[i], colsString[i]);
                        sqlBulkCopy.ColumnMappings.Add(mapID);
                    }
                    sqlBulkCopy.WriteToServer(dt);
                    ret = 1;
                }
                sqlBulkCopy.Close();
                stopwatch.Stop();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ret;
        }
    }
}
