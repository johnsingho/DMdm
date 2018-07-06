using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TB_Charging
    {
        public const string col_ID = "ID";
        public const string col_EmployeeNo = "EmployeeNo";
        public const string col_Name = "Name";
        public const string col_CardNo = "CardNo";
        public const string col_ChargeContent = "ChargeContent";
        public const string col_Money = "Money";
        public const string col_CreateTime = "CreateTime";
        public const string col_Creator = "Creator";
        public const string col_UpdateDate = "UpdateDate";
        public const string col_UpdateBy = "UpdateBy";
        public const string col_SiteID = "SiteID";
        public const string col_BUID = "BUID";
        public const string col_BU = "BU";

        public int ID { get; set; }
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public string CardNo { get; set; }
        public string ChargeContent { get; set; }
        public decimal Money { get; set; }
        public string AirConditionFee { get; set; }
        public decimal AirConditionFeeMoney { get; set; }
        public string RoomKeyFee { get; set; }
        public decimal RoomKeyFeeMoney { get; set; }
        public string OtherFee { get; set; }
        public decimal OtherFeeMoney { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public int SiteID { get; set; }
        public int BUID { get; set; }
        public string BU { get; set; }

        //拆分CreateTime，用于查询
        public DateTime CreateTimeBegin { get; set; }
        public DateTime CreateTimeEnd { get; set; }
    }
}
