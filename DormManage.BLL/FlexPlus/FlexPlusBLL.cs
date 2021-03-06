﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using System.Data;
using DormManage.BLL.AssignRoom;
using System.IO;
using DormManage.Common;

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
        
        public void ApplyDorm(List<string> mKeys, string sHandlerWorkdayNo, string sEnName, string sDormAreaID, string sHandle, string sMsg)
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
                        _mDAL.HandleApplyDorm(k, sEnName, sHandle, sMsg);
                    }
                }
                else
                {
                    _mDAL.HandleApplyDorm(k, sEnName, sHandle, sMsg);
                }
                MessageBLL.SendJpush(sWorkdayNo, "宿舍申请", "宿舍申请", sMsg, "msg");
            }

        }

        public bool IsHandledRequire(int kind, string mKey, out string sAppr, out string sResp)
        {
            sAppr = "1";
            sResp = string.Empty;
            var bRet = false;
            if (0 == kind)
            {
                var dt = _mDAL.GetRepairDormByID(mKey);
                var dr = dt.Rows[0];
                int nStatus = 0;
                nStatus = Convert.ToInt32(dr["Status"]);
                bRet = nStatus > 0;
                if (bRet)
                {
                    sAppr = dr["Status"].ToString();
                    sResp = dr["Response"].ToString();
                }
            }
            else if (1 == kind)
            {
                var dt = _mDAL.GetReissueKeyByID(mKey);
                var dr = dt.Rows[0];
                int nStatus = 0;
                nStatus = Convert.ToInt32(dr["Status"]);
                bRet = nStatus > 0;
                if (bRet)
                {
                    sAppr = dr["Status"].ToString();
                    sResp = dr["Response"].ToString();
                }
            }
            return bRet;
        }

        public bool HandleRequired(int kind, string mKey, string sHandlerWorkdayNo, string sHandle, string sMsg)
        {
            var bRet = false;
            if (0==kind)
            {
                var dt = _mDAL.GetRepairDormByID(mKey);
                var dr = dt.Rows[0];
                var sWorkdayNo = dr["EmployeeNo"] as string;

                bRet = _mDAL.HandleRepairDorm(mKey, sHandlerWorkdayNo, sHandle, sMsg);
                MessageBLL.SendJpush(sWorkdayNo, "宿舍报修", "宿舍报修", sMsg, "msg");
            }
            else if (1==kind)
            {
                var dt = _mDAL.GetReissueKeyByID(mKey);
                var dr = dt.Rows[0];
                var sWorkdayNo = dr["EmployeeNo"] as string;

                bRet = _mDAL.HandleReissueKey(mKey, sHandlerWorkdayNo, sHandle, sMsg);
                MessageBLL.SendJpush(sWorkdayNo, "补办钥匙", "补办钥匙", sMsg, "msg");
            }
            return bRet;
        }

        public bool HandleSuggest(string key, string sHandlerWorkdayNo, string sMsg)
        {
            var bRet = false;
            bRet = _mDAL.HandleSuggest(key, sHandlerWorkdayNo, sMsg);

            var dt = _mDAL.GetDormSuggestByID(key);
            var dr = dt.Rows[0];
            var sWorkdayNo = dr["EmployeeNo"] as string;
            MessageBLL.SendJpush(sWorkdayNo, "宿舍建议箱", "宿舍建议箱", sMsg, "msg");
            return bRet;
        }

        public bool EditDormNotice(string key, string sTitle, string sContext, string sCreator)
        {
            return _mDAL.EditDormNotice(key, sTitle, sContext, sCreator);
        }

        public bool AddDormNotice(string sTitle, string sContext, string sCreator)
        {
            return _mDAL.AddDormNotice(sTitle, sContext, sCreator);
        }

        public void DelDormNotice(string key)
        {
            _mDAL.DelDormNotice(key);
        }

        public void SetDormNoticeEnable(string key, bool bEnable)
        {
            _mDAL.SetDormNoticeEnable(key, bEnable);
        }

        public DataTable GetRepairDormTypeList()
        {
            return _mDAL.GetRepairDormTypeList();
        }
        public DataTable GetRepairDormList(TB_DormRepair mItem, ref Pager pager)
        {
            return _mDAL.GetRepairDormList(mItem, pager);
        }
        public DataTable GetRepairDormImage(string batchNo)
        {
            return _mDAL.GetRepairDormImage(batchNo);
        }

        public DataTable GetReissueKeyList(TB_DormReissueKey mItem, ref Pager pager)
        {
            return _mDAL.GetReissueKeyList(mItem, pager);
        }

        public DataTable GetDormNoticeByID(string key)
        {
            return _mDAL.GetDormNoticeByID(key);
        }

        public DataTable GetDormNotice(ref Pager pager)
        {
            return _mDAL.GetDormNotice(pager);
        }

        public DataTable GetDormSuggestList(TB_DormSuggest mItem, ref Pager pager)
        {
            return _mDAL.GetDormSuggestList(mItem, pager);
        }
        public DataTable GetDormSuggestByID(string key)
        {
            return _mDAL.GetDormSuggestByID(key);
        }


        public string ExportRepairDorm(TB_DormRepair mItem, string sfn)
        {
            DataTable dt = _mDAL.GetRepairDormList(mItem);
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "RepairDorm");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, sfn);
            new ExcelHelper().RenderToExcel(dt, strFileName);
            return strFileName;
        }

        public string ExportDormSugget(TB_DormSuggest mItem, string sfn)
        {
            DataTable dt = _mDAL.GetDormSuggestList(mItem);
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "DormSuggest");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, sfn);
            new ExcelHelper().RenderToExcel(dt, strFileName);
            return strFileName;
        }
    }
}
