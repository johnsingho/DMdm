using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.BLL.UserManage
{
    public class BUBLL
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="tb_BU"></param>
        /// <returns></returns>
        public int Edit(TB_BU tb_BU)
        {
            TB_BUDAL mTB_BUDAL = new TB_BUDAL();
            //编辑
            if (tb_BU.ID > 0)
            {
                return mTB_BUDAL.Edit(tb_BU);
            }
            //添加
            else
            {
                return mTB_BUDAL.Create(tb_BU);
            }
        }

        /// <summary>
        /// 根据SiteID获取到所有的事业部
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable(int intSiteID)
        {
            return new TB_BUDAL().GetTable(intSiteID);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Role"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetPagerData(TB_BU tb_BU, ref Pager pager)
        {
            return new TB_BUDAL().GetTable(tb_BU, ref pager);
        }

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="intID"></param>
        /// <returns></returns>
        public TB_BU Get(int intID)
        {
            DataTable dt = new TB_BUDAL().Get(intID);
            var query = (from v in dt.AsEnumerable()
                         select new
                         {
                             ID = v.Field<int>(TB_BU.col_ID),
                             Name = v.Field<string>(TB_BU.col_Name),
                             SiteID = v.Field<int>(TB_BU.col_SiteID),
                         }).ToList();
            TB_BU mTB_BU = new TB_BU();
            mTB_BU.ID = query.FirstOrDefault().ID;
            mTB_BU.Name = query.FirstOrDefault().Name;
            mTB_BU.SiteID = query.FirstOrDefault().SiteID;
            return mTB_BU;
        }

    }
}
