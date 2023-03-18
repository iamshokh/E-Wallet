using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.Core.Security
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        public static string GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string password,
                                          string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, Iterations))
            {
                byte[] hashBytes = pbkdf2.GetBytes(HashSize);
                byte[] resultBytes = new byte[SaltSize + HashSize];

                Buffer.BlockCopy(saltBytes, 0, resultBytes, 0, SaltSize);
                Buffer.BlockCopy(hashBytes, 0, resultBytes, SaltSize, HashSize);
                
                return Convert.ToBase64String(resultBytes);
            }
        }

        public static bool VerifyPassword(string password,
                                          string hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] saltBytes = new byte[SaltSize];

            Buffer.BlockCopy(hashBytes, 0, saltBytes, 0, SaltSize);

            string expectedHash = GenerateHash(password: password,
                                               salt: Convert.ToBase64String(saltBytes));
            
            return hash == expectedHash;
        }
    }
}
