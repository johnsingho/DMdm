using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 床号
    /// </summary>
    public class TB_Bed
    {
        public const string col_ID = "ID";
        public const string col_SiteID = "SiteID";
        public const string col_DormAreaID = "DormAreaID";
        public const string col_BuildingID = "BuildingID";
        public const string col_UnitID = "UnitID";
        public const string col_FloorID = "FloorID";
        public const string col_RoomID = "RoomID";
        public const string col_Name = "Name";
        public const string col_Status = "Status";
        public const string col_Creator = "Creator";
        public const string col_CreateDate = "CreateDate";
        public const string col_UpdateBy = "UpdateBy";
        public const string col_UpdateDate = "UpdateDate";
        public const string col_KeyCount = "KeyCount";

        public int ID { get; set; }
        public int SiteID { get; set; }
        public int DormAreaID { get; set; }
        public int BuildingID { get; set; }
        public int UnitID { get; set; }
        public int FloorID { get; set; }
        public int RoomID { get; set; }
        public int RoomType { get; set; }
        public string RoomSexType { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int KeyCount { get; set; }
    }
}
