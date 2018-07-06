using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_AssignRoom
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public int BedID { get; set; }
        public int SiteID { get; set; }
        public int IsActive { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
