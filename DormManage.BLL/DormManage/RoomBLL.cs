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
    public class RoomBLL
    {
        private TB_RoomDAL _mTB_RoomDAL = null;

        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public RoomBLL()
        {
            _mTB_RoomDAL = new TB_RoomDAL();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tb_Room"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_Room tb_Room, ref Pager pager)
        {
            return _mTB_RoomDAL.GetTable(tb_Room, ref pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="tb_Room"></param>
        /// <returns></returns>
        public int Edit(TB_Room tb_Room)
        {
            DataTable dt = null;
            //查询房间号是否存在
            dt = _mTB_RoomDAL.GetTable(tb_Room);
            if (dt != null && dt.Rows.Count > 0) _errMessage = "房间号已存在！";
            else
            {
                //更新操作
                if (tb_Room.ID > 0) _mTB_RoomDAL.Edit(tb_Room);
                //添加操作
                else tb_Room.ID = _mTB_RoomDAL.Create(tb_Room);
            }
            return tb_Room.ID;
        }

        /// <summary>
        /// 根据房间ID获取到房间对象
        /// </summary>
        /// <param name="intRoomID"></param>
        /// <returns></returns>
        public TB_Room Get(int intRoomID)
        {
            return _mTB_RoomDAL.Get(intRoomID);
        }

        /// <summary>
        /// 根据楼层ID获取到房间
        /// </summary>
        /// <param name="intFloorID"></param>
        /// <returns></returns>
        public DataTable GetRoomByFloorID(int intFloorID)
        {
            Pager mPager = null;
            return _mTB_RoomDAL.GetTable(new TB_Room() { FloorID = intFloorID }, ref mPager);
        }

        /// <summary>
        /// 根据楼栋ID获取房间
        /// </summary>
        /// <param name="intBuildingID"></param>
        /// <returns></returns>
        public DataTable GetRoomByBuildingID(int intBuildingID)
        {
            Pager mPager = null;
            return _mTB_RoomDAL.GetTable(new TB_Room() { BuildingID = intBuildingID }, ref mPager);
        }

        /// <summary>
        /// 获取到DormInfo
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetDormInfoBySiteID(int siteID)
        {
            return _mTB_RoomDAL.GetDormInfoBySiteID(siteID);
        }

        

        /// <summary>
        /// 获取到DayDormInfo
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetDayDormInfoBySiteIDEX(int siteID, string startDay, string endDay)
        {
           
            DataTable dtAllBeg = _mTB_RoomDAL.GetDayDormInfoBySiteID(siteID, startDay, endDay, "AllBegCount");
            if (dtAllBeg == null) return null;        
            dtAllBeg.Columns.Add("project");
            for(int i=0;i< dtAllBeg.Rows.Count;i++)
            {
                dtAllBeg.Rows[i]["project"] = "总床位数";
            }

            DataTable dtAllCheckIn = _mTB_RoomDAL.GetDayDormInfoBySiteID(siteID, startDay, endDay, "AllCheckIn");
            dtAllCheckIn.Columns.Add("project");
            for (int i = 0; i < dtAllCheckIn.Rows.Count; i++)
            {
                dtAllCheckIn.Rows[i]["project"] = "已入住人数";
            }

            DataTable dtNewCheckIn = _mTB_RoomDAL.GetDayDormInfoBySiteID(siteID, startDay, endDay, "NewCheckIn");
            dtNewCheckIn.Columns.Add("project");
            for (int i = 0; i < dtNewCheckIn.Rows.Count; i++)
            {
                dtNewCheckIn.Rows[i]["project"] = "新入住人数";
            }

            DataTable dtCheckOut = _mTB_RoomDAL.GetDayDormInfoBySiteID(siteID, startDay, endDay, "CheckOut");
            dtCheckOut.Columns.Add("project");
            for (int i = 0; i < dtCheckOut.Rows.Count; i++)
            {
                dtCheckOut.Rows[i]["project"] = "退宿人数";
            }

            DataTable dtFreeBegCount = _mTB_RoomDAL.GetDayDormInfoBySiteID(siteID, startDay, endDay, "FreeBegCount");
            dtFreeBegCount.Columns.Add("project");
            for (int i = 0; i < dtFreeBegCount.Rows.Count; i++)
            {
                dtFreeBegCount.Rows[i]["project"] = "空床位数";
            }

            //DataTable dtCheckInRate = _mTB_RoomDAL.GetDayDormInfoBySiteID(siteID, startDay, endDay, "CheckInRate");
            //dtCheckInRate.Columns.Add("project");
            //for (int i = 0; i < dtCheckInRate.Rows.Count; i++)
            //{
            //    dtCheckInRate.Rows[i]["project"] = "入住率";
            //}

            //合并
            DataTable dt = dtAllBeg.Copy();

            foreach(DataRow dr in dtAllBeg.Rows)
            {
                //总入住
                DataRow dr_dt = dt.NewRow();
                DataRow[] drAllCheckIn=dtAllCheckIn.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach(DataRow dr1 in drAllCheckIn)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //新入住
                dr_dt = dt.NewRow();
                DataRow[] drNewCheckIn = dtNewCheckIn.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach (DataRow dr1 in drNewCheckIn)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //退宿
                dr_dt = dt.NewRow();
                DataRow[] drCheckOut = dtCheckOut.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach (DataRow dr1 in drCheckOut)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //空床
                dr_dt = dt.NewRow();
                DataRow[] drFreeBegCount = dtFreeBegCount.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach (DataRow dr1 in drFreeBegCount)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //入住率=总入住/总床位
                dr_dt = dt.NewRow();
                dr_dt["Areaname"] = dr["Areaname"].ToString();
                dr_dt["Roomtypt"] = dr["Roomtypt"].ToString();
                dr_dt["project"] = "入住率%";
                if(drAllCheckIn.Length>0)
                {
                    for (int i = 0; i < drAllCheckIn[0].ItemArray.Length; i++)
                    {
                        if (drAllCheckIn[0].Table.Columns[i].ColumnName != "Areaname" && drAllCheckIn[0].Table.Columns[i].ColumnName != "Roomtypt" && drAllCheckIn[0].Table.Columns[i].ColumnName != "project")
                        {
                            if (Convert.ToDecimal(dr[i]==System.DBNull.Value ? 0 : dr[i]) == 0) dr_dt[i]=0;
                            else
                                dr_dt[i] = Math.Round(Convert.ToDecimal(drAllCheckIn[0][i] == System.DBNull.Value ? 0 : drAllCheckIn[0][i]) / Convert.ToDecimal(dr[i]) * 100, 2);
                        }

                    }
                }
               
                dt.Rows.Add(dr_dt);
            }


            return dt;
        }

        /// <summary>
        /// 获取到MonthDormInfo
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataTable GetMonthDormInfoBySiteID(int siteID, string startDay)
        {

            DataTable dtAllBeg = _mTB_RoomDAL.GetMonthDormInfoBySiteID(siteID, startDay,"总床位", "AllBegCount");
            if (dtAllBeg == null) return null;
          

            DataTable dtAllCheckIn = _mTB_RoomDAL.GetMonthDormInfoBySiteID(siteID, startDay, "总入住数", "AllCheckIn");
           
           

            DataTable dtNewCheckIn = _mTB_RoomDAL.GetMonthDormInfoBySiteID(siteID, startDay, "新入住数", "NewCheckIn");
          
          
            DataTable dtCheckOut = _mTB_RoomDAL.GetMonthDormInfoBySiteID(siteID, startDay, "退宿数", "CheckOut");
            
            

            DataTable dtFreeBegCount = _mTB_RoomDAL.GetMonthDormInfoBySiteID(siteID, startDay, "空床位", "FreeBegCount");
           
            

            //DataTable dtCheckInRate = _mTB_RoomDAL.GetDayDormInfoBySiteID(siteID, startDay, endDay, "CheckInRate");
            //dtCheckInRate.Columns.Add("project");
            //for (int i = 0; i < dtCheckInRate.Rows.Count; i++)
            //{
            //    dtCheckInRate.Rows[i]["project"] = "入住率";
            //}

            //合并
            DataTable dt = dtAllBeg.Copy();

            foreach (DataRow dr in dtAllBeg.Rows)
            {
                //总入住
                DataRow dr_dt = dt.NewRow();
                DataRow[] drAllCheckIn = dtAllCheckIn.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach (DataRow dr1 in drAllCheckIn)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //新入住
                dr_dt = dt.NewRow();
                DataRow[] drNewCheckIn = dtNewCheckIn.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach (DataRow dr1 in drNewCheckIn)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //退宿
                dr_dt = dt.NewRow();
                DataRow[] drCheckOut = dtCheckOut.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach (DataRow dr1 in drCheckOut)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //空床
                dr_dt = dt.NewRow();
                DataRow[] drFreeBegCount = dtFreeBegCount.Select("Areaname='" + dr["Areaname"].ToString() + "' and Roomtypt='" + dr["Roomtypt"].ToString() + "'");
                foreach (DataRow dr1 in drFreeBegCount)
                {
                    dr_dt.ItemArray = dr1.ItemArray;
                    dt.Rows.Add(dr_dt);
                }

                //入住率=总入住/总床位
                dr_dt = dt.NewRow();
                dr_dt["Areaname"] = dr["Areaname"].ToString();
                dr_dt["Roomtypt"] = dr["Roomtypt"].ToString();
                dr_dt["project"] = "入住率%";
                if(drAllCheckIn.Length>0)
                {
                    for (int i = 0; i < drAllCheckIn[0].ItemArray.Length; i++)
                    {
                        if (drAllCheckIn[0].Table.Columns[i].ColumnName != "Areaname" && drAllCheckIn[0].Table.Columns[i].ColumnName != "Roomtypt" && drAllCheckIn[0].Table.Columns[i].ColumnName != "project")
                        {
                            if (Convert.ToDecimal(dr[i])==0) dr_dt[i] = 0;
                            else
                            dr_dt[i] = Math.Round(Convert.ToDecimal(drAllCheckIn[0][i]) / Convert.ToDecimal(dr[i]) * 100, 2);
                        }

                    }
                }
                dt.Rows.Add(dr_dt);
            }


            return dt;
        }

        /// <summary>
        /// 获取到未锁定的房间数
        /// </summary>
        /// <param name="tb_Room"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetUnLockRoom(TB_Room tb_Room, ref Pager pager)
        {
            return _mTB_RoomDAL.GetUnLockRoom(tb_Room, ref pager);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        internal void Remove(string strID, DbTransaction tran, Database db)
        {
            int intSiteID = SessionHelper.Get(HttpContext.Current, TypeManager.User) != null ?
                ((TB_User)SessionHelper.Get(HttpContext.Current, TypeManager.User)).SiteID :
                ((TB_SystemAdmin)SessionHelper.Get(HttpContext.Current, TypeManager.Admin)).SiteID;
            TB_BedDAL mTB_BedDAL = new TB_BedDAL();

            DataTable dtBed = new DataTable();

            string strBedID = string.Empty;

            DataRow[] drBedArr = null;

            dtBed = mTB_BedDAL.GetTableBySite(intSiteID);

            foreach (string roomID in strID.Split(','))
            {
                drBedArr = (from v in dtBed.Rows.Cast<DataRow>()
                            where v["RoomID"].ToString().Equals(roomID)
                            select v).ToArray();
                foreach (DataRow dr in drBedArr)
                {
                    if (string.IsNullOrEmpty(strBedID))
                    {
                        strBedID = dr["ID"].ToString();
                    }
                    else
                    {
                        strBedID += "," + dr["ID"];
                    }
                }
            }
            //删除床位号
            new BedBLL().Remove(strBedID, tran, db);
            //删除房间
            _mTB_RoomDAL.Delete(strID, tran, db);
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
