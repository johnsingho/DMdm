using System.Web;

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
}