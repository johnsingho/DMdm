using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace DormManage.Framework.LogManager
{
    public sealed class LogManager
    {
        //private static readonly ILog _Logger = log4net.LogManager.GetLogger("logger-name");
        private static readonly ILog _LoggerError = log4net.LogManager.GetLogger("logerror");
        private static readonly ILog _LoggerDebug = log4net.LogManager.GetLogger("loginfo");
        private static readonly LogManager instance = new LogManager();

        private LogManager()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public static LogManager GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="ex"></param>
        public void ErrorLog(string sErr)
        {
            // 直接调用本类方法，而不操作 log4net，这样抛异常时，可以只修改一个方法即可
            ErrorLog(sErr, null);
        }

        /// <summary>
        /// 写调试日志，方便调试
        /// </summary>
        /// <param name="ex"></param>
        public void DebugLog(string ex)
        {
            // 直接调用本类方法，而不操作 log4net，这样抛异常时，可以只修改一个方法即可
            DebugLog(ex, null);
        }

        public void ErrorLog(string strMsg, Exception e)
        {
            //这里为了规范代码，并且方便调试，因此如下写法，将来必须重写
            //throw new Exception();
            if (e != null)
            {
                _LoggerError.Error(strMsg, e);
            }
            else
            {
                _LoggerError.Error(strMsg);
            }
        }

        public void DebugLog(string strMsg, Exception e)
        {
            //这里为了规范代码，并且方便调试，因此如下写法，将来必须重写
            //throw new Exception();
            if (e != null)
            {
                _LoggerDebug.Debug(strMsg, e);
            }
            else
            {
                _LoggerDebug.Debug(strMsg);
            }
        }
    }
}
