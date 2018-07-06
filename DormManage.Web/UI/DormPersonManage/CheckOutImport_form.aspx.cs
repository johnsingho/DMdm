using DormManage.BLL.DormPersonManage;
using DormManage.Framework.LogManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.DormPersonManage
{
    public partial class CheckOutImport_form : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            UploadExcel(fulExcelFile);
        }

        protected void UploadExcel(FileUpload fileUpload)
        {
            btnDownFails.Enabled = false;
            if (string.IsNullOrEmpty(fileUpload.PostedFile.FileName))
            {
                ShowMessage("请选择待上传的文件", false);                
                return;
            }
            //save
            string fileName = SaveServerFile(fileUpload);
            if (string.IsNullOrEmpty(fileName))
            {
                ShowMessage("上传文件失败", false);
                return;
            }

            ImportToDB(fileName);
        }

        private void ImportToDB(string fileName)
        {
            var checkInBll = new EmployeeCheckInBLL();
            DataTable dtErr = null;
            int nSuccess = 0;
            if(checkInBll.CheckOutBatch(fileName, out nSuccess, out dtErr))
            {
                var str = string.Format("批量退房，{0}人成功", nSuccess);
                ShowMessage(str, true);
                hidFailXls.Value = "";
                return;
            }
            else
            {
                var nFailed = (dtErr == null) ? 0 : dtErr.Rows.Count;
                if (nFailed > 0)
                {
                    EmployeeCheckInBLL mEmployeeCheckInBLL = new EmployeeCheckInBLL();
                    string strFileName = mEmployeeCheckInBLL.Export(dtErr, "批量退房失败");
                    btnDownFails.Enabled = true;
                    hidFailXls.Value = strFileName;
                    //this.DownLoadFile(this.Request, this.Response, Path.GetFileName(strFileName), File.ReadAllBytes(strFileName), 10240000);
                }
                var str = string.Format("批量退房，{0}人成功，{1}人失败", nSuccess, nFailed);
                ShowMessage(str, false);
            }
        }

        private string SaveServerFile(FileUpload fnUpload)
        {
            string savePath = Path.Combine(Server.MapPath("..\\..\\"), "upload", DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(savePath))
            {
                //创建目录
                try
                {
                    Directory.CreateDirectory(savePath);
                }
                catch(Exception ex)
                {
                    LogManager.GetInstance().ErrorLog(ex.Message, ex);
                    return string.Empty;
                }
            }
            try
            {
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + fnUpload.FileName;
                string sFullPath = Path.Combine(savePath, newFileName);
                fnUpload.SaveAs(sFullPath);
                return sFullPath;
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message, ex);
                return string.Empty;
            }
        }

        private void ShowMessage(String str, bool bok)
        {
            lblResultMsgOk.Text = "";
            lblResultMsgErr.Text = "";
            if (bok)
            {
                lblResultMsgOk.Text = str;
            }
            else
            {
                lblResultMsgErr.Text = str;
            }
            //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Error", "alert('" + str + "')", true);
        }

        protected void btnDownFails_Click(object sender, EventArgs e)
        {
            var sFile = hidFailXls.Value.Trim();
            if (!string.IsNullOrEmpty(sFile))
            {
                this.DownLoadFile(this.Request, this.Response, Path.GetFileName(sFile), File.ReadAllBytes(sFile), 10240000);
            }
        }
    }
}