using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_ChangeRoomRecord
    {
        public const string col_ID = "ID";
        public const string col_SiteID = "SiteID";
        public const string col_EmployeeNo = "EmployeeNo";
        public const string col_Name = "Name";
        public const string col_Sex = "Sex";
        public const string col_BUID = "BUID";
        public const string col_BU = "BU";
        public const string col_Company = "Company";
        public const string col_CardNo = "CardNo";
        public const string col_Telephone = "Telephone";
        public const string col_Province = "Province";
        public const string col_IsSmoking = "IsSmoking";
        public const string col_CheckInDate = "CheckInDate";
        public const string col_CheckOutDate = "ChangeRoomDate";
        public const string col_Creator = "Creator";
        public const string col_CreateDate = "CreateDate";
        public const string col_OldRoomID = "OldRoomID";
        public const string col_OldBedID = "OldBedID";
        public const string col_EmployeeTypeName = "EmployeeTypeName";

        public int ID { get; set; }
        public int SiteID { get; set; }
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public int BUID { get; set; }
        public string BU { get; set; }
        public string Company { get; set; }
        public string CardNo { get; set; }
        public string Telephone { get; set; }
        public string Province { get; set; }
        public bool IsSmoking { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime ChangeRoomDate { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int OldRoomID { get; set; }
        public int OldBedID { get; set; }
        public int NewRoomID { get; set; }
        public int NewBedID { get; set; }
        public string EmployeeTypeName { get; set; }

        //把ChangeRoomDate拆分，用来查询
        public DateTime ChangeRoomDateBegin { get; set; }
        public DateTime ChangeRoomDateEnd { get; set; }
    }
}
