using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DormManage.BLL.AssignRoom
{
   public class StaffingBLL
    {

        private TB_StaffingDAL _mTB_StaffingDAL = null;
        //private Database _db;
        //private DbConnection _connection;
        //private DbTransaction _tran;

        public StaffingBLL()
        {
            _mTB_StaffingDAL = new TB_StaffingDAL();
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="strCardNo">身份证号码</param>
        /// <param name="strStatus">0为未分配，1为已分配</param>
        /// <returns></returns>
        public DataTable GetData(string seCardID)
        {
            return _mTB_StaffingDAL.GetTable(seCardID);
        }

        public DataTable GetTableWithIDL(string sWorkdayNo, string seCardID)
        {
            return _mTB_StaffingDAL.GetTableWithIDL(sWorkdayNo, seCardID);
        }
    }
}
