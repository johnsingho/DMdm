using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// Site
    /// </summary>
    public class TB_Site
    {
        public const string col_ID = "ID";
        public const string col_SiteName = "SiteName";
        public const string col_SiteCode = "SiteCode";

        public int ID { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
    }
}
