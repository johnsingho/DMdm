using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Common.DBManager
{
    /// <summary>
    /// 对连接的处理
    /// </summary>
    public sealed class ConnectionControl
    {

        private static string _DefaultConnetionstring = "Connection";
        private static string _StaffingConnectionstring = "StaffingConnection";
        private static string _KQXTConnectiostring = "KQXTConnection";
        private static string _EMConnection = "EMConnection";

        /// <summary>
        /// 默认连接   与配置文件的同步
        /// </summary>
        public static string DefaultConnectionstring
        {
            get { return _DefaultConnetionstring; }
            set { _DefaultConnetionstring = value; }
        }

        /// <summary>
        /// 默认连接   与配置文件的同步
        /// </summary>
        public static string StaffingConnectionstring
        {
            get { return _StaffingConnectionstring; }
            set { _StaffingConnectionstring = value; }
        }

        public static string KQXTConnectiostring
        {
            get
            {
                return _KQXTConnectiostring;
            }

            set
            {
                _KQXTConnectiostring = value;
            }
        }
        public static string EMConnection
        {
            get
            {
                return _EMConnection;
            }

            set
            {
                _EMConnection = value;
            }
        }
    }
}
