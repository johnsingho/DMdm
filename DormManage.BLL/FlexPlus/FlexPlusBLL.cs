using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using System.Data;
using DormManage.BLL.AssignRoom;

namespace DormManage.BLL.FlexPlus
{
    public class FlexPlusBLL
    {
        private FlexPlusBAL _mDAL = null;
        public FlexPlusBLL()
        {
            _mDAL = new FlexPlusBAL();
        }

        public DataTable GetApplyDorms(TB_DormAreaApply mTB_DormAreaApply, ref Pager pager)
        {
            return _mDAL.GetApplyDorms(mTB_DormAreaApply, pager);
        }

        public void ApplyDorm(List<string> mKeys, string sHandlerWorkdayNo, string sDormAreaID, string sHandle, string sMsg)
        {
            var assignArea = new AssignRoomBLL();
            foreach (var k in mKeys)
            {
                var dtAppInfo = _mDAL.GetApplyDormByID(k);
                var dr = dtAppInfo.Rows[0];
                var sWorkdayNo = dr["EmployeeNo"] as string;

                if ("1" == sHandle)
                {
                    TB_AssignDormArea tB_AssignDormArea = new TB_AssignDormArea();
                    tB_AssignDormArea.DormAreaID = Convert.ToInt32(sDormAreaID);
                    tB_AssignDormArea.CardNo = dr["CardNo"] as string;
                    tB_AssignDormArea.EmployeeNo = sWorkdayNo;
                    tB_AssignDormArea.CreateUser = sHandlerWorkdayNo;
                    tB_AssignDormArea.CreateDate = System.DateTime.Now;

                    if (assignArea.AssignArea(tB_AssignDormArea))
                    {
                        _mDAL.HandleApplyDorm(k, sHandle, sMsg);
                    }
                }
                else
                {
                    _mDAL.HandleApplyDorm(k, sHandle, sMsg);
                }
                MessageBLL.SendJpush(sWorkdayNo, "宿舍申请", "宿舍申请", sMsg, "msg");
            }

        }
    }
}
