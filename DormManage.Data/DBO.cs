using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.Common;
using DormManage.Common.DBManager;
using DormManage.Framework.LogManager;
using DormManage.Framework.Entrypt;

namespace DormManage.Data.DAL
{
    public sealed class DBO
    {
        private static Database _dataBase = null;    //内部单例模式
        private static ConnectionStringSettings _connectSetting;
        private static string _connectString;
        private static string _providerName;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private DBO()
        {

        }
        /// <summary>
        /// 单例模式获取数据库对象，为事务使用
        /// </summary>
        /// <returns></returns>
        public static Database GetInstance()
        {
            //在并发时，使用单一对象
            if (_dataBase == null)
            {
                _dataBase = CreateDatabase();
                return _dataBase;
            }
            else
            {
                lock (_dataBase)
                {
                    return _dataBase;
                }
            }
        }

        /// <summary>
        /// 构造 Database 对象
        /// </summary>
        /// <returns></returns>
        public static Database CreateDatabase()
        {
            string strConnectString = string.Empty;
            string strProviderName = string.Empty;
            Database db = null;
            try
            {
                if (_connectSetting == null)
                {
                    _connectSetting = ConfigurationManager.ConnectionStrings[ConnectionControl.DefaultConnectionstring];
                    _connectString = _connectSetting.ConnectionString;
                    _providerName = _connectSetting.ProviderName;
                }
                // 构造连接对象
                GenericDatabase dbSqlServer = new GenericDatabase(_connectString, DbProviderFactories.GetFactory(_providerName));
                db = dbSqlServer;                
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message, ex);
            }
            return db;
        }

        public static string GetConStr()
        {
            return _connectString;
        }


        /// <summary>
        /// 单例模式获取数据库对象，为事务使用
        /// </summary>
        /// <returns></returns>
        public static Database GetInstanceStaffing()
        {
            ////在并发时，使用单一对象
            //if (_dataBase == null)
            //{
            //    _dataBase = CreateDatabaseStaffing();
            //    return _dataBase;
            //}
            //else
            //{
            //    lock (_dataBase)
            //    {
            //        return _dataBase;
            //    }
            //}
            Database _dataBaseStaffing;
            _dataBaseStaffing = CreateDatabaseStaffing();
            return _dataBaseStaffing;
        }

        /// <summary>
        /// 构造 Database 对象
        /// </summary>
        /// <returns></returns>
        public static Database CreateDatabaseStaffing()
        {
            ConnectionStringSettings _connectSettingStaffing;
            string strConnectString = string.Empty;
            string strProviderName = string.Empty;
            Database db = null;
            try
            {
                //if (_connectSetting == null)
                //{
                //    _connectSetting = ConfigurationManager.ConnectionStrings[ConnectionControl.StaffingConnectionstring];
                //    _connectString = _connectSetting.ConnectionString;
                //    _providerName = _connectSetting.ProviderName;
                //}
                _connectSettingStaffing = ConfigurationManager.ConnectionStrings[ConnectionControl.StaffingConnectionstring];
                strConnectString = _connectSettingStaffing.ConnectionString;
                strProviderName = _connectSettingStaffing.ProviderName;

                // 构造连接对象
                GenericDatabase dbSqlServer = new GenericDatabase(strConnectString, DbProviderFactories.GetFactory(strProviderName));
                db = dbSqlServer;
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message, ex);
            }
            return db;
        }

        public static Database CreateDatabaseKQXT()
        {
            ConnectionStringSettings _connectSettingStaffing;
            string strConnectString = string.Empty;
            string strProviderName = string.Empty;
            Database db = null;
            try
            {
                _connectSettingStaffing = ConfigurationManager.ConnectionStrings[ConnectionControl.KQXTConnectiostring];
                strConnectString = _connectSettingStaffing.ConnectionString;
                strProviderName = _connectSettingStaffing.ProviderName;

                // 构造连接对象
                GenericDatabase dbSqlServer = new GenericDatabase(strConnectString, DbProviderFactories.GetFactory(strProviderName));
                db = dbSqlServer;
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog(ex.Message, ex);
            }
            return db;
        }
    }
}
