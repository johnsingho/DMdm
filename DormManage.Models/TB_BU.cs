using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_BU
    {

        public const string col_ID = "ID";
        public const string col_Name = "Name";
        public const string col_SiteID = "SiteID";

        public int ID { get; set; }
        public string Name { get; set; }
        public int SiteID { get; set; }
    }
}
