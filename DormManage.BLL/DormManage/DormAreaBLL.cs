using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;
using DormManage.Models;
using DormManage.Framework;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DormManage.Common;

namespace DormManage.BLL.DormManage
{
    public class DormAreaBLL
    {
        private TB_DormAreaDAL _mTB_DormAreaDAL;
        private string _errMessage = string.Empty;

        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        public DormAreaBLL()
        {
            _mTB_DormAreaDAL = new TB_DormAreaDAL();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="mTB_DormArea"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public DataTable GetTable(TB_DormArea tb_DormArea, ref Pager pager)
        {
            return _mTB_DormAreaDAL.GetTable(tb_DormArea, ref pager);
        }

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="intID"></param>
        /// <returns></returns>
        public TB_DormArea Get(int intID)
        {
            return _mTB_DormAreaDAL.Get(intID);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="tb_DormArea"></param>
        /// <returns></returns>
        public int Edit(TB_DormArea tb_DormArea)
        {
            DataTable dt = null;
            //查询宿舍区域名称是否存在
            dt = _mTB_DormAreaDAL.GetTable(tb_DormArea);
            if (dt != null && dt.Rows.Count > 0) _errMessage = "名称重复！";
            else
            {
                //更新操作
                if (tb_DormArea.ID > 0) _mTB_DormAreaDAL.Edit(tb_DormArea);
                //添加操作
                else tb_DormArea.ID = _mTB_DormAreaDAL.Create(tb_DormArea);
            }
            return tb_DormArea.ID;
        }

        /// <summary>
        /// 获取到用户拥有的宿舍区
        /// </summary>
        /// <param name="intUserID"></param>
        /// <returns></returns>
        public DataTable GetTableByUserID(int intUserID)
        {
            return _mTB_DormAreaDAL.GetTable(intUserID);
        }

        /// <summary>
        /// 删除宿舍区
        /// </summary>
        /// <param name="strID"></param>
        public void Remove(string strID)
        {
            int intSiteID = System.Web.HttpContext.Current.Session[TypeManager.User] != null ?
                ((TB_User)System.Web.HttpContext.Current.Session[TypeManager.User]).SiteID :
                ((TB_SystemAdmin)System.Web.HttpContext.Current.Session[TypeManager.Admin]).SiteID;

            TB_BuildingDAL mTB_BuildingDAL = new TB_BuildingDAL();
            TB_UserConnectDormAreaDAL mTB_UserConnectDormArea = new TB_UserConnectDormAreaDAL();

            DataTable dtBuilding = new DataTable();

            string strBuildingID = string.Empty;

            DataRow[] drBuildingArr = null;

            Database db = DBO.GetInstance();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction tran = connection.BeginTransaction();
            try
            {
                dtBuilding = mTB_BuildingDAL.GetTableBySiteID(intSiteID);
                foreach (string dormID in strID.Split(','))
                {
                    drBuildingArr = (from v in dtBuilding.Rows.Cast<DataRow>()
                                     where v["DormAreaID"].ToString().Equals(dormID)
                                     select v).ToArray();
                    foreach (DataRow dr in drBuildingArr)
                    {
                        if (string.IsNullOrEmpty(strBuildingID))
                        {
                            strBuildingID = dr["ID"].ToString();
                        }
                        else
                        {
                            strBuildingID += "," + dr["ID"];
                        }
                    }
                }
                //删除楼栋
                new BuildingBLL().Remove(strBuildingID, tran, db);
                //删除宿舍区与用户的关联关系
                mTB_UserConnectDormArea.Delete(strID, tran, db);
                //删除宿舍区
                _mTB_DormAreaDAL.Delete(strID, tran, db);
                //提交事务
                tran.Commit();
            }
            catch (Exception ex)
            {
                //回滚事务
                tran.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                connection.Close();
            }
        }
    }
}
