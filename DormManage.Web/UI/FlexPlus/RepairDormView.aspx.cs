using DormManage.BLL.FlexPlus;
using DormManage.Common;
using DormManage.Models;
using System;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class RepairDormView : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sid = Request.Params["id"].ToString();
            if (!IsPostBack)
            {
                InitData(sid);
            }
        }

        private void InitData(string sid)
        {
            int nID = 0;
            int.TryParse(sid, out nID);
            if (0 == nID)
            {
                return;
            }
            var bll = new FlexPlusBLL();
            var mItem = new TB_DormRepair();
            mItem.ID = nID;
            Framework.Pager pager = null;
            var dt = bll.GetRepairDormList(mItem, ref pager);
            if (DataTableHelper.IsEmptyDataTable(dt))
            {
                return;
            }

            var dr = dt.Rows[0];
            txtEmployeeNo.Text = dr["EmployeeNo"].ToString();
            txtCName.Text = dr["CName"].ToString();
            txtMobileNo.Text = dr["MobileNo"].ToString();

            txtDormAddress.Text = dr["DormAddress"].ToString();
            var sDate = (dr["CreateDate"]==null) ? ""
                                                 : Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm");
            txtCreateDate.Text = sDate;
            sDate = (dr["RepairTime"] == null) ? ""
                                               : Convert.ToDateTime(dr["RepairTime"]).ToString("yyyy-MM-dd HH:mm");
            txtRepairTime.Text = sDate;

            txtDeviceType.Text = dr["DeviceType"].ToString();
            txtRequireDesc.Value = dr["RequireDesc"].ToString();
        }
    }
}