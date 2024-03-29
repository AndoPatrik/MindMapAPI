﻿using System.Text;

namespace MindMapAPI.Util
{
    public class Encryption
    {
        public static string CreateMD5HAsh(string input) 
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X1"));
                }
                return sb.ToString();
            }
        }
    }
}
