using DormManage.BLL.FlexPlus;
using DormManage.Common;
using DormManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class ReissueKeyView : BasePage
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
            var mItem = new TB_DormReissueKey();
            mItem.ID = nID;
            Framework.Pager pager = null;
            var dt = bll.GetReissueKeyList(mItem, ref pager);
            if (DataTableHelper.IsEmptyDataTable(dt))
            {
                return;
            }

            var dr = dt.Rows[0];
            txtEmployeeNo.Text = dr["EmployeeNo"].ToString();
            txtCName.Text = dr["CName"].ToString();
            txtMobileNo.Text = dr["MobileNo"].ToString();

            txtDormAddress.Text = dr["DormAddress"].ToString();
            txtKeyTypes.Text = dr["KeyTypes"].ToString();
            txtMoney.Text = dr["Money"].ToString();
            var sDate = (dr["CreateDate"] == null) ? ""
                                                 : Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm");
            txtCreateDate.Text = sDate;

            txtReason.Value = dr["Reason"].ToString();
            txtMemo.Value = dr["Memo"].ToString();
        }
    }
}