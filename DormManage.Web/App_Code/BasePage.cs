using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Text;
using System.IO;
using System.Threading;
using DormManage.Common;
using System.Collections.Generic;
using DormManage.Models;

public class BasePage : Page
{
    public BasePage()
    {

    }

    /// <summary>
    /// 登陆用户信息
    /// </summary>
    public TB_User UserInfo
    {
        get
        {
            if (null != Session[TypeManager.User])
            {
                return (TB_User)Session[TypeManager.User];
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 登陆超级管理员信息
    /// </summary>
    public TB_SystemAdmin SystemAdminInfo
    {
        get
        {
            if (null != Session[TypeManager.Admin])
            {
                return (TB_SystemAdmin)Session[TypeManager.Admin];
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 初始化处理
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        if (HttpContext.Current != null && Context.Session != null)
        {
            if (Session[TypeManager.User] == null && Session[TypeManager.Admin] == null)
            {
                Session.Abandon();
                Response.Write("<script>top.location.href='" + GetUrl() + "Login.aspx'</script>");
            }
            base.OnInit(e);
        }
    }

    /// <summary>
    /// 退出
    /// </summary>
    protected void Logoff(string target = "Login.aspx")
    {
        Session.Abandon();
        Response.Write("<script>top.location.href='" + GetUrl() + target + "'</script>");
    }

    protected virtual void AddStyle(string cssPath)
    {
        Literal ltlCss = new Literal();
        ltlCss.Text = "<link rel='stylesheet' href='" + GetUrl() + cssPath + "' type='text/css' />";
        Page.Header.Controls.Add(ltlCss);

        this.Page.Header.Controls.Add(ltlCss);
    }

    protected virtual void AddStyle(string[] cssPath)
    {
        for (int i = 0; i < cssPath.Length; i++)
        {
            if (cssPath[i] != null)
                AddStyle(cssPath[i]);
        }
    }

    /// <summary>
    /// 获取页面的相对位置
    /// </summary>
    /// <returns></returns>
    protected string GetUrl()
    {
        string root = Server.MapPath("~/");
        string current = Server.MapPath("./");
        current = current.Replace(root, "");
        string[] temp = current.Split('\\');
        string result = "";
        for (int i = 0; i < temp.Length - 1; i++)
        {
            result += "../";
        }
        return result;
    }

    /// <summary>
    /// 描  述：截取字符串长度 
    /// 作  者：
    /// 时  间：
    /// 修  改：
    /// 原  因：
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="lenght">长度</param>
    /// <returns></returns>
    public static string GetStringSub(string str, int lenght)
    {
        if (str != null && str.Length > 1)
        {
            if (str.Length > lenght)
            {
                return str.Substring(0, lenght) + "...";
            }
            else
            {
                return str;
            }
        }
        else
        {
            return str;
        }
    }

    /// <summary>
    /// 描  述：将ID转为A,B,C格式
    /// 作  者：
    /// 时  间：
    /// 修  改：
    /// 原  因：
    /// </summary>
    /// <param name="lst"></param>
    /// <returns></returns>
    protected string ReturnStringIDs(List<int> lst)
    {
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < lst.Count; i++)
        {
            if (i == 0)
            {
                strBuilder.Append(lst[i].ToString());
            }
            else
            {
                strBuilder.Append(",");
                strBuilder.Append(lst[i].ToString());
            }
        }
        return strBuilder.ToString();
    }

    /// <summary>
    /// 扩展GridView外观（鼠标经过，离开，单击行的颜色设定）
    /// </summary>
    /// <param name="e">当前行</param>
    /// <param name="hiddenName">隐藏控件名</param>
    /// <param name="gdvName">所属GridView名</param>
    public void ExtendGridview(GridViewRowEventArgs e, string hiddenName, string gdvName)
    {
        Boolean bolClickNoPostback = true;
        if (e.Row.RowIndex != -1)
        {
            ////鼠标停留颜色设定
            //e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;if (currentcolor !='#c0c0ff'){this.style.backgroundColor='#EFF2F7';}");
            ////鼠标离开颜色设定
            //e.Row.Attributes.Add("onmouseout", "currentcolor=this.style.backgroundColor;if(currentcolor != '#c0c0ff'){this.style.backgroundColor='#ffffff';}");
            //鼠标停留颜色设定       修改光棒效果
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#66CCFF'");
            //鼠标离开颜色设定
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //鼠标单击颜色设定（可选项）
            if (bolClickNoPostback && !String.IsNullOrEmpty(hiddenName) && !String.IsNullOrEmpty(gdvName))
            {
                StringBuilder stbGdv = new StringBuilder();
                //'''' 如果隐藏文本内容不为空，说明用户已经第二次单击，设定当前单击的行颜色同时，取消上次单机行颜色
                stbGdv.Append("if(document.getElementById('");
                stbGdv.Append(hiddenName);
                stbGdv.Append("').value !=''");
                stbGdv.Append(" && document.getElementById('");
                stbGdv.Append(hiddenName);
                stbGdv.Append("').value !=");
                stbGdv.Append(e.Row.RowIndex.ToString());
                stbGdv.Append(")");
                //'''' 取消上次单击颜色
                stbGdv.Append("{document.getElementById('");
                stbGdv.Append(gdvName);
                stbGdv.Append("').rows(Number(document.getElementById('");
                stbGdv.Append(hiddenName);
                stbGdv.Append("').value)+1).style.backgroundColor=c;}");
                //'''' 设定本次单击行颜色
                stbGdv.Append("currentcolor=this.style.backgroundColor;this.style.backgroundColor='#c0c0ff';");
                stbGdv.Append("document.getElementById('");
                stbGdv.Append(hiddenName);
                stbGdv.Append("').value =");
                stbGdv.Append(e.Row.RowIndex.ToString());
                stbGdv.Append(";");
                e.Row.Attributes.Add("onclick", stbGdv.ToString());
            }
        }
    }

    /// <summary>
    /// 描  述：下载任意文件
    /// 作  者：
    /// 时  间：
    /// 修  改：
    /// 原  因：
    /// </summary>
    /// <param name="_Request"></param>
    /// <param name="_Response"></param>
    /// <param name="_fileName"></param>
    /// <param name="myFileStream">文件流</param>
    /// <param name="_speed">下载速度</param>
    /// <returns></returns>
    public bool DownLoadFile(HttpRequest _Request, HttpResponse _Response, string _fileName, byte[] bytes, long _speed)
    {
        try
        {
            if (bytes == null)
            {
                return false;
            }
            Stream myFileStream = new MemoryStream(bytes);
            BinaryReader br = new BinaryReader(myFileStream);
            string strUserAgent = Request.ServerVariables["http_user_agent"].ToLower();
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFileStream.Length;
                long startBytes = 0;

                double pack = 10240; //10K bytes
                //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    //Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength-1, fileLength));
                }

                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";

                if (strUserAgent.ToLower().IndexOf("firefox") == TypeManager.IntDefault)
                {
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));
                }
                else
                {
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + _fileName);
                }
                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
                //return false;
            }
            finally
            {
                br.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    public void downloadfileEX(string s_path)
    {
        System.IO.FileInfo file = new System.IO.FileInfo(s_path);
        HttpContext.Current.Response.ContentType = "application/ms-download";
        HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
        HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
        HttpContext.Current.Response.WriteFile(file.FullName);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.Clear();
        //if (File.Exists(s_path))
        //    File.Delete(s_path);
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 描  述：上传文件
    /// 作  者：
    /// 时  间：
    /// 修  改：
    /// 原  因：
    /// </summary>
    /// <param name="fileUpload"></param>
    /// <returns></returns>
    public bool UpLoadFile(FileUpload fileUpload, string strUpLoadDir, out string strFilePath)
    {
        bool bolResult = false;
        System.Guid guid = Guid.NewGuid();
        guid.ToString();
        string strUpFilePath = fileUpload.PostedFile.FileName;
        strFilePath = string.Empty;
        try
        {
            FileInfo fileInfo = new FileInfo(strUpFilePath);
            string strNewFileName = guid.ToString() + "_" + fileInfo.Name;
            string strNewFilePath = strUpLoadDir + strNewFileName;
            fileUpload.SaveAs(strNewFilePath);
            strFilePath = strNewFilePath;
            bolResult = true;
        }
        catch
        {
            bolResult = false;
        }
        return bolResult;
    }

    public void ShowMsg(string msg)
    {
        string sScript = "";

        sScript = "<script language=\"javascript\" type=\"text/javascript\"> ";
        sScript = sScript + " alert('" + msg + "');";
        sScript = sScript + " </script>";
        Response.Write(sScript);
    }

    public void RunScript(string sKey, string sScript)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), sKey, sScript, true);
    }
    public void RunScript(Control control, string sKey, string sScript)
    {
        ScriptManager.RegisterClientScriptBlock(control, this.GetType(), sKey, sScript, true);
    }    

    ///描述：将纯文本的SQL脚本导出为文件
    ///作者：
    ///时间：
    ///修改：
    ///原因：
    public void ExportSQLFile(string filename, string data)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/ms-txt";

        System.IO.StringWriter writer = new System.IO.StringWriter();
        writer.Write(data);

        Response.Write(writer.ToString());
        Response.End();
    }

    /// <summary>
    /// 导出GridView到EXCEL表格
    /// +1次重载
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="FileName"></param>
    public void ToEXCELByGridView(GridView ctl, string FileName, ArrayList al)
    {

        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        Page page = new Page();
        HtmlForm form = new HtmlForm();

        ctl.EnableViewState = false;

        page.EnableEventValidation = false;

        page.DesignerInitialize();

        page.Controls.Add(form);
        form.Controls.Add(ctl);

        page.RenderControl(htw);

        if (al.Count > 0)
        {
            foreach (GridViewRow gvr in ctl.Rows)
            {
                for (Int32 i = 0; i < al.Count; i++)
                {
                    //解决自动科学计数法
                    gvr.Cells[Convert.ToInt32(al[i])].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
                }
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8) + ".xls");
        Response.Charset = "gb2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(sb.ToString());
        Response.End();

    }

    /// <summary>
    /// 导出GridView到EXCEL表格
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="FileName"></param>
    public void ToEXCELByGridView(GridView ctl, string FileName)
    {

        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        Page page = new Page();
        HtmlForm form = new HtmlForm();

        ctl.EnableViewState = false;

        page.EnableEventValidation = false;

        page.DesignerInitialize();

        page.Controls.Add(form);
        form.Controls.Add(ctl);

        page.RenderControl(htw);

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8) + ".xls");
        //Response.Charset = "UTF-8";
        //Response.ContentEncoding = Encoding.UTF8;

        //屈政斌修改 中文乱码
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Charset = "GB2312";


        Response.Write(sb.ToString());
        Response.End();


    }

    /// <summary>
    /// 导出数据集到Excel表格
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="ds"></param>
    /// <param name="outPutExcelHeight"></param>
    /// <param name="outPutExcelWidth"></param>
    public void ToEXCELByDataSet(string FileName, DataSet ds, int outPutExcelHeight, int outPutExcelWidth)
    {
        //调用ToEXCELByDataTabel()方法
        DataTable dt = (DataTable)ds.Tables[0];
        ToEXCELByDataTabel(FileName, dt, outPutExcelHeight, outPutExcelWidth);
    }

    /// <summary>
    /// 导出数据集到Excel表格
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="dt"></param>
    /// <param name="outPutExcelHeight"></param>
    /// <param name="outPutExcelWidth"></param>
    public void ToEXCELByDataTabel(string FileName, DataTable dt, int outPutExcelHeight, int outPutExcelWidth)
    {
        GridView gd = new GridView();
        gd.Width = outPutExcelWidth;
        gd.Height = outPutExcelHeight;
        gd.DataSource = dt;
        gd.DataBind();
        //不允许分页，为打印方便
        gd.AllowPaging = false;
        //排序不允许
        gd.AllowSorting = false;
        //调用公用方法
        ToEXCELByGridView(gd, FileName);
        //废弃不用的控件
        this.Controls.Remove(gd);
    }

    /// <summary>
    /// 获取汉字拼音首字母
    /// </summary>
    /// <param name="unicodeString"></param>
    /// <returns></returns>
    public string GetPinyinCode(string unicodeString)
    {
        int i = 0;
        ushort key = 0;
        string strResult = string.Empty;

        //创建两个不同的encoding对象
        Encoding unicode = Encoding.Unicode;
        Encoding gbk = Encoding.GetEncoding(936);

        //将unicode字符串转换为字节,再转化为GBK码
        byte[] unicodeBytes = unicode.GetBytes(unicodeString);
        byte[] gbkBytes = Encoding.Convert(unicode, gbk, unicodeBytes);

        while (i < gbkBytes.Length)
        {
            //如果为数字\字母\其他ASCII符号
            if (gbkBytes[i] <= 127)
            {
                strResult = strResult + (char)gbkBytes[i];
                i++;
            }
            else
            {
                key = (ushort)(gbkBytes[i] * 256 + gbkBytes[i + 1]);
                if (key >= '\uB0A1' && key <= '\uB0C4')
                {
                    strResult = strResult + "A";
                }
                else if (key >= '\uB0C5' && key <= '\uB2C0')
                {
                    strResult = strResult + "B";
                }
                else if (key >= '\uB2C1' && key <= '\uB4ED')
                {
                    strResult = strResult + "C";
                }
                else if (key >= '\uB4EE' && key <= '\uB6E9')
                {
                    strResult = strResult + "D";
                }
                else if (key >= '\uB6EA' && key <= '\uB7A1')
                {
                    strResult = strResult + "E";
                }
                else if (key >= '\uB7A2' && key <= '\uB8C0')
                {
                    strResult = strResult + "F";
                }
                else if (key >= '\uB8C1' && key <= '\uB9FD')
                {
                    strResult = strResult + "G";
                }
                else if (key >= '\uB9FE' && key <= '\uBBF6')
                {
                    strResult = strResult + "H";
                }
                else if (key >= '\uBBF7' && key <= '\uBFA5')
                {
                    strResult = strResult + "J";
                }
                else if (key >= '\uBFA6' && key <= '\uC0AB')
                {
                    strResult = strResult + "K";
                }
                else if (key >= '\uC0AC' && key <= '\uC2E7')
                {
                    strResult = strResult + "L";
                }
                else if (key >= '\uC2E8' && key <= '\uC4C2')
                {
                    strResult = strResult + "M";
                }
                else if (key >= '\uC4C3' && key <= '\uC5B5')
                {
                    strResult = strResult + "N";
                }
                else if (key >= '\uC5B6' && key <= '\uC5BD')
                {
                    strResult = strResult + "O";
                }
                else if (key >= '\uC5BE' && key <= '\uC6D9')
                {
                    strResult = strResult + "P";
                }
                else if (key >= '\uC6DA' && key <= '\uC8BA')
                {
                    strResult = strResult + "Q";
                }
                else if (key >= '\uC8BB' && key <= '\uC8F5')
                {
                    strResult = strResult + "R";
                }
                else if (key >= '\uC8F6' && key <= '\uCBF9')
                {
                    strResult = strResult + "S";
                }
                else if (key >= '\uCBFA' && key <= '\uCDD9')
                {
                    strResult = strResult + "T";
                }
                else if (key >= '\uCDDA' && key <= '\uCEF3')
                {
                    strResult = strResult + "W";
                }
                else if (key >= '\uCEF4' && key <= '\uD188')
                {
                    strResult = strResult + "X";
                }
                else if (key >= '\uD1B9' && key <= '\uD4D0')
                {
                    strResult = strResult + "Y";
                }
                else if (key >= '\uD4D1' && key <= '\uD7F9')
                {
                    strResult = strResult + "Z";
                }
                else
                {
                    strResult = strResult + "?";
                }
                i = i + 2;
            }
        }

        return strResult;
    }

    /// <summary>
    /// 使用此方法必须设置grideview的DataKeyNames属性为主键
    /// </summary>
    /// <param name="mGridView">gridview</param>
    /// <param name="strChkID">CheckBox ID</param>
    /// <returns>选中的记录主键ID</returns>
    protected string GetCheckedIDs(ExtendGridView.GridView mGridView, string strChkID)
    {
        string strID = string.Empty;
        List<int> lstID = new List<int>();
        //遍历找出所有被选中的行
        for (int i = 0; i < mGridView.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)mGridView.Rows[i].Cells[0].FindControl(strChkID);
            //被选中
            if (chk.Checked && chk.Visible == true)
            {
                lstID.Add(Convert.ToInt32(mGridView.DataKeys[i].Value.ToString()));
            }
        }
        strID = this.ReturnStringIDs(lstID);
        return strID;
    }

    /// <summary>
    /// GridView合并行，
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="startCol">开始列</param>
    /// <param name="endCol">结束列</param>
    public static void MergeRow(GridView gv, int startCol, int endCol)
    {
        RowArg init = new RowArg()
        {
            StartRowIndex = 0,
            EndRowIndex = gv.Rows.Count - 2
        };
        for (int i = startCol; i < endCol + 1; i++)
        {
            if (i > 0)
            {
                List<RowArg> list = new List<RowArg>();
                //从第二列开始就要遍历前一列
                TraversesPrevCol(gv, i - 1, list);
                foreach (var item in list)
                {
                    MergeRow(gv, i, item.StartRowIndex, item.EndRowIndex);
                }
            }
            //合并开始列的行
            else
            {
                MergeRow(gv, i, init.StartRowIndex, init.EndRowIndex);
            }
        }
    }

    /// <summary>
    /// 遍历前一列
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="prevCol">当前列的前一列</param>
    /// <param name="list"></param>
    private static void TraversesPrevCol(GridView gv, int prevCol, List<RowArg> list)
    {
        if (list == null)
        {
            list = new List<RowArg>();
        }
        RowArg ra = null;
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            if (!gv.Rows[i].Cells[prevCol].Visible)
            {
                continue;
            }
            ra = new RowArg();
            ra.StartRowIndex = gv.Rows[i].RowIndex;
            ra.EndRowIndex = ra.StartRowIndex + gv.Rows[i].Cells[prevCol].RowSpan - 2;
            list.Add(ra);
        }
    }

    /// <summary>
    /// 合并单列的行
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="currentCol">当前列</param>
    /// <param name="startRow">开始合并的行索引</param>
    /// <param name="endRow">结束合并的行索引</param>
    private static void MergeRow(GridView gv, int currentCol, int startRow, int endRow)
    {
        for (int rowIndex = endRow; rowIndex >= startRow; rowIndex--)
        {
            GridViewRow currentRow = gv.Rows[rowIndex];
            GridViewRow prevRow = gv.Rows[rowIndex + 1];
            if (currentRow.Cells[currentCol].Text != "" && currentRow.Cells[currentCol].Text != " ")
            {
                if (currentRow.Cells[currentCol].Text == prevRow.Cells[currentCol].Text)
                {
                    currentRow.Cells[currentCol].RowSpan = prevRow.Cells[currentCol].RowSpan < 1 ? 2 : prevRow.Cells[currentCol].RowSpan + 1;
                    prevRow.Cells[currentCol].Visible = false;
                }
            }
        }
    }

    class RowArg
    {
        public int StartRowIndex { get; set; }
        public int EndRowIndex { get; set; }
    }

}
