using DormManage.BLL.FlexPlus;
using DormManage.Common;
using DormManage.Models;
using System;

namespace DormManage.Web.UI.FlexPlus
{
    public partial class ApplyDormView : BasePage
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
            var mTB_DormAreaApply = new TB_DormAreaApply();
            mTB_DormAreaApply.ID = nID;
            Framework.Pager pager = null;
            var dt = bll.GetApplyDorms(mTB_DormAreaApply, ref pager);
            if (DataTableHelper.IsEmptyDataTable(dt))
            {
                return;
            }
            int nTemp = 0;
            var dr = dt.Rows[0];
            txtEmployeeNo.Text = dr["EmployeeNo"].ToString();
            txtCName.Text = dr["CName"].ToString();

            nTemp = 0;
            int.TryParse(dr["Sex"].ToString(), out nTemp);
            txtSex.Text = nTemp > 0 ? "男" : "女";

            txtCardNo.Text = dr["CardNo"].ToString();
            txtCreateDate.Text = dr["CreateDate"].ToString();
            txtDormArea.Text = dr["DormArea"].ToString();
            txtGrade.Text = dr["Grade"].ToString();
            nTemp = 0;
            int.TryParse(dr["HasHousingAllowance"].ToString(), out nTemp);
            txtHasHousingAllowance.Text = nTemp>0 ? "是" : "否";

            txtmemo.Text = dr["memo"].ToString();
            txtMobileNo.Text = dr["MobileNo"].ToString();
            txtRequireReason.Text = dr["RequireReason"].ToString();

            nTemp = 0;
            int.TryParse(dr["RequireType"].ToString(), out nTemp);
            var sTemp = string.Empty;
            switch (nTemp)
            {
                case 2:
                    sTemp = "复入住";
                    break;
                case 3:
                    sTemp = "调房";
                    break;
                default:
                    sTemp = "新入住";
                    break;
            }
            txtRequireType.Text = sTemp;

        }
    }
}