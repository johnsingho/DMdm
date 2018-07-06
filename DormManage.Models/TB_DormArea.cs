using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 宿舍区
    /// </summary>
    public class TB_DormArea
    {
        public const string col_ID = "ID";
        public const string col_SiteID = "SiteID";
        public const string col_Name = "Name";
        public const string col_Creator = "Creator";
        public const string col_CreateDate = "CreateDate";
        public const string col_UpdateBy = "UpdateBy";
        public const string col_UpdateDate = "UpdateDate";

        public int ID { get; set; }
        public int SiteID { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
