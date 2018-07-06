using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage.BLL.AssignRoom;
using DormManage.BLL.DormManage;
using DormManage.Framework;
using DormManage.Models;
using DormManage.Web;

namespace DormManage.Web.UI.Report
{
    public partial class MonthDormInfo : BasePage
    {

        #region 私有方法
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="intCurrentIndex"></param>
        private void Bind(int intCurrentIndex)
        {
            TB_Room mTB_Room = new TB_Room();
            RoomBLL mRoomBLL = new RoomBLL();
            Pager pager = new Pager();
            pager.CurrentPageIndex = intCurrentIndex;
            pager.srcOrder = "  ID desc";

            mTB_Room.SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID);
            mTB_Room.DormAreaID = Convert.ToInt32(this.ddlDormArea.SelectedValue);
            mTB_Room.BuildingID = Convert.ToInt32(Request.Form[this.ddlBuildingName.UniqueID.ToString()]);
            mTB_Room.UnitID = Convert.ToInt32(Request.Form[this.ddlUnit.UniqueID.ToString()]);
            mTB_Room.FloorID = Convert.ToInt32(Request.Form[this.ddlFloor.UniqueID.ToString()]);

            DataTable dt = mRoomBLL.GetMonthDormInfoBySiteID(2, txtStartDay.Text);

            if(dt!=null)
            {
                GridView1.Columns.Clear();//首先清空要显示的列名称（每次绑定时重新填充）
                BoundField bf = new BoundField();
                bf.DataField = dt.Columns[0].ColumnName;
                bf.HeaderText = "宿舍区";
                this.GridView1.Columns.Add(bf);

                bf = new BoundField();
                bf.DataField = dt.Columns[1].ColumnName;
                bf.HeaderText = "入住级别";
                this.GridView1.Columns.Add(bf);

                bf = new BoundField();
                bf.DataField = "project";
                bf.HeaderText = "项目";
                this.GridView1.Columns.Add(bf);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName != "Areaname" && dt.Columns[i].ColumnName != "Roomtypt" && dt.Columns[i].ColumnName != "project")
                    {
                        bf = new BoundField();
                        bf.DataField = dt.Columns[i].ColumnName;
                        bf.HeaderText = dt.Columns[i].ColumnName;
                        this.GridView1.Columns.Add(bf);//只能添加BoundField 对象
                    }

                }

                DataView dw = dt.DefaultView;

                dw.Sort = "Areaname,Roomtypt desc";

                GridView1.DataSource = dw;// GetUnLockRoom(mTB_Room, ref pager);
                GridView1.DataBind();
                MergeRow(GridView1, 0, 3);
            }
            

        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void ddlBind()
        {
            Pager mPager = null;
            #region 宿舍区
            DormAreaBLL mDormAreaBLL = new DormAreaBLL();
            this.ddlDormArea.DataValueField = TB_DormArea.col_ID;
            this.ddlDormArea.DataTextField = TB_DormArea.col_Name;

            this.ddlDormArea.DataSource = mDormAreaBLL.GetTable(new TB_DormArea() { SiteID = (base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID) }, ref mPager);
            this.ddlDormArea.DataBind();
            this.ddlDormArea.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼栋
            this.ddlBuildingName.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 单元
            this.ddlUnit.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            #region 楼层
            this.ddlFloor.Items.Insert(0, new ListItem() { Value = "0", Text = "--请选择--" });
            #endregion
            
        }
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            ClientScript.RegisterForEventValidation(this.ddlBuildingName.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlUnit.UniqueID, "argument");
            ClientScript.RegisterForEventValidation(this.ddlFloor.UniqueID, "argument");
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DormManageAjaxServices));
            if (!IsPostBack)
            {
                txtStartDay.Text = System.DateTime.Now.ToString("yyyy-MM");
                this.ddlBind();
                //this.Bind(1);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bind(1);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                //保持列不变形  
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    //方法一：  
                    e.Row.Cells[i].Text = "&nbsp;" + e.Row.Cells[i].Text + "&nbsp;";
                    e.Row.Cells[i].Wrap = false;
                    //方法二：  
                    //e.Row.Cells[i].Text = "<nobr>&nbsp;" + e.Row.Cells[i].Text + "&nbsp;</nobr>";              
                }
            }
        }

        protected void pagerList_Command(object sender, CommandEventArgs e)
        {
            this.Bind(Convert.ToInt32(e.CommandArgument));
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
                    lstID.Add(Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString()));
                }
            }
            if (new AssignRoomBLL().Add(lstID))
            {
                this.Bind(1);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            this.ToEXCELByGridView(GridView1, "宿舍入住基本信息");
        }
    }
}