using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
      
public class Utility
{
    private static String _webRoot = null;

    /// <summary>
    /// 获取网站的根路径
    /// </summary>
    public static String WebRoot
    {
        get
        {
            if (_webRoot == null)
            {
                _webRoot = HttpContext.Current.Request.ApplicationPath;
                if (_webRoot.Length > 0 && _webRoot.EndsWith("/"))
                {
                    _webRoot = _webRoot.Substring(0, _webRoot.Length - 1);
                }
            }

            return _webRoot;
        }
    }

    /// <summary>
    /// 获取Excel模板的根路径。
    /// </summary>
    public static String ExcelTemplateRoot
    {
        get
        {
            return WebRoot + "/FileTemplate/";
        }
    }
}