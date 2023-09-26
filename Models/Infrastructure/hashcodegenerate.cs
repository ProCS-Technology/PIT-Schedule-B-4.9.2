using System.Security.Cryptography;
using System.Text;

namespace ProcsDLL.Models.Infrastructure
{
    public class hashcodegenerate
    {
        public static string GetSHA512(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512hasher = new SHA512Managed();

            byte[] hashBytes = sha512hasher.ComputeHash(encoder.GetBytes(phrase));
            return BytesToHex(hashBytes);
        }

        public static string BytesToHex(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString.Append(bytes[i].ToString("X2"));
            }
            return hexString.ToString();
        }
    }
}