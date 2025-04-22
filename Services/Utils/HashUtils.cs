using System.Security.Cryptography;
using System.Text;

namespace AG.Services.Utils
{
    public class HashUtils
    {
        public static string HashPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass))
                return "";

            var bytes = Encoding.Default.GetBytes(pass);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
