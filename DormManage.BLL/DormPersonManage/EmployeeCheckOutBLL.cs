using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.BLL.DormPersonManage
{
    public class EmployeeCheckOutBLL
    {
        private TB_EmployeeCheckOutDAL _mTB_EmployeeCheckOutDAL = null;

        public EmployeeCheckOutBLL()
        {
            _mTB_EmployeeCheckOutDAL = new TB_EmployeeCheckOutDAL();
        }

        /// <summary>
        /// 获取退房记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckOut"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_EmployeeCheckOut tb_EmployeeCheckOut, ref Pager pager)
        {
            return _mTB_EmployeeCheckOutDAL.GetTable(tb_EmployeeCheckOut, ref pager);
        }

        /// <summary>
        /// 获取退房记录分页数据
        /// </summary>
        /// <param name="tb_EmployeeCheckOut"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_EmployeeCheckOut tb_EmployeeCheckOut, int iDormAreaID, int iRoomTypeID, string sRoomName, ref Pager pager)
        {
            return _mTB_EmployeeCheckOutDAL.GetTable(tb_EmployeeCheckOut, iDormAreaID, iRoomTypeID, sRoomName, ref pager);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="tb_EmployeeCheckOut"></param>
        /// <returns></returns>
        public string Export(TB_EmployeeCheckOut tb_EmployeeCheckOut,int iDormAreaID)
        {
            DataTable dt = _mTB_EmployeeCheckOutDAL.GetTable(tb_EmployeeCheckOut, iDormAreaID);
            dt.Columns.Remove("楼层");
            dt.Columns.Remove("单元");
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "Report");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, DateTime.Now.ToString("yyMMddHHmmssms_") + "退房记录.xls");
            new ExcelHelper().RenderToExcel(dt, strFileName);
            return strFileName;
        }
    }
}
