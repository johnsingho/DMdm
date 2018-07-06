using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Common
{
    /// <summary>
    /// ------------------------------------------------------------------------------
    /// 描  述：系统基础信息配置[数据库连接串、]
    /// 版本号：
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </summary>
    public class ConfManager
    {
        private static string MainConStr = null;//数据库连接串

        /// <summary>
        /// 描  述：数据库连接串
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <returns></returns>
        public static string GetProperty_MainConStr()
        {
            if (MainConStr == null)
            {
                MainConStr = System.Configuration.ConfigurationManager.AppSettings["MainConStr"];
                return MainConStr;
            }
            else
            {
                return MainConStr;
            }
        }
    }
}
