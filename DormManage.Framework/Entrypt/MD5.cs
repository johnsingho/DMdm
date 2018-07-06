using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Security;
using System.Web;

namespace DormManage.Framework.Entrypt
{
    public class MD5
    {
        private static Byte[] ConvertStringToByteArray(String s)
        {
            return Encoding.ASCII.GetBytes(s);
        }

        public string Enpt(string olds)
        {
            Byte[] dataToHash = ConvertStringToByteArray(olds);
            byte[] hashvalue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(dataToHash);
            return BitConverter.ToString(hashvalue).Replace("-", "");
        }


        public static string DecryptStr(string rs) //顺序减1解码 
        {

            byte[] bytes = HttpServerUtility.UrlTokenDecode(rs);
            rs = (new UnicodeEncoding()).GetString(bytes);


            byte[] by = new byte[rs.Length];
            for (int i = 0;
            i <= rs.Length - 1;
            i++)
            {
                by[i] = (byte)((byte)rs[i] - 1);
            }
            rs = "";
            for (int i = by.Length - 1;
            i >= 0;
            i--)
            {
                rs += ((char)by[i]).ToString();
            }


            //byte[] bytes = Convert.FromBase64String(rs);
            //rs = (new UnicodeEncoding()).GetString(bytes);
            //byte[] bytes=HttpServerUtility.UrlTokenDecode(rs);
            //rs = (new UnicodeEncoding()).GetString(bytes);
            //rs = rs.Replace( "_M_","http://");
            return rs;
            //return HttpServerUtility.UrlTokenEncode(System.Convert.FromBase64String(rs));

            //byte[] inputBytes = System.Convert.FromBase64String(rs);
            //string inputString = System.Convert.ToBase64String(inputBytes);
            //return inputBytes;
        }

        public static string EncryptStr(string rs) //倒序加1加密 
        {
            //rs=HttpServerUtility.UrlTokenDecode(System.Convert.FromBase64String(rs));
            //string inputString = System.Convert.ToBase64String(inputBytes);
            //return inputBytes;

            //byte[] bytes = (new UnicodeEncoding()).GetBytes(rs);
            //rs = Convert.ToBase64String(bytes);




            byte[] by = new byte[rs.Length];
            for (int i = 0;
            i <= rs.Length - 1;
            i++)
            {
                by[i] = (byte)((byte)rs[i] + 1);
            }
            rs = "";
            for (int i = by.Length - 1;
            i >= 0;
            i--)
            {
                rs += ((char)by[i]).ToString();
            }

            byte[] bytes = (new UnicodeEncoding()).GetBytes(rs);
            rs = HttpServerUtility.UrlTokenEncode(bytes);


            return rs;
        }

    }
}

