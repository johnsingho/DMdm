using DormManage.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;

namespace DormManage.Web
{
    /// <summary>
    /// 员工信息
    /// </summary>
    [DataContract]
    public class TEmpInfo
    {
        [DataMember]
        public string Employee_ID { get; set; }
        [DataMember]
        public string Chinese_Name { get; set; }
        [DataMember]
        public string English_Name { get; set; }
        [DataMember]
        public string IDCardNumber { get; set; }
        [DataMember]
        public string Segment { get; set; }
        [DataMember]
        public DateTime Hire_Date { get; set; }
        [DataMember]
        public string EmployeeTypeName { get; set; }
    }

    /// <summary>
    /// 返回值
    /// </summary>
    [DataContract]
    public class TOperRes
    {
        [DataMember]
        public bool bok { get; set; }
        [DataMember]
        public string msg { get; set; }
    }

    /// <summary>
    /// Summary description for SrvUploadEmp
    /// </summary>
    [WebService(Namespace = "http://dorm.dmn.flextronics.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SrvUploadEmp : System.Web.Services.WebService
    {
        private TB_StaffingDAL mStaffingDal = new TB_StaffingDAL();

        [WebMethod]
        public TOperRes UploadEmp(TEmpInfo empInfo)
        {
            var ret = new TOperRes
            {
                bok = true
            };

            //拷贝一份
            var einf = new DormManage.Data.DAL.TEmpInfo
            {
                Employee_ID = empInfo.Employee_ID,
                Chinese_Name = empInfo.Chinese_Name,
                English_Name = empInfo.English_Name,
                IDCardNumber = empInfo.IDCardNumber,
                Segment = empInfo.Segment,
                Hire_Date = empInfo.Hire_Date,
                EmployeeTypeName = empInfo.EmployeeTypeName
            };

            var sErr = string.Empty;
            ret.bok = mStaffingDal.UploadEmpInfo(einf, out sErr);
            ret.msg = sErr;
            return ret;
        }
        
    }
}
