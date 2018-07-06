using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.AssignRoom;
using DormManage.BLL.DormPersonManage;
using DormManage.Framework;
using DormManage.Models;

namespace DormManage.Web.UI.AssignRoom
{
    public partial class AssignRecord : BasePage
    {
        private void Bind(int intCurrentIndex)
        {
            TB_AssignRoom mTB_AssignRoom = new TB_AssignRoom();
            AssignRoomBLL mAssignRoomBLL = new AssignRoomBLL();
            Pager pager = new Pager();
            DataTable dtSource = null;
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            dtSource = mAssignRoomBLL.GetPagerData(ref pager, this.txtCardNo.Text.Trim(), this.ddlStatus.SelectedValue);
            GridView1.DataSource = dtSource;
            GridView1.DataBind();

            this.Pager1.ItemCount = pager.TotalRecord;
            this.Pager1.PageCount = pager.TotalPage;
            this.Pager1.CurrentIndex = pager.CurrentPageIndex;
            this.Pager1.PageSize = pager.PageSize;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Bind(1);
            }
        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Button btnOperate = e.Row.FindControl("btnOperate") as Button;
                Button btnCancelCheckIn = e.Row.FindControl("btnCancelCheckIn") as Button;
                CheckBox chkBox = e.Row.FindControl("chkLeftSingle") as CheckBox;
                btnOperate.CommandArgument = drv["ID"] + "|" + drv["BedID"] + "|" + drv["EmployeeCheckInID"];
                btnCancelCheckIn.CommandArgument = drv["ID"] + "|" + drv["BedID"] + "|" + drv["EmployeeCheckInID"];
                if (String.IsNullOrEmpty(drv["CardNo"].ToString()))
                {
                    btnOperate.Text = "解除锁定";
                    btnCancelCheckIn.Visible = false;
                }
                else
                {
                    btnOperate.Text = "确认入住";
                    chkBox.Enabled = false;
                }
            }
        }

        protected void btnOperate_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int intID = Convert.ToInt32(btn.CommandArgument.Split('|')[0]);
            int result;
            int.TryParse(btn.CommandArgument.Split('|')[1], out result);
            int intBedId = result;
            int.TryParse(btn.CommandArgument.Split('|')[2], out result);
            int intEmployeeCheckInID = result;
            if (btn.Text == "解除锁定")
            {
                new AssignRoomBLL().Remove(intID);
            }
            else if (btn.Text == "确认入住")
            {
                new AssignRoomBLL().ConfirmCheckIn(intID, intBedId, intEmployeeCheckInID);
            }
            this.Bind(1);
        }

        /// <summary>
        /// 取消入住
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelCheckIn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int intID = Convert.ToInt32(btn.CommandArgument.Split('|')[0]);
            int intBedID = Convert.ToInt32(btn.CommandArgument.Split('|')[1]);
            int intEmployeeCheckInID = Convert.ToInt32(btn.CommandArgument.Split('|')[2]);
            new AssignRoomBLL().CancelCheckIn(intID, intBedID, intEmployeeCheckInID);
            this.Bind(1);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bind(1);
        }

        protected void btnAllLock_Click(object sender, EventArgs e)
        {
            List<int> lstID = new List<int>();
            //遍历找出所有被选中的行
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)this.GridView1.Rows[i].Cells[0].FindControl("chkLeftSingle");
                //被选中
                if (chk.Checked && chk.Visible == true)
                {
                    //lstID.Add(Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString()));
                    new AssignRoomBLL().Remove(Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString()));
                }
            }
            this.Bind(1);
            //if (new AssignRoomBLL().Add(lstID))
            //{
            //    this.Bind(1);
            //}


        }
    }
}