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
    public class ChangeRoomRecordBLL
    {
        private TB_ChangeRoomRecordDAL _mTB_ChangeRoomRecordDAL = null;
        private ExcelHelper _mExcelHelper = null;
        public ChangeRoomRecordBLL()
        {
            _mTB_ChangeRoomRecordDAL = new TB_ChangeRoomRecordDAL();
            _mExcelHelper = new ExcelHelper();
        }

        /// <summary>
        /// 获取到分页数据
        /// </summary>
        /// <param name="tb_ChangeRoomRecord"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_ChangeRoomRecord tb_ChangeRoomRecord, ref Pager pager)
        {
            return _mTB_ChangeRoomRecordDAL.GetTable(tb_ChangeRoomRecord, ref pager);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="tb_ChangeRoomRecord"></param>
        /// <returns></returns>
        public string Export(TB_ChangeRoomRecord tb_ChangeRoomRecord)
        {
            DataTable dt = _mTB_ChangeRoomRecordDAL.GetTable(tb_ChangeRoomRecord);
            dt.Columns.Remove("原楼层");
            dt.Columns.Remove("原单元");
            dt.Columns.Remove("新楼层");
            dt.Columns.Remove("新单元");
            string strFilePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("..\\..\\"), "Report");
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            string strFileName = Path.Combine(strFilePath, DateTime.Now.ToString("yyMMddHHmmssms_") + "换房记录.xls");
            _mExcelHelper.RenderToExcel(dt, strFileName);
            return strFileName;
        }
    }
}
