using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_AssignDormArea
    {
        public const string col_ID = "ID";
        public const string col_RoomID = "DormAreaID";
        public const string col_EmployeeNo = "EmployeeNo";  
        public const string col_CardNo = "CardNo";     
        public const string col_CreateDate = "CreateDate"; 
        public const string col_IsActive = "CreateUser";

        public int ID { get; set; }
        public int DormAreaID { get; set; }
        public string EmployeeNo { get; set; }
        public string CardNo { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
     
    }
}
