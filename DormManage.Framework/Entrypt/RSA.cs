using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DormManage.Framework.Entrypt
{

    /// <summary>
    /// ------------------------------------------------------------------------------
    /// 描  述：非对称加密，RSA
    /// 版本号：
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </summary>
    public class RSA : EncryptBase
    {
        private RSACryptoServiceProvider _rsa = null;  //非对称加密实例

        /// <summary>
        /// 描  述：构造函数
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        public RSA()
        {
            this.EType = EncryptType.RSA;
            _rsa = new RSACryptoServiceProvider();
        }

        /// <summary>
        /// 描  述：RSA字符串解密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="strInput">待解密串</param>
        /// <returns>解密结果</returns>
        public override string DecryptStr(string strInput)
        {
            try
            {
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(strInput);
                //执行加密
                dataToEncrypt = this.Decrypt(dataToEncrypt);
                //可根据需要在以下内容，进行深度加密。
                //暂时不扩展
                return Encoding.UTF8.GetString(dataToEncrypt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 描  述：RSA字符串加密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="strInput">待加密串</param>
        /// <returns>加密结果</returns>
        public override string EncryptStr(string strInput)
        {
            try
            {
                //可根据需要在以下内容，进行深度解密。
                //暂时不扩展
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(strInput);
                //执行解密
                dataToEncrypt = this.Encrypt(dataToEncrypt);
                return Encoding.UTF8.GetString(dataToEncrypt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 描  述：RSA文件或流解密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input">待解密流或文件</param>
        /// <returns>加密结果</returns>
        public override byte[] Decrypt(byte[] input)
        {
            try
            {
                input = _rsa.Decrypt(input, false);
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 描  述：RSA文件或流加密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input">待加密文件或流</param>
        /// <returns>解密结果</returns>
        public override byte[] Encrypt(byte[] input)
        {
            try
            {
                input = _rsa.Encrypt(input, false);
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
