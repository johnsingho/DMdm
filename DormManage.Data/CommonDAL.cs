using DormManage.Common.DBManager;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DormManage.Data.DAL
{
    public class CommonDAL
    {
        public bool ChangeBegStatus(int ID)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"select status from  TB_Bed   WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND id = @id");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);
              
                dbCommandWrapper.CommandText = strBuilder.ToString();
                DataSet ds= db.ExecuteDataSet(dbCommandWrapper);

                return ds.Tables[0].Rows[0][0].ToString() == "1" ? true : false;

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

        public int ChangeBegEnable( int ID,string value)
        {
           
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"update TB_Bed set IsEnable=@value  WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND id = @id");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);
                string uValue = value == "已禁用" ? "已启用" : "已禁用";
                db.AddInParameter(dbCommandWrapper, "@value", DbType.String, uValue);
                dbCommandWrapper.CommandText = strBuilder.ToString();
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
                    dbCommandWrapper = null;
                }
            }
        }

        //获取已被占用的床位
        public List<int> CheckBedCheckins(List<int> ids)
        {
            List<int> bedUsed = new List<int>();
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"select ID from TB_Bed WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;

                strBuilder.AppendLine(" AND status<>1");
                var sids = string.Join(",", ids);
                strBuilder.AppendFormat(" AND ID in ({0})", sids);
                strBuilder.AppendLine();

                dbCommandWrapper.CommandText = strBuilder.ToString();
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var bedID = int.Parse(dr["ID"].ToString());
                    bedUsed.Add(bedID);
                }
                return bedUsed;
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
        /// 批量启用/禁用
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="bEnable"></param>
        public int ChangeBedEnable(List<int> ids, bool bEnable)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"update TB_Bed set IsEnable=@value  WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;

                var sids = string.Join(",", ids);
                strBuilder.AppendFormat(" AND ID in ({0})", sids);
                strBuilder.AppendLine();
                string uValue = bEnable ? "已启用" : "已禁用";
                db.AddInParameter(dbCommandWrapper, "@value", DbType.String, uValue);
                dbCommandWrapper.CommandText = strBuilder.ToString();
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
                    dbCommandWrapper = null;
                }
            }
        }

        public bool ChangeRoomStatus(int ID)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"select status from  TB_Bed   WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND roomid = @id");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);

                dbCommandWrapper.CommandText = strBuilder.ToString();
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    if(dr[0].ToString()=="3")
                    {
                        return false;
                    }
                }

                return true;

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

        //获取已经被入住的房间
        public List<int> CheckRoomCheckins(List<int> ids)
        {
            DbCommand dbCommandWrapper = null;
            List<int> roomUseds = new List<int>();
            try
            {
                string strSQL = string.Format(@"select distinct RoomID from TB_Bed WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                var sids = string.Join(",", ids);
                strBuilder.AppendLine(" AND Status=3");
                strBuilder.AppendFormat(" AND roomid in ({0})", sids);

                dbCommandWrapper.CommandText = strBuilder.ToString();
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var roomid = int.Parse(dr["RoomID"].ToString());
                    roomUseds.Add(roomid);
                }

                return roomUseds;
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

        public int ChangeRoomEnable(int ID, string value)
        {

            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"update TB_Room set IsEnable=@value  WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND id = @ID");
                strBuilder.AppendLine(" update TB_Bed set IsEnable=@value  WHERE 1=1 ");
                strBuilder.AppendLine(" AND roomid = @ID");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);
                string uValue = value == "已禁用" ? "已启用" : "已禁用";
                db.AddInParameter(dbCommandWrapper, "@value", DbType.String, uValue);
                dbCommandWrapper.CommandText = strBuilder.ToString();
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
                    dbCommandWrapper = null;
                }
            }
        }

        /// <summary>
        /// 批量启用/禁用
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="bEnable"></param>
        public int ChangeRoomEnable(List<int> ids, bool bEnable)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"update TB_Room set IsEnable=@value  WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;

                var sids = string.Join(",", ids);
                strBuilder.AppendFormat(" AND id in ({0})", sids);
                strBuilder.AppendLine();
                strBuilder.AppendLine(" update TB_Bed set IsEnable=@value  WHERE 1=1 ");
                strBuilder.AppendFormat(" AND roomid in ({0})", sids);
                strBuilder.AppendLine();
                
                string uValue = bEnable ? "已启用" : "已禁用";
                db.AddInParameter(dbCommandWrapper, "@value", DbType.String, uValue);
                dbCommandWrapper.CommandText = strBuilder.ToString();

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
                    dbCommandWrapper = null;
                }
            }
        }

        public bool ChangeBuildingStatus(int ID)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"select status from  TB_Bed   WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND buildingID = @ID");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);

                dbCommandWrapper.CommandText = strBuilder.ToString();
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr[0].ToString() == "3")
                    {
                        return false;
                    }
                }

                return true;

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

        public int ChangeBuildingEnable(int ID, string value)
        {

            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"update TB_Building set IsEnable=@value  WHERE  id = @ID ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" update TB_Room set IsEnable=@value  WHERE  buildingID = @ID ");
                strBuilder.AppendLine(" update TB_Bed set IsEnable=@value  WHERE  buildingID = @ID ");
               
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);
                string uValue = value == "已禁用" ? "已启用" : "已禁用";
                db.AddInParameter(dbCommandWrapper, "@value", DbType.String, uValue);
                dbCommandWrapper.CommandText = strBuilder.ToString();
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
                    dbCommandWrapper = null;
                }
            }
        }

        public bool ChangeDormAreaStatus(int ID)
        {
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"select status from  TB_Bed   WHERE 1=1 ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" AND DormAreaID = @ID");
                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);

                dbCommandWrapper.CommandText = strBuilder.ToString();
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr[0].ToString() == "3")
                    {
                        return false;
                    }
                }

                return true;

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

        public int ChangeDormAreaEnable(int ID, string value)
        {

            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = string.Format(@"update TB_DormArea set IsEnable=@value  WHERE  id = @ID ");
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstance();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                strBuilder.AppendLine(" update TB_Building set IsEnable=@value  WHERE  DormAreaID = @ID ");
                strBuilder.AppendLine(" update TB_Room set IsEnable=@value  WHERE  DormAreaID = @ID ");
                strBuilder.AppendLine(" update TB_Bed set IsEnable=@value  WHERE  DormAreaID = @ID ");

                db.AddInParameter(dbCommandWrapper, "@ID", DbType.Int32, ID);
                string uValue = value == "已禁用" ? "已启用" : "已禁用";
                db.AddInParameter(dbCommandWrapper, "@value", DbType.String, uValue);
                dbCommandWrapper.CommandText = strBuilder.ToString();
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
                    dbCommandWrapper = null;
                }
            }
        }

        public static DataTable GetEmployeeInfoByIcCard(string ICCardNo)
        {
            var db = DBO.CreateDatabaseKQXT();
            using (var conn = db.CreateConnection())
            {
                conn.Open();
                string strUpdateSql = @"select OutID,Name,IDCardNo from Customer where SCardSNR=@SCardSNR";
                try
                {
                    var dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);
                    db.AddInParameter(dbCommandWrapper, "@SCardSNR", DbType.String, ICCardNo);
                    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public static DataTable GetEmployeeInfoByWorkID(string sWorkID)
        {
            var db = DBO.CreateDatabaseKQXT();
            using (var conn = db.CreateConnection())
            {
                conn.Open();
                string strUpdateSql = @"select OutID,Name,IDCardNo from Customer where OutID=@sOutID";
                try
                {
                    var dbCommandWrapper = db.GetSqlStringCommand(strUpdateSql);
                    db.AddInParameter(dbCommandWrapper, "@sOutID", DbType.String, sWorkID);
                    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
