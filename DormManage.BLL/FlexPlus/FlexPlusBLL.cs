using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using System.Data;

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
    }
}
