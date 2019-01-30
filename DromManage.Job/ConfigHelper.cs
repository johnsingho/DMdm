using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace DromManage.Job
{
    public class ConfigHelper
    {
        public static string SMTPConnection;
        public static string EmailSubject;
        public static int ExpiredDay=3;
        public static bool SendEmail;

        static ConfigHelper()
        {
            SMTPConnection = ConfigurationManager.AppSettings["SMTPConnection"].Trim();
            EmailSubject = ConfigurationManager.AppSettings["EmailSubject"].Trim();

            var sTemp = ConfigurationManager.AppSettings["ExpiredDay"].Trim();
            int.TryParse(sTemp, out ExpiredDay);

            sTemp = ConfigurationManager.AppSettings["SendEmail"].Trim();
            bool.TryParse(sTemp, out SendEmail);
        }

        public static string GetAppSettings(string key)
        {
            return "Server=DMNNT813;Database=DormManage;Uid=admin_Dorm;Pwd=dmn@DORM022";
        }

        public static void SetValue(XmlDocument xmlDocument, string selectPath, string key, string keyValue)
        {
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    xmlNode.Attributes["value"].Value = keyValue;
                    break;
                }
            }
        }
    }
}
