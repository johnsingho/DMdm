using System;

namespace DormManage.Models
{
    // from V_TB_DormAreaApply
    public class TB_DormAreaApply
    {
        public TB_DormAreaApply()
        {
            Status = -1;
        }

        public int ID { get; set; }
        public string EmployeeNo { get; set; }
        public string CName { get; set; }
        public string Sex { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public int Grade { get; set; }
        public int DormAreaID { get; set; }
        public string DormArea { get; set; }
        public int RequireType { get; set; }
        public string RequireReason { get; set; }
        public string HasHousingAllowance { get; set; }
        public string memo { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string Response { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
