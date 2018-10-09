using DormManage.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DormManage;
using DormManage.Data.DAL;

namespace DormManage.Web
{

    /// <summary>
    /// 2018-07-02 用来提供给HR上传新IDL员工信息的
    /// 数据保存在 TB_LongEmployee
    /// </summary>
    public partial class UpdateEmpInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string savePath = Path.Combine(Server.MapPath(".\\"), "upload", DateTime.Now.ToString("yyyyMM"));
            string newFileName = string.Empty;
            if (!Directory.Exists(savePath))
            {
                //创建目录
                try
                {
                    Directory.CreateDirectory(savePath);
                }
                catch
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>alert('创建上传目录失败！')</script>");
                }
            }
            try
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + fileUpload.FileName;
                var tarFile = Path.Combine(savePath, newFileName);
                fileUpload.SaveAs(tarFile);

                ImpFile(tarFile);
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>alert('文件上传失败！')</script>");
            }
        }

        private void ShowMsg(string smsg)
        {
            var str = string.Format("<script>alert('{0}')</script>", smsg);
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", str);
        }

        private void ImpFile(string xlsFile)
        {
            lblSuccess.Text = "";
            lblError.Text = "";

            //读取Excel内容
            var _mExcelHelper = new ExcelHelper();
            var dt = _mExcelHelper.GetDataFromExcelBreakEmptyLine(xlsFile);
            if (null == dt)
            {
                ShowMsg("读取文件数据失败！");
                return;
            }

            try
            {
                dt.Columns["ENAME"].ColumnName = "English_Name";
                dt.Columns["Employee_type"].ColumnName = "EmployeeTypeName";
                dt.Columns.Remove("Employee");
                if (null == dt.Columns["IDCardNumber"])
                {
                    dt.Columns.Add("IDCardNumber");
                }
                if (null == dt.Columns["SrcImport"])
                {
                    dt.Columns.Add("SrcImport"); //0-GBSHR import, 1--multek import
                }                
            }
            catch (Exception)
            {
                lblError.Text = "Excel列名不匹配";
                return;
            }

            var tarTable = "TB_LongEmployeeTemp";
            var db = DBO.GetInstance();

            //delete old
            var commSql = db.DbProviderFactory.CreateCommand();
            commSql.CommandType = System.Data.CommandType.Text;
            commSql.CommandText = "delete from TB_LongEmployeeTemp";
            db.ExecuteNonQuery(commSql);

            //bulk insert
            var sErr = DataTableHelper.BulkToDB(db.CreateConnection().ConnectionString, 
                                                dt, 
                                                tarTable);
            if (!string.IsNullOrEmpty(sErr))
            {
                lblError.Text = sErr;
                return;
            }
            else
            {
                lblError.Text = string.Empty;
            }

            //merge
            var comm = db.DbProviderFactory.CreateCommand();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "Proc_Merge_LongEmployee";
            try
            {
                db.ExecuteNonQuery(comm);
                lblSuccess.Text = "上传完成！";
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}