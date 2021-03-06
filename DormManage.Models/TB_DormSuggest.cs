﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_DormSuggest
    {
        public TB_DormSuggest()
        {
            Status = -1;
        }

        public int ID { get; set; }
        public string CName { get; set; }
        public string EmployeeNo { get; set; }
        public string MobileNo { get; set; }
        public string Suggest { get; set; }
        public string Response { get; set; }
        //public DateTime CreateDate { get; set; }
        public DateTime SubmitDayBegin { get; set; }
        public DateTime SubmitDayEnd { get; set; }
        public int Status { get; set; }
        public string ModifyUserID { get; set; }
        public DateTime ModifyDate { get; set; }        
    }
}
