﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WBClient2
{
    public abstract class TokenHelper

    {

        /// <summary>

        /// 验证加密签名

        /// </summary>

        /// <param name="header"></param>

        /// <returns></returns>

        public static bool TokenVerify(string signature, string timestamp, string nonce)

        {
            bool isok = false;

            if (!string.IsNullOrEmpty(signature)

                && !string.IsNullOrEmpty(nonce))

            {


                DateTime timestamp2 = Convert.ToDateTime(timestamp.Replace('T', ' '));
                TimeSpan ts = DateTime.Now.Subtract(timestamp2).Duration();



                if (ts.Seconds < 1000)//如果请求端时间戳与系统时间差小于7秒则继续验证

                {

                    if (signature.Equals(TokenHelper.GetSignature(timestamp2, nonce)))

                    {

                        return true;

                    }

                }

            }
            return isok;
        }

        /// <summary>

        /// 获取加密签名

        /// </summary>

        /// <param name="timestamp"></param>

        /// <param name="nonce"></param>

        /// <returns></returns>

        public static string GetSignature(DateTime timestamp, string nonce)

        {

            string token = System.Configuration.ConfigurationManager.AppSettings["APIToken"];

            string str = string.Format("{0}{1}{2}", token, timestamp.ToString(), nonce);

            List<char> str2 = str.ToList<char>();

            str2.Sort();

            string str3 = "";

            foreach (var item in str2)

            {

                str3 = string.Format("{0}{1}", str3, item.ToString());

            }

            return TokenHelper.MD5Encrypt(str3);

        }

        /// <summary>

        /// MD5加密

        /// </summary>

        /// <param name="strText"></param>

        /// <returns></returns>

        public static string MD5Encrypt(string strText)

        {

            string cryptStr = "";

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bytes = Encoding.UTF8.GetBytes(strText);

            byte[] cryptBytes = md5.ComputeHash(bytes);

            for (int i = 0; i < cryptBytes.Length; i++)

            {

                cryptStr += cryptBytes[i].ToString("X2");

            }

            return cryptStr;

        }



    }
}
