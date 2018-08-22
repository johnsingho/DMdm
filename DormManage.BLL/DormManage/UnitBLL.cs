using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DormManage.Common;
using DormManage.Data.DAL;
using DormManage.Framework;
using DormManage.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web;

namespace DormManage.BLL.DormManage
{
    public class UnitBLL
    {
        private TB_UnitDAL _mTB_UnitDAL = null;

        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public UnitBLL()
        {
            _mTB_UnitDAL = new TB_UnitDAL();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Unit"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_Unit tb_Unit, ref Pager pager)
        {
            return _mTB_UnitDAL.GetTable(tb_Unit, ref pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="tb_DormArea"></param>
        /// <returns></returns>
        public int Edit(TB_Unit tb_Unit)
        {
            DataTable dt = null;
            //查询宿舍区域名称是否存在
            dt = _mTB_UnitDAL.GetTable(tb_Unit);
            if (dt != null && dt.Rows.Count > 0) _errMessage = "名称重复！";
            else
            {
                //更新操作
                if (tb_Unit.ID > 0) _mTB_UnitDAL.Edit(tb_Unit);
                //添加操作
                else tb_Unit.ID = _mTB_UnitDAL.Create(tb_Unit);
            }
            return tb_Unit.ID;
        }

        /// <summary>
        /// 根据单元ID获取到单元对象
        /// </summary>
        /// <param name="intUnitID"></param>
        /// <returns></returns>
        public TB_Unit Get(int intUnitID)
        {
            return _mTB_UnitDAL.Get(intUnitID);
        }

        /// <summary>
        /// 根据楼栋ID获取到单元
        /// </summary>
        /// <param name="intBuildingID"></param>
        /// <returns></returns>
        public DataTable GetUnitByBuildingID(int intBuildingID)
        {
            Pager mPager = null;
            return _mTB_UnitDAL.GetTable(new TB_Unit() { BuildingID = intBuildingID }, ref mPager);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        internal void Remove(string strID, DbTransaction tran, Database db)
        {
            int intSiteID = SessionHelper.Get(HttpContext.Current, TypeManager.User)!=null
                            ? ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).SiteID
                            : ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).SiteID;                

            TB_FloorDAL mTB_FloorDAL = new TB_FloorDAL();

            DataTable dtFloor = new DataTable();

            string strFloorID = string.Empty;

            DataRow[] drFloorArr = null;

            dtFloor = mTB_FloorDAL.GetTableBySite(intSiteID);
            foreach (string unitID in strID.Split(','))
            {
                drFloorArr = (from v in dtFloor.Rows.Cast<DataRow>()
                              where v["UnitID"].ToString().Equals(unitID)
                              select v).ToArray();
                foreach (DataRow dr in drFloorArr)
                {
                    if (string.IsNullOrEmpty(strFloorID))
                    {
                        strFloorID = dr["ID"].ToString();
                    }
                    else
                    {
                        strFloorID += "," + dr["ID"];
                    }
                }
            }
            //删除楼层
            new FloorBLL().Remove(strFloorID, tran, db);
            //删除单元
            _mTB_UnitDAL.Delete(strID, tran, db);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        public void Remove(string strID)
        {
            Database db = DBO.GetInstance();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction tran = connection.BeginTransaction();
            try
            {
                this.Remove(strID, tran, db);
                tran.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
