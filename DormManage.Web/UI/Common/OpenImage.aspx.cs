using DormManage.BLL.FlexPlus;
using DormManage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.Common
{
    public partial class OpenImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string batchNo = Request.Params["batchNo"];
            InitData(batchNo);
        }

        private void InitData(string batchNo)
        {
            var bll = new FlexPlusBLL();
            var dt = bll.GetRepairDormImage(batchNo);
            if (DataTableHelper.IsEmptyDataTable(dt))
            {
                return;
            }

            rp_Item.DataSource = dt;
            rp_Item.DataBind();
        }
    }
}