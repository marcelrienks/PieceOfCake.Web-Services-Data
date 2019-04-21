using System;
using System.Security.Cryptography;
using System.Text;

namespace Web.Libraries
{
    public class Encryption
    {
        /// <summary>
        ///     Performs a Sha 256 Hash function on a supplied value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string Hash(string value)
        {
            var result = new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToBase64String(result, 0, result.Length);
        }
    }
}