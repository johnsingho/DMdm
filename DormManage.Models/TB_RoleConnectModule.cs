using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_RoleConnectModule
    {
        public string col_ID = "ID";
        public string col_RoleID = "RoleID";
        public string col_ModuleID = "ModuleID";

        public int ID { get; set; }
        public int RoleID { get; set; }
        public int ModuleID { get; set; }
    }
}
