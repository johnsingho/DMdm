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
    public class BuildingBLL
    {
        private TB_BuildingDAL _mTB_BuildingDAL = null;
        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public BuildingBLL()
        {
            _mTB_BuildingDAL = new TB_BuildingDAL();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Building"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_Building tb_Building, ref  Pager pager)
        {
            return _mTB_BuildingDAL.GetTable(tb_Building, ref pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="tb_Building"></param>
        /// <returns></returns>
        public int Edit(TB_Building tb_Building)
        {
            DataTable dt = null;
            //查询宿舍区域名称是否存在
            dt = _mTB_BuildingDAL.GetTable(tb_Building);
            if (dt != null && dt.Rows.Count > 0) _errMessage = "名称重复！";
            else
            {
                //更新操作
                if (tb_Building.ID > 0) _mTB_BuildingDAL.Edit(tb_Building);
                //添加操作
                else tb_Building.ID = _mTB_BuildingDAL.Create(tb_Building);
            }
            return tb_Building.ID;
        }

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="intBuildingID"></param>
        /// <returns></returns>
        public TB_Building Get(int intBuildingID)
        {
            return _mTB_BuildingDAL.Get(intBuildingID);
        }

        /// <summary>
        /// 根据宿舍区ID获取楼栋
        /// </summary>
        /// <param name="intDormAreaID"></param>
        /// <returns></returns>
        public DataTable GetBuildingByDormAreaID(int intDormAreaID)
        {
            Pager mPager = null;
            return _mTB_BuildingDAL.GetTable(new TB_Building() { DormAreaID = intDormAreaID }, ref mPager);
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
            //TB_UnitDAL mTB_UnitDAL = new TB_UnitDAL();

            DataTable dtRoom = new DataTable();
            //DataTable dtUnit = new DataTable();

            string strRoomID = string.Empty;
            //string strUnitID = string.Empty;

            DataRow[] drRoomArr = null;
            //DataRow[] drUnitArr = null;

            dtRoom = mTB_RoomDAL.GetTableBySiteID(intSiteID);
            //dtUnit = mTB_UnitDAL.GetTableBySite(intSiteID);

            foreach (string buildingID in strID.Split(','))
            {
                drRoomArr = (from v in dtRoom.Rows.Cast<DataRow>()
                             where v["BuildingID"].ToString().Equals(buildingID)
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

                //drUnitArr = (from v in dtUnit.Rows.Cast<DataRow>()
                //             where v["BuildingID"].ToString().Equals(buildingID)
                //             select v).ToArray();
                //foreach (DataRow dr in drUnitArr)
                //{
                //    if (string.IsNullOrEmpty(strUnitID))
                //    {
                //        strUnitID = dr["ID"].ToString();
                //    }
                //    else
                //    {
                //        strUnitID += "," + dr["ID"];
                //    }
                //}
            }
            //删除单元
            //new UnitBLL().Remove(strUnitID, tran, db);
            //删除房间
            new RoomBLL().Remove(strRoomID, tran, db);
            //删除楼栋
            _mTB_BuildingDAL.Delete(strID, tran, db);

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
