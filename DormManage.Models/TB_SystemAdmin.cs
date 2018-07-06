using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 系统超级管理员
    /// </summary>
    public class TB_SystemAdmin
    {
        public const string col_ID = "ID";
        public const string col_SiteID = "SiteID";
        public const string col_Account = "Account";
        public const string col_PassWord = "PassWord";

        public int ID { get; set; }
        public int SiteID { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
    }
}
