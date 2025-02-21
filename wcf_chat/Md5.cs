using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;


namespace wcf_chat
{
    internal class Md5
    {
        public static string hashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            //MD5 md5 = MD5.Create();

            /*
            byte[] b = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(b);

            string str = "";
            for (int i = 0; i < hash.Length; i++) { str += hash[i]; }
            MessageBox.Show($"hash {str}");
            return str;
            */
            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);

            string str = "";
            for (int i = 0; i < hash.Length; i++) { str += hash[i]; }
            //MessageBox.Show($"hash {str}");
            return str;
        }
    }
}
