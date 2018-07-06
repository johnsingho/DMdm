using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 员工退房记录
    /// </summary>
    public class TB_EmployeeCheckOut
    {
        public const string col_ID = "ID";
        public const string col_RoomID = "RoomID";
        public const string col_BedID = "BedID";
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
        public const string col_CheckOutDate = "CheckOutDate";
        public const string col_Creator = "Creator";
        public const string col_CreateDate = "CreateDate";
        public const string col_UpdateBy = "UpdateBy";
        public const string col_UpdateDate = "UpdateDate";
        public const string col_SiteID = "SiteID";
        public const string col_Reason = "Reason";
        public const string col_Remark = "Remark";
        public const string col_EmployeeTypeName = "EmployeeTypeName";

        public int ID { get; set; }
        public int RoomID { get; set; }
        public int BedID { get; set; }
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
        public DateTime CheckOutDate { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int SiteID { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public int CanLeave { get; set; }
        public string EmployeeTypeName { get; set; }

        //将CheckOutDate拆分为范围，用于查询
        public DateTime CheckOutDateBegin { get; set; }
        public DateTime CheckOutDateEnd { get; set; }
    }
}
