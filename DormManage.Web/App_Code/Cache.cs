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
using System.Collections.Generic;


/// <summary>
/// 缓存操作
/// Author: Eric Liu(Eric.Liu1@flextronics.com)
/// Date:2014-6-13
/// </summary>
public class Cache
{
    protected string strCacheName = "";//缓存名称
    protected string strCacheItemName = "";//缓存项目名称
    protected int intExpireTime = 0;//缓存过期时间,单位秒
    protected Object objCacheObject = new object();//缓存对象
    protected string strCacheKey = "";//缓存键名称
    protected string strLastUpdate = "";//缓存最后更新时间
    protected string strResult = "";//操作结果
    public Cache()
    {
        strCacheKey = strCacheItemName + "_" + strCacheName;
    }

    /// <summary>
    /// 应用在本系统中的缓存时间
    /// </summary>
    /// <param name="user"></param>
    /// <param name="CacheName"></param>
    public Cache(string userid, string CacheName)
    {
        intExpireTime = 3600;
        strCacheItemName = "DPIP_" + userid;
        strCacheName = CacheName;
        strCacheKey = strCacheItemName + "_" + strCacheName;
    }

    #region 设置/获得缓存项目

    /// <summary>
    /// 设置/获得缓存项目。
    /// </summary>

    public string CacheItemName
    {

        get
        {

            return strCacheItemName;

        }

        set
        {

            strCacheItemName = value;

            strCacheKey = strCacheItemName + "_" + strCacheName;

        }

    }

    #endregion

    #region 设置/获得缓存名称

    /// <summary>
    /// 设置/获得缓存名称。
    /// </summary>

    public string CacheName
    {

        get
        {

            return strCacheName;

        }

        set
        {

            strCacheName = value;

            strCacheKey = strCacheItemName + "_" + strCacheName;

        }

    }

    #endregion

    #region 设置缓存过期时间间隔

    /// <summary>
    /// 设置缓存过期时间间隔。
    /// </summary>

    public int ExpireTime
    {

        get
        {

            return intExpireTime;

        }

        set
        {

            intExpireTime = value;

        }

    }

    #endregion

    #region 获得缓存最后更新时间

    /// <summary>
    /// 获得缓存最后更新时间。
    /// </summary>

    public string getLastUpdatetime
    {

        get
        {

            if (System.Web.HttpContext.Current.Cache[strCacheKey + "_UpdateTime"] != null)
            {

                return System.Web.HttpContext.Current.Cache[strCacheKey + "_UpdateTime"].ToString();

            }

            else
            {

                return "";

            }

        }

    }

    #endregion

    #region 获得缓存过期时间

    /// <summary>
    /// 读取缓存过期时间。
    /// </summary>

    public string getLostTime
    {

        get
        {

            if (System.Web.HttpContext.Current.Cache[strCacheKey + "_LostDateTime"] != null)
            {

                return System.Web.HttpContext.Current.Cache[strCacheKey + "_LostDateTime"].ToString();

            }

            else
            {

                return "";

            }

        }

    }

    #endregion

    #region 保存对象到缓存中

    /// <summary>
    /// 保存对象到缓存中。
    /// </summary>

    public void SetCache(object objContent)
    {

        if (CheckParameter() == false) return;

        lock (objCacheObject)
        {

            DateTime Dt = DateTime.Now;

            System.Web.HttpContext.Current.Cache.Insert(strCacheKey, objContent, null, Dt.AddSeconds(intExpireTime), System.TimeSpan.Zero);

            System.Web.HttpContext.Current.Cache.Insert(strCacheKey + "_UpdateTime", Dt.ToString(), null, Dt.AddSeconds(intExpireTime), System.TimeSpan.Zero);

            System.Web.HttpContext.Current.Cache.Insert(strCacheKey + "_LostDateTime", Dt.AddSeconds(intExpireTime), null, Dt.AddSeconds(intExpireTime), System.TimeSpan.Zero);

        }

    }

    #endregion

    #region 从缓存中取出对象

    /// <summary>
    /// 从缓存中取出对象。
    /// </summary>

    public object GetCache()
    {

        if (CheckParameter() == false) return null;

        lock (objCacheObject)
        {

            if (System.Web.HttpContext.Current.Cache[strCacheKey] != null)
            {

                return System.Web.HttpContext.Current.Cache[strCacheKey];

            }

            else
            {

                return null;

            }

        }

    }

    #endregion

    #region 从缓存中清空对象

    /// <summary>
    /// 从缓存中清空对象。
    /// </summary>

    public void Clear()
    {

        if (CheckParameter() == false) return;

        lock (objCacheObject)
        {

            if (System.Web.HttpContext.Current.Cache[strCacheKey] != null)
            {

                System.Web.HttpContext.Current.Cache.Remove(strCacheKey);

            }

            if (System.Web.HttpContext.Current.Cache[strCacheKey + "_UpdateTime"] != null)
            {

                System.Web.HttpContext.Current.Cache.Remove(strCacheKey + "_UpdateTime");

            }

            if (System.Web.HttpContext.Current.Cache[strCacheKey + "_LostDateTime"] != null)
            {

                System.Web.HttpContext.Current.Cache.Remove(strCacheKey + "_LostDateTime");

            }

        }

    }

    #endregion

    #region 缓存对象是否有效

    /// <summary>
    /// 缓存对象是否有效。
    /// </summary>

    public bool ValidCache()
    {

        if (CheckParameter() == false) return false;

        lock (objCacheObject)
        {

            if (System.Web.HttpContext.Current.Cache[strCacheKey] == null)
            {

                return false;

            }

            else
            {

                return true;

            }

        }

    }

    #endregion

    #region 获得缓存操作结果

    /// <summary>
    /// 获得缓存操作结果。
    /// </summary>

    public string getResult
    {

        get
        {

            return strResult;

        }

    }

    #endregion

    #region 检查缓存参数

    /// <summary>
    /// 检查缓存参数。
    /// </summary>

    public bool CheckParameter()
    {

        if (strCacheItemName == "")
        {

            strResult = "缓存项目名称为空";

            return false;

        }

        if (strCacheName == "")
        {

            strResult = "缓存名称为空";

            return false;

        }

        return true;

    }

    #endregion

}

