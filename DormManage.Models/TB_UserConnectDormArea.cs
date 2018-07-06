using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 用户关联宿舍区
    /// </summary>
    public class TB_UserConnectDormArea
    {
        public const string col_ID = "ID";
        public const string col_UserID = "UserID";
        public const string col_DormAreaID = "DormAreaID";

        public int ID { get; set; }
        public int UserID { get; set; }
        public int DormAreaID { get; set; }
    }
}
