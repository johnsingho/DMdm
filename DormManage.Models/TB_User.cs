using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class TB_User
    {
        public const string col_ID = "ID";
        public const string col_SiteID = "SiteID";
        public const string col_ADAccount = "ADAccount";
        public const string col_EmployeeNo = "EmployeeNo";
        public const string col_EName = "EName";
        public const string col_CName = "CName";
        public const string col_RoleID = "RoleID";
        public const string col_Creator = "Creator";
        public const string col_CreateDate = "CreateDate";
        public const string col_UpdateBy = "UpdateBy";
        public const string col_UpdateDate = "UpdateDate";

        public int ID { get; set; }
        public int SiteID { get; set; }
        public string ADAccount { get; set; }
        public string EmployeeNo { get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
        public int RoleID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
