using System.Security.Cryptography;
using System.Text;

namespace Helpers
{
    public static class Auth
    {
        public static string GenerateSha256Hash(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] passwordHash = sha.ComputeHash(
                    Encoding.Unicode.GetBytes(password)
                    );
                      
                return Convert.ToBase64String(passwordHash);
            }
        }
        
        public static bool ValidatePassword(string password, string passwordHash)
        {
            password = GenerateSha256Hash(password);

            return password == passwordHash;
        }
    }
}
