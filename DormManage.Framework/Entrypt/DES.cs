using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace DormManage.Framework.Entrypt
{

    /// <summary>
    /// ------------------------------------------------------------------------------
    /// 描  述：DES加密
    /// 版本号：
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </summary>
    public class DES : EncryptBase
    {
        private TripleDESCryptoServiceProvider tdes = null; //DES加密方式实例
        /// <summary>
        /// 描  述：构造函数
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        public DES()
        {
            this.EType = EncryptType.DES;
            tdes = new TripleDESCryptoServiceProvider();
        }

        /// <summary>
        /// 描  述：DES文件或流解密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input">待解密的文件或流</param>
        /// <returns>解密结果</returns>
        public override byte[] Decrypt(byte[] input)
        {
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(this.Key);
                byte[] IV = Encoding.UTF8.GetBytes(this.Excursion);
                //根据加密后的字节数组创建一个内存流
                MemoryStream memoryStream = new MemoryStream();
                //使用传递的私钥、IV 和内存流创建解密流
                CryptoStream cryptoStream = new CryptoStream(memoryStream, new TripleDESCryptoServiceProvider().CreateDecryptor(key, IV), CryptoStreamMode.Write);
                //创建一个字节数组保存解密后的数据
                //byte[] decryptBytes = new byte[input.Length];
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();

                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 描  述：DES字符串解密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="strInput">解加密的字符串</param>
        /// <returns>解密结果</returns>
        public override string DecryptStr(string strInput)
        {
            try
            {
                //实施解密串
                return Encoding.UTF8.GetString(this.Decrypt(Convert.FromBase64String(strInput)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 描  述：DES文件或流加密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input">待加密的文件或流</param>
        /// <returns>加密结果</returns>
        public override byte[] Encrypt(byte[] input)
        {
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(this.Key);
                byte[] IV = Encoding.UTF8.GetBytes(this.Excursion);
                //得到加密后的字节流
                //创建一个内存流
                MemoryStream memoryStream = new MemoryStream();
                //使用传递的私钥和IV 创建加密流
                CryptoStream cryptoStream = new CryptoStream(memoryStream, new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV), CryptoStreamMode.Write);
                //将字节数组写入加密流,并清除缓冲区
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();
                //得到加密后的字节数组
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 描  述：DES字符串加密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="strInput">待加密的串</param>
        /// <returns>加密结果</returns>
        public override string EncryptStr(string strInput)
        {
            try
            {
                //实施加密串
                return Convert.ToBase64String(this.Encrypt(Encoding.UTF8.GetBytes(strInput)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
