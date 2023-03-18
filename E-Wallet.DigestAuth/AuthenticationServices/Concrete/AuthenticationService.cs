using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DigestAuth.AuthenticationServices
{
    public class AuthenticationService
    {
        /*public bool IsAuthenticated(HttpRequestHeaders headers)
        {
            // Verify that the X-UserId and X-Digest headers are present
            if (!headers.Contains("X-UserId") || !headers.Contains("X-Digest"))
            {
                return false;
            }
            // Verify that the X-Digest header matches the hash of the request body
            var userId = headers.GetValues("X-UserId").FirstOrDefault();
            var digest = headers.GetValues("X-Digest").FirstOrDefault();
            var requestBody = Request.Content.ReadAsStringAsync().Result;
            var expectedDigest = CalculateDigest(userId, requestBody);

            return digest == expectedDigest;
        }*/

        private string CalculateDigest(string userId, string requestBody)
        {
            var key = Encoding.UTF8.GetBytes(userId);
            var data = Encoding.UTF8.GetBytes(requestBody);
            using (var hmac = new HMACSHA1(key))
            {
                var hash = hmac.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
