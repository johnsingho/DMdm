using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_DormReissueKey
    {
        public TB_DormReissueKey()
        {
            Status = -1;
        }
        public int ID { get; set; }
        public string CName { get; set; }
        public string EmployeeNo { get; set; }
        public string MobileNo { get; set; }
        public string DormAddress { get; set; }
        public string KeyTypes { get; set; }
        public string Reason { get; set; }
        public string Memo { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public string ModifyUserID { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
