using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_DormRepair
    {
        public TB_DormRepair()
        {
            Status = -1;
        }

        public int ID { get; set; }
        public string CName { get; set; }
        public string EmployeeNo { get; set; }
        public string MobileNo { get; set; }
        public string DormAddress { get; set; }
        public DateTime RepairTime { get; set; }
        public string DeviceType { get; set; }
        public string RequireDesc { get; set; }
        //public DateTime CreateDate { get; set; }
        public DateTime SubmitDayBegin { get; set; }
        public DateTime SubmitDayEnd { get; set; }

        public int Status { get; set; }
        public string ModifyUserID { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
