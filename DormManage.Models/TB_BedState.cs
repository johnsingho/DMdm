using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    /// <summary>
    /// 床位状态
    /// </summary>
    public class TB_BedState
    {
        public const string col_ID = "ID";
        public const string col_Name = "Name";

        public int ID { get; set; }
        public string Name { get; set; }
    }
}
