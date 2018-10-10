using DormManage.Data.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DormManage.BLL
{
    public class CommonBLL
    {
        public void ChangeBegEnable( int ID,string value)
        {
            try
            {
                CommonDAL dal = new CommonDAL();

                bool isOK= dal.ChangeBegStatus(ID);

                if(isOK)
                {
                    dal.ChangeBegEnable(ID, value);
                }
                else
                {
                   Exception e= new Exception("床位已有入住，不能禁用");
                   throw e;
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
               
            }
        }

        public void BatchChangeBedEnable(List<int> ids, bool bEnable)
        {
            CommonDAL dal = new CommonDAL();
            if (!bEnable)
            {
                //不能禁用已被入住房间
                List<int> lUsed = dal.CheckBedCheckins(ids);
                if (lUsed.Count > 0)
                {
                    var sUsed = string.Join(",", lUsed);
                    var sPrompt = string.Format("床位已有入住，不能禁用: {0}", sUsed);
                    Exception e = new Exception(sPrompt);
                    throw e;
                }
            }

            dal.ChangeBedEnable(ids, bEnable);
        }

        public void ChangeRoomEnable( int ID,string value)
        {
            try
            {
                CommonDAL dal = new CommonDAL();
             

                bool isOK = dal.ChangeRoomStatus(ID);

                if (isOK)
                {
                    dal.ChangeRoomEnable(ID, value);
                }
                else
                {
                    Exception e = new Exception("床位已有入住，不能禁用");
                    throw e;
                }


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
               
            }
        }

        /// <summary>
        /// 批量启用/禁用房间
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="bEnable"></param>
        public void ChangeRoomEnable(List<int> ids, bool bEnable)
        {
            CommonDAL dal = new CommonDAL();
            if (!bEnable)
            {
                //不能禁用已被入住房间
                List<int> lUsed = dal.CheckRoomCheckins(ids);
                if (lUsed.Count > 0)
                {
                    var sUsed = string.Join(",", lUsed);
                    var sPrompt = string.Format("床位已有入住，不能禁用: {0}", sUsed);
                    Exception e = new Exception(sPrompt);
                    throw e;
                }
            }

            dal.ChangeRoomEnable(ids, bEnable);
        }

        public void ChangeBuildingEnable(int ID, string value)
        {
            try
            {
                CommonDAL dal = new CommonDAL();

                bool isOK = false;
                if(value == "已禁用")
                {
                    isOK = true;
                }
                else
                {
                    isOK = dal.ChangeBuildingStatus(ID);
                }

                
                

                if (isOK)
                {
                    dal.ChangeBuildingEnable(ID, value);
                }
                else
                {
                    Exception e = new Exception("床位已有入住，不能禁用");
                    throw e;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }
        }

        public void ChangeDormAreaEnable(int ID, string value)
        {
            try
            {
                CommonDAL dal = new CommonDAL();


                bool isOK = dal.ChangeDormAreaStatus(ID);

                if (isOK)
                {
                    dal.ChangeDormAreaEnable(ID, value);
                }
                else
                {
                    Exception e = new Exception("床位已有入住，不能禁用");
                    throw e;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }
        }

        public class TEmpInfo{
            public string empID { get; set; }
            public string idCardNum { get; set; }
            public string sname { get; set; }
        }

        private static string ConvertICCard(string sICCardNo)
        {
            //转换成16进制
            var ICCardNo = UInt64.Parse(sICCardNo);
            string ICCard_16 = ICCardNo.ToString("X8");
            //取反
            string ICCard_16Cross = ICCard_16.Substring(6, 2) + ICCard_16.Substring(4, 2) + ICCard_16.Substring(2, 2) + ICCard_16.Substring(0, 2);
            return ICCard_16Cross;
        }


        //用来区分是IC读卡器得到的员工卡内芯片号，还是手输的员工卡号
        private static readonly int SNR_LIMIT = 10;

        //根据工号或员工卡内芯片号在一卡通库找用户信息
        public static TEmpInfo GetEmployeeInfo(string sICCardNo)
        {
            //johnsing 2018-10-10 长沙版不启用此功能
            return null;
            //DataTable dt = null;
            //if(sICCardNo.Length >= SNR_LIMIT)
            //{
            //    // IC卡转换成工号
            //    var ICCard_16Cross = ConvertICCard(sICCardNo);
            //    dt = CommonDAL.GetEmployeeInfoByIcCard(ICCard_16Cross);
            //}
            //else
            //{
            //    dt = CommonDAL.GetEmployeeInfoByWorkID(sICCardNo);
            //}
            
            //if (dt!=null && dt.Rows.Count>0)
            //{
            //    var r = dt.Rows[0];
            //    var emp = new TEmpInfo();
            //    emp.empID = r["OutID"].ToString();
            //    emp.idCardNum = r["IDCardNo"].ToString();
            //    emp.sname = r["Name"].ToString();
            //    return emp;
            //}
            //return null;
        }

    }
}
