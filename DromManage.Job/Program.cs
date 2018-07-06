using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace DromManage.Job
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                DataTable dt = GetData();

                BulkToDB(ConfigHelper.GetAppSettings("SqlServer_RM_DB"), dt);

                ClearAssignDormArea(ConfigHelper.GetAppSettings("SqlServer_RM_DB"));
            }
            catch (System.Exception ex)
            {

            }
        }

        static DataTable GetData()
        {

            SqlConnection con = new SqlConnection(ConfigHelper.GetAppSettings("SqlServer_RM_DB"));//连接数据库
            con.Open();
            StringBuilder strSqlUserScore = new StringBuilder();
            strSqlUserScore.Append(" select * From view_DayDormReport");
            DataSet DS = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(strSqlUserScore.ToString(), con);
            da.Fill(DS);
            con.Close();
            return DS.Tables[0];
        }

        static bool BulkToDB(string constring, DataTable dt)
        {
            try
            {

                //声明SqlBulkCopy ,using释放非托管资源
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(constring))
                {
                    //一次批量的插入的数据量
                    sqlBC.BatchSize = 3000;

                    //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                    sqlBC.BulkCopyTimeout = 60;

                    //设置要批量写入的表
                    sqlBC.DestinationTableName = "TB_DayDormReport";


                    //将dt的列名改为数据库字段名
                    //for (int i = 0; i < dt.Columns.Count; i++)
                    //{
                    //    dt.Columns[i].ColumnName = dt.Rows[0][i].ToString();
                    //}
                    dt.Columns[0].ColumnName = "Areaname";
                    dt.Columns[1].ColumnName = "Roomtypt";
                    dt.Columns[2].ColumnName = "AllBegCount";
                    dt.Columns[3].ColumnName = "AllCheckIn";
                    dt.Columns[4].ColumnName = "NewCheckIn";
                    dt.Columns[5].ColumnName = "CheckOut";
                    dt.Columns[6].ColumnName = "FreeBegCount";
                    dt.Columns[7].ColumnName = "CheckInRate";
                    dt.Columns[8].ColumnName = "CreateDate";
                    

                    //自定义的OleDbDataReader和数据库的字段进行对应
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sqlBC.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }

                    //删除数据库字段行
                    //dt.Rows.RemoveAt(0);

                    //Clear day old data
                    if(dt.Rows.Count > 0)
                    {
                        DateTime? dat = (DateTime)dt.Rows[0]["CreateDate"];
                        if (null != dat) { ClearDayOldData(sqlBC.DestinationTableName, dat.Value); }
                    }

                    //批量写入
                    sqlBC.WriteToServer(dt);

                    return true;
                }


            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }

        //2017-12-07 通过job配置，现在一天可能刷新几次统计数据，因此先删除当天旧的
        private static bool ClearDayOldData(string sTab, DateTime dat)
        {
            using (SqlConnection con = new SqlConnection(ConfigHelper.GetAppSettings("SqlServer_RM_DB")))
            {
                con.Open();
                StringBuilder strSqlUserScore = new StringBuilder();
                strSqlUserScore.AppendFormat("delete from {0} where CreateDate='{1}'", sTab, dat.ToString("yyyy-MM-dd"));
                var cmd = con.CreateCommand();
                cmd.CommandText = strSqlUserScore.ToString();
                return cmd.ExecuteNonQuery()>0;
            }
        }

        static void ClearAssignDormArea(string constring)
        {
            SqlConnection sqlconnection = new SqlConnection(constring);


            SqlCommand sqlcommand = new SqlCommand();
            sqlconnection.Open();
            sqlcommand.Connection = sqlconnection;
            sqlcommand.CommandText = " delete from TB_AssignDormArea where DATEDIFF(dd,CreateDate,GETDATE())>=5 ";
            sqlcommand.ExecuteNonQuery();
            sqlcommand = null;
            sqlconnection.Close();
            sqlconnection = null;
        }
    }
}
