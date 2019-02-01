using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace DromManage.Job
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //1
                DataTable dt = GetData();
                BulkToDB(ConfigHelper.GetAppSettings("SqlServer_RM_DB"), dt);
                ClearAssignDormArea(ConfigHelper.GetAppSettings("SqlServer_RM_DB"));

                //2检测超过三天未处理的报修，建议，发送邮件
                //只在凌晨的时候进行检查
                var nHour = DateTime.Now.Hour;
                if (nHour>22 || nHour<=1)
                {
                    SendNoticeMail();
                }                
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


        private static void SendNoticeMail()
        {
            if (!ConfigHelper.SendEmail) { return; }
            var dtDormRepair = GetExpiredDormRepair();
            var dtDormSuggest = GetExpiredDormSuggest();
            if((null==dtDormRepair || 0 == dtDormRepair.Rows.Count)
                && (null==dtDormSuggest || 0== dtDormSuggest.Rows.Count)
                )
            { return;}
            var sMailRecveiver = string.Empty;
            var sMailCC = string.Empty;
            LoadMailReceiver(out sMailRecveiver, out sMailCC);
            if (sMailRecveiver.Length<2 && sMailCC.Length<2) { return; }

            var sFileTeml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "email.template");
            var mailSender = new MailSender(ConfigHelper.SMTPConnection, sMailRecveiver, sMailCC);
            var sContent = string.Format("宿舍管理系统后台的以下消息已超过{0}天未处理，请及时处理，谢谢！", ConfigHelper.ExpiredDay);
            var sDetail = new StringBuilder();
            MakeTableHtml(sDetail, dtDormRepair, "宿舍报修");
            MakeTableHtml(sDetail, dtDormSuggest, "宿舍建议");
            mailSender.SendMail(sFileTeml, ConfigHelper.EmailSubject, sContent, sDetail.ToString());
        }

        private static DataTable GetExpiredDormRepair()
        {
            using (var con = new SqlConnection(ConfigHelper.GetAppSettings("SqlServer_RM_DB")))
            {
                con.Open();
                var sql = string.Format(@"
                                select top 100 
                                CName as '中文名', EmployeeNo as '工号', MobileNo as '手机号', 
                                DormAddress as '宿舍地址', RepairTime as '预约时间', RequireDesc as '描述',
                                CreateDate as '提交时间'
                                from TB_DormRepair
                                where 1=1
                                and DATEDIFF(day, CreateDate, GetDate())>{0}
                                and Status=0
                                ", ConfigHelper.ExpiredDay);
                DataSet DS = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(DS);
                if(null!=DS && DS.Tables.Count > 0)
                {
                    return DS.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }

        private static DataTable GetExpiredDormSuggest()
        {
            using (var con = new SqlConnection(ConfigHelper.GetAppSettings("SqlServer_RM_DB")))
            {
                con.Open();
                var sql = string.Format(@"
                                select top 100 
                                CName as '中文名', EmployeeNo as '工号', MobileNo as '手机号', Suggest as '建议',
                                CreateDate as '提交时间'
                                from TB_DormSuggest
                                where 1=1
                                and DATEDIFF(day, CreateDate, GetDate())>{0}
                                and Status=0
                                ", ConfigHelper.ExpiredDay);
                DataSet DS = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(DS);
                if (null != DS && DS.Tables.Count > 0)
                {
                    return DS.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }


        private static void LoadMailReceiver(out string recvs, out string ccs)
        {
            recvs = string.Empty;
            ccs = string.Empty;
            using (SqlConnection conn = new SqlConnection(ConfigHelper.GetAppSettings("SqlServer_RM_DB")))
            {
                conn.Open();

                var sql = new StringBuilder();
                sql.Append(@"
                        select MailAddress,MailType 
                        from TB_DormMailReceiver
                        where DeleteMark=1
                        ");
                var cmm = conn.CreateCommand();
                cmm.CommandText = sql.ToString();
                cmm.CommandType = CommandType.Text;
                var dt = new DataTable();
                using (var dr = cmm.ExecuteReader())
                {
                    dt.Load(dr);
                }

                var lstRecv = new List<string>();
                var lstCc = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    int nType = (int)row["MailType"];
                    if (1 == nType)
                    {
                        lstRecv.Add(row["MailAddress"] as string);
                    }
                    else
                    {
                        lstCc.Add(row["MailAddress"] as string);
                    }
                }
                if (lstRecv.Count == 0)
                {
                    recvs = string.Join(";", lstCc.ToArray());
                }
                else
                {
                    recvs = string.Join(";", lstRecv.ToArray());
                    ccs = string.Join(";", lstCc.ToArray());
                }
            }
        }

        private static void MakeTableHtml(StringBuilder sb, DataTable dt, string sTabTitle)
        {
            if (dt.Rows.Count == 0) { return; }

            sb.Append("<hr><br>");
            sb.Append(@"<table border='0' cellspacing='0' cellpadding='0' class='myTab'>");
            sb.AppendFormat(@"<caption>{0}</caption>", sTabTitle);
            sb.Append(@"<thead><tr>");
            foreach (DataColumn col in dt.Columns)
            {
                sb.AppendFormat(@"<th><p>{0}</p></th>", col.ColumnName);
            }
            sb.Append(@"</thead></tr>");
            sb.Append(@"<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append(@"<tr>");
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    if ("提交时间".Equals(dt.Columns[j].ColumnName) || "预约时间".Equals(dt.Columns[j].ColumnName))
                    {
                        var dat = (DateTime)row[j];
                        sb.AppendFormat(@"<td>{0}</td>", dat.ToString("yyyy-MM-dd HH:mm"));
                    }
                    else
                    {
                        sb.AppendFormat(@"<td>{0}</td>", row[j]);
                    }

                }
                sb.Append(@"</tr>");
            }
            sb.Append(@"</tbody>");
            sb.Append(@"</table>");
        }

    }
}
