using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DromManage.Job
{
    public class ConfigHelper
    {
        public static string GetAppSettings(string key)
        {
            return "Server=10.206.157.14;Database=OA_CSH_DormManage;Uid=OA_CSH_DormManage;Pwd=OAdor!2018mgen";
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
