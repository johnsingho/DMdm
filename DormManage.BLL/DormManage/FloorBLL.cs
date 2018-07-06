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

namespace DormManage.BLL.DormManage
{
    public class FloorBLL
    {
        private TB_FloorDAL _mTB_FloorDAL = null;

        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public FloorBLL()
        {
            _mTB_FloorDAL = new TB_FloorDAL();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Floor"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_Floor tb_Floor, ref Pager pager)
        {
            return _mTB_FloorDAL.GetTable(tb_Floor, ref pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="tb_Floor"></param>
        /// <returns></returns>
        public int Edit(TB_Floor tb_Floor)
        {
            DataTable dt = null;
            dt = _mTB_FloorDAL.GetTable(tb_Floor);
            if (dt != null && dt.Rows.Count > 0) _errMessage = "名称重复！";
            else
            {
                //更新操作
                if (tb_Floor.ID > 0) _mTB_FloorDAL.Edit(tb_Floor);
                //添加操作
                else tb_Floor.ID = _mTB_FloorDAL.Create(tb_Floor);
            }
            return tb_Floor.ID;
        }

        /// <summary>
        /// 根据单元ID获取到单元对象
        /// </summary>
        /// <param name="intFloorID"></param>
        /// <returns></returns>
        public TB_Floor Get(int intFloorID)
        {
            return _mTB_FloorDAL.Get(intFloorID);
        }

        /// <summary>
        /// 根据单元ID获取到楼层
        /// </summary>
        /// <param name="intUnitID"></param>
        /// <returns></returns>
        public DataTable GetFloorByUnitID(int intUnitID)
        {
            Pager mPager = null;
            return _mTB_FloorDAL.GetTable(new TB_Floor() { UnitID = intUnitID }, ref mPager);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        internal void Remove(string strID, DbTransaction tran, Database db)
        {
            int intSiteID = System.Web.HttpContext.Current.Session[TypeManager.User] != null ?
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).SiteID :
                ((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).SiteID;

            TB_RoomDAL mTB_RoomDAL = new TB_RoomDAL();

            DataTable dtRoom = new DataTable();

            string strRoomID = string.Empty;

            DataRow[] drRoomArr = null;

            dtRoom = mTB_RoomDAL.GetTableBySiteID(intSiteID);
            foreach (string floorID in strID.Split(','))
            {
                drRoomArr = (from v in dtRoom.Rows.Cast<DataRow>()
                             where v["FloorID"].ToString().Equals(floorID)
                             select v).ToArray();
                foreach (DataRow dr in drRoomArr)
                {
                    if (string.IsNullOrEmpty(strRoomID))
                    {
                        strRoomID = dr["ID"].ToString();
                    }
                    else
                    {
                        strRoomID += "," + dr["ID"];
                    }
                }
            }
            //删除房间
            new RoomBLL().Remove(strRoomID, tran, db);
            //删除楼层
            _mTB_FloorDAL.Delete(strID, tran, db);

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
