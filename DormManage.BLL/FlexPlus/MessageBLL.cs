using DormManage.Framework.LogManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DormManage.BLL.FlexPlus
{
    public class MessageBLL
    {
        //极光推送到Flex+ app
        public static void SendJpush(string workdayNo, string title, string title_Content, string msgMainContent, string msg_type)
        {
            try
            {
                //ashx Url
                string getGscUserUrl = "https://zhmobile.flextronics.com/EvaluationApp/JpushMsgService.ashx";

                //加入参数，用于更新请求
                string urlHandler = getGscUserUrl + "?action=push_by_user" + "&workdayNo=" + workdayNo + "&title=" + title + "&msgcontent=" + title_Content + "&msgMainContent=" + msgMainContent + "&msg_type=" + msg_type;
                var webRequest = (HttpWebRequest)HttpWebRequest.Create(urlHandler);
                webRequest.Timeout = 3000;//3秒超时
                //调用ashx，并取值
                var responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                string currentUserGulid = responseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().ErrorLog("消息推送失败", ex);
            }
        }

    }
}
