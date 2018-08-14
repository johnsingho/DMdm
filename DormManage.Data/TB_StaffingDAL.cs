using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using DormManage.Common;
using DormManage.Framework;
using DormManage.Framework.Enum;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace DormManage.Data.DAL
{
    public class TEmpInfo
    {
        public string Employee_ID { get; set; }
        public string Chinese_Name { get; set; }
        public string English_Name { get; set; }
        public string IDCardNumber { get; set; }
        public string Segment { get; set; }
        public DateTime Hire_Date { get; set; }
        public string EmployeeTypeName { get; set; }
    }

    public class TB_StaffingDAL
    {

        /// <summary>
        /// 根据ID获取到超级管理员信息
        /// </summary>
        /// <param name="intAdminID"></param>
        /// <returns></returns>
        public DataTable GetTable(string seCardID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                //string strSQL = @"select ProcessTab.IDCardNumber,ProcessTab.EmployeeID
                //            ,ProcessTab.Phone,ProcessTab.SegmentName,IDCardTab.Sex,IDCardTab.ChineseName
                //             from [dbo].[Process]  as  ProcessTab
                //            left join 
                //            [dbo].[IDCard] as IDCardTab
                //            on ProcessTab.IDCardNumber = IDCardTab.IDCardNumber
                //            where ProcessTab.processstatus='6000' and ProcessTab.IDCardNumber='{0}'
                //            order by ProcessTab.ID desc";


                //string strSQL = @"select EmployeeTab.IDCard as IDCardNumber,EmployeeTab.EmployeeID,EmployeeTab.Mobile as Phone,SegmentTab.Name as SegmentName,SegmentTab.ID as SegmentID
                //                    ,EmployeeTab.Sex,EmployeeTab.ChineseName
                //                    from [dbo].[Employee2] as EmployeeTab
                //                    left join [dbo].[Segment] as SegmentTab
                //                    on EmployeeTab.SegmentID = SegmentTab.ID where EmployeeTab.IDCard='{0}'
                //                    order by EmployeeTab.ID desc";

                string strSQL = @"select top 1  EmployeeTab.IDCardNumber ,EmployeeTab.EmployeeID,EmployeeTab.Phone,SegmentTab.SegmentName+' '+Building.BuildingName SegmentName,SegmentTab.ID as SegmentID
                                    ,A.Sex,A.ChineseName,B.EmployeeTypeName,EmployeeTab.DormAndOneCarAccessOperateDatetime Hire_Date
                                    from Process as EmployeeTab
                                    LEFT JOIN IDCard as A ON A.IDCardNumber=EmployeeTab.IDCardNumber
                                    LEFT JOIN EmployeeType as B ON B.ID=EmployeeTab.EmployeeTypeID
                                    left join [dbo].[Segment] as SegmentTab
                                    on EmployeeTab.SegmentID = SegmentTab.ID 
                                    left join [dbo].[Building] as Building on Building.ID=EmployeeTab.BuildingID
                                    where EmployeeTab.processstatus>=5000 and EmployeeTab.IDCardNumber='{0}'  ORDER BY EmployeeTab.ID DESC ";

                strSQL = string.Format(strSQL, seCardID);
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstanceStaffing();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                //strBuilder.AppendLine(" AND IDCardNumber = @ID");
                //db.AddInParameter(dbCommandWrapper, "@ID", DbType.String, seCardID);
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

        //并入IDL人员信息，但部分还没有身份证
        public DataTable GetTableWithIDL(string sWorkdayNo, string seCardID)
        {
            DataTable dt = null;
            DbCommand dbCommandWrapper = null;
            try
            {
                string strSQL = @"select top 1  EmployeeTab.IDCardNumber ,EmployeeTab.EmployeeID,EmployeeTab.Phone,SegmentTab.SegmentName+' '+Building.BuildingName SegmentName,SegmentTab.ID as SegmentID
                                    ,A.Sex,A.ChineseName,B.EmployeeTypeName,EmployeeTab.DormAndOneCarAccessOperateDatetime Hire_Date
                                    from Process as EmployeeTab
                                    LEFT JOIN IDCard as A ON A.IDCardNumber=EmployeeTab.IDCardNumber
                                    LEFT JOIN EmployeeType as B ON B.ID=EmployeeTab.EmployeeTypeID
                                    left join [dbo].[Segment] as SegmentTab
                                    on EmployeeTab.SegmentID = SegmentTab.ID 
                                    left join [dbo].[Building] as Building on Building.ID=EmployeeTab.BuildingID
                                    where EmployeeTab.processstatus>=5000 
                                    and (EmployeeTab.IDCardNumber='{0}' or EmployeeTab.EmployeeID='{1}')
                                    ORDER BY EmployeeTab.ID DESC ";

                strSQL = string.Format(strSQL, seCardID, sWorkdayNo);
                StringBuilder strBuilder = new StringBuilder(strSQL);
                Database db = DBO.GetInstanceStaffing();
                dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                dbCommandWrapper.CommandType = CommandType.Text;
                dbCommandWrapper.CommandText = strBuilder.ToString();
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];

                if (!DataTableHelper.IsEmptyDataTable(dt))
                {
                    return dt;
                }

                strSQL = @"
                        select top 1 
                                IDCardNumber, Employee_ID as EmployeeID, null as Phone, Segment as SegmentName, 
		                        0 as SegmentID, 
                                CASE when [IDCardNumber] IS NULL THEN NULL ELSE 
                                    CASE when (cast(SUBSTRING([IDCardNumber], 17,1) as int) % 2)>0 then '男' else '女' END
                                END as Sex, 
                                Chinese_Name as ChineseName,
		                        EmployeeTypeName, Hire_Date
                        from TB_LongEmployee t
                        where 1=1
                        and (IDCardNumber='{0}' or Employee_ID='{1}')
                        ORDER BY Employee_ID DESC 
                           ";
                strBuilder.Clear();
                strBuilder.AppendFormat(strSQL, seCardID, sWorkdayNo);
                Database dbDorm = DBO.GetInstance();
                var dbComm = dbDorm.DbProviderFactory.CreateCommand();
                dbComm.CommandType = CommandType.Text;
                dbComm.CommandText = strBuilder.ToString();
                dt = dbDorm.ExecuteDataSet(dbComm).Tables[0];
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


        public bool UploadEmpInfo(TEmpInfo empInfo, out string sErr)
        {
            sErr = string.Empty;
            if (null == empInfo
                || string.IsNullOrEmpty(empInfo.Chinese_Name)
                || string.IsNullOrEmpty(empInfo.Employee_ID)
                || string.IsNullOrEmpty(empInfo.IDCardNumber)
                )
            {
                sErr = "工号、中文名、身份证证号不能为空!";
                return false;
            }

            try
            {
                var db = DBO.GetInstance();
                DbCommand dbCommandWrapper = null;
                string sSql = @"INSERT INTO [TB_LongEmployee] ([Employee_ID],[English_Name],[Chinese_Name],[Segment],[Hire_Date],[EmployeeTypeName],[IDCardNumber])      
                            VALUES  (@Employee_ID,@English_Name,@Chinese_Name,@Segment,@Hire_Date,@EmployeeTypeName,@IDCardNumber)
                            ";

                dbCommandWrapper = db.GetSqlStringCommand(sSql);
                db.AddInParameter(dbCommandWrapper, "@Employee_ID", DbType.String, empInfo.Employee_ID);
                db.AddInParameter(dbCommandWrapper, "@English_Name", DbType.String, empInfo.English_Name);
                db.AddInParameter(dbCommandWrapper, "@Chinese_Name", DbType.String, empInfo.Chinese_Name);
                db.AddInParameter(dbCommandWrapper, "@Segment", DbType.String, empInfo.Segment);
                db.AddInParameter(dbCommandWrapper, "@Hire_Date", DbType.Date, empInfo.Hire_Date);
                db.AddInParameter(dbCommandWrapper, "@EmployeeTypeName", DbType.String, empInfo.EmployeeTypeName);
                db.AddInParameter(dbCommandWrapper, "@IDCardNumber", DbType.String, empInfo.IDCardNumber);
                return db.ExecuteNonQuery(dbCommandWrapper) > 0;
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
                return false;
            }
        }
        

    }
}
