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
    /// 描  述：对称加密，RI加密方式类
    /// 版本号：
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </summary>
    public class Rijndael:EncryptBase
    {
       private SymmetricAlgorithm mobjCryptoService;  //RI加密实例

        /// <summary>
        /// 描  述：构造函数
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="key">加密钥匙</param>
        public Rijndael(string key)
        {
            this.Key = key;
            this.EType = EncryptType.RI;
            mobjCryptoService = new RijndaelManaged();
        }

        /// <summary>
        /// 描  述：RI文件或流解密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input">待解密的文件或流</param>
        /// <returns>解密结果</returns>
        public override byte[]  Decrypt(byte[] input)
        {
            try
            {
                MemoryStream ms = new MemoryStream(input, 0, input.Length);
                mobjCryptoService.Key =Encoding.UTF8.GetBytes(this.Key);
                mobjCryptoService.IV = Encoding.UTF8.GetBytes(this.Excursion);
                //创建对称解密器对象
                ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
                //定义将数据流链接到加密转换的流
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return Encoding.UTF8.GetBytes(sr.ReadToEnd());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 描  述：RI文件或流加密方法
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input">待加密的文件或流</param>
        /// <returns>加密结果</returns>
        public override byte[]  Encrypt(byte[] input)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                mobjCryptoService.Key =Encoding.UTF8.GetBytes(this.Key);
                mobjCryptoService.IV = Encoding.UTF8.GetBytes(this.Excursion);
                //创建对称加密器对象
                ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
                //定义将数据流链接到加密转换的流
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();
                ms.Close();
                byte[] bytOut = ms.ToArray();
                return bytOut;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}