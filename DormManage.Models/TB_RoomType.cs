using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 房间类型
    /// </summary>
    public class TB_RoomType
    {
        public const string col_ID = "ID";
        public const string col_SiteID = "SiteID";
        public const string col_Name = "Name";

        public int ID { get; set; }
        public int SiteID { get; set; }
        public string Name { get; set; }
    }
}
