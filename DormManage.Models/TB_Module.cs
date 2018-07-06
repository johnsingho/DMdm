using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 模块
    /// </summary>
    public class TB_Module
    {
        public const string col_ID = "ID";
        public const string col_PID = "PID";
        public const string col_Name = "Name";
        public const string col_URL = "URL";
        public const string col_IsActive = "IsActive";
        public const string col_SiteID = "SiteID";

        public int ID { get; set; }
        public int PID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int IsActive { get; set; }
        public int SiteID { get; set; }
    }
}
