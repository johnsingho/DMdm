using System.Web;
using System.Web.SessionState;

namespace DormManage.Common
{

    /// <summary>
    /// SessionHelper
    /// asp.net session visit
    /// By H.Z.XIN
    /// Modified:
    ///     2018-08-22 整理
    /// </summary>
    public class SessionHelper
    {
        public static void Set(string key, object obj)
        {
            HttpContext rq = HttpContext.Current;
            rq.Session[key] = obj;
        }

        public static object Get(string key)
        {
            HttpContext rq = HttpContext.Current;
            return rq.Session[key];
        }

        public static void Set(HttpContext cont, string key, object obj)
        {
            if (null != cont)
            {
                cont.Session[key] = obj;
            }
        }

        public static object Get(HttpContext cont, string key)
        {
            if (null != cont)
            {
                return cont.Session[key];
            }
            return null;
        }
    }

}