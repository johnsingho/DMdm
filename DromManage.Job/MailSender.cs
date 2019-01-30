using System;
using DromManage.Job.comm;
using System.Text;

namespace DromManage.Job
{
    /// <summary
    /// 生成邮件发送
    /// johnsing 2019-01-30
    /// 
    /// </summary>
    public class MailSender
    {
        private string SmtpConnStr;
        private string EmailCC;
        private string EmailReceiver;

        public MailSender(string sSmtpConn, string sEmailRecv, string sEmailCc)
        {
            SmtpConnStr = sSmtpConn;
            EmailReceiver = sEmailRecv;
            EmailCC = sEmailCc;
        }

        public bool SendMail(string sFileTempl, string sSubject, string sContent, string sTable)
        {
            //send mail
            var sBody = new StringBuilder();
            try
            {
                sBody.Append(System.IO.File.ReadAllText(sFileTempl));
                sBody.Replace("{title}", sSubject);
                sBody.Replace("{content}", sContent);
                sBody.Replace("{tabContent}", sTable);
            }
            catch (Exception ex)
            {
                //LogHelper.WriteError(typeof(CommodityRecv_DAL), ex);
                return false;
            }

            var eh = new EmailHelper(SmtpConnStr);
            eh.AddToAddress(EmailReceiver);
            eh.AddCcAddress(EmailCC);
            eh.Subject = sSubject;
            eh.Body = sBody.ToString();

            var bSend = true;
            try
            {
                eh.SendMail();
            }
            catch(Exception ex)
            {
                bSend = false;
            }
            return bSend;
            
        }
        
    }
}
