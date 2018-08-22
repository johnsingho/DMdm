using DormManage.Common;
using System;
using System.Data;
using System.Web;

namespace DormManage.Web.UI.Common
{
    public partial class ExcelDownHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/html; charset=UTF-8";
            var action = Request.QueryString["action"];

            switch (action)
            {
                case TypeManager.SESSIONKEY_ImpErrCheckIn:
                    DownImpError(action, "入住记录导入失败");
                    break;
                case TypeManager.SESSIONKEY_ImpErrAllowanceApply:
                    DownImpError(action, "住房津贴导入失败");
                    break;
                case TypeManager.SESSIONKEY_ImpErrAllowanceAppCancel:
                    DownImpError(action, "住房津贴取消导入失败");
                    break;
                case TypeManager.SESSIONKEY_ImpErrCharing:
                    DownImpError(action, "扣费记录导入失败");
                    break;
                default:
                    break;
            }
    }

        //下载二进制数为文件
        protected void DownloadBinData(HttpContext cont, byte[] bys, string fn)
        {
            if (null == bys || 0 == bys.Length) { return; }
            //var sCont = string.Format("attachment; filename = {0}", HttpUtility.UrlEncode(fn));
            fn = fn.Replace(' ', '-');
            var sCont = string.Format("attachment; filename = {0}",
                                    HttpUtility.UrlEncode(fn, System.Text.Encoding.UTF8));
            var resp = cont.Response;
            resp.Clear();
            resp.Charset = "utf-8";
            resp.ContentType = "Application/octet-stream";
            resp.AppendHeader("Content-Disposition", sCont);
            resp.BinaryWrite(bys);
            resp.End(); //
        }
        
        private void DownImpError(string type, string name)
        {
            var context = HttpContext.Current;
            DataTable dt = SessionHelper.Get(context, type) as DataTable;

            if (null == dt || 0 == dt.Rows.Count)
            {
                context.Response.Write("没有失败记录待下载！");
                context.Response.End(); //
                return;
            }
            var bys = ExcelHelper.BuilderExcel(dt);
            var fn = string.Format("{0:yyyyMMddHHmmss_}{1}.xlsx", DateTime.Now, name);
            DownloadBinData(context, bys, fn);
        }


    }
}