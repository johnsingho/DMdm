using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Framework.Entrypt
{

    /// <summary>
    /// ------------------------------------------------------------------------------
    /// 描  述：加密基类，包含可逆和非可逆加密。
    /// 版本号：
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </summary>
    public class EncryptBase
    {
        private string _key = string.Empty;        //加密的密钥
        private string _excursion = "Flextronics";  //加密的偏移量，提高破解难度
        private EncryptType _EncryptType = EncryptType.DES; //加密类型

        /// <summary>
        /// 描  述：加密密钥
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        public string Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        /// <summary>
        /// 描  述：加密偏移量
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        public string Excursion
        {
            get { return this._excursion; }
            set { this._excursion = value; }
        }

        /// <summary>
        /// 描  述：加密类型
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        public EncryptType EType
        {
            get { return this._EncryptType; }
            set { this._EncryptType = value; }
        }

        /// <summary>
        /// 描  述：字符串加密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="strInput">待加密字符串</param>
        /// <returns>加密串</returns>
        public virtual string EncryptStr(string strInput)
        {
            try
            {
                //实施加密串
                return Encoding.UTF8.GetString(this.Encrypt(Encoding.UTF8.GetBytes(strInput)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 描  述：字符串解密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="strInput">待解密字符串</param>
        /// <returns>解密串</returns>
        public virtual string DecryptStr(string strInput)
        {
            try
            {
                //实施解密串
                return Encoding.UTF8.GetString(this.Decrypt(Encoding.UTF8.GetBytes(strInput)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 描  述：文件及流加密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual byte[] Encrypt(byte[] input)
        {
            try
            {
                //默认直接返回结果串
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 描  述：文件及流解密
        /// 作  者：
        /// 时  间：
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual byte[] Decrypt(byte[] input)
        {
            try
            {
                //默认直接返回结果串
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    /// <summary>
    /// ------------------------------------------------------------------------------
    /// 描  述：加密类型
    /// 版本号：
    /// 作  者：
    /// 日  期：
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </summary>
    public enum EncryptType
    {
        DES = 1,//DES
        RI = 2, //RI
        RSA = 3,//RSA
    }
}
