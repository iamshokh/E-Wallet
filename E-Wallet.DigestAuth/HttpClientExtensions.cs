using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_Wallet.DigestAuth
{
    public static class HttpClientExtensions
    {
        private static readonly string _sharedSecret = "mysharedsecretkey";

        public static async Task<HttpResponseMessage> SendWithDigestAuthAsync(this HttpClient client,
            HttpRequestMessage request, HttpCompletionOption httpCompletionOption,
            string username, string password)
        {
            var newRequest = CloneBeforeContentSet(request);
            var response = await client.SendAsync(request, httpCompletionOption);
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized) return response;

            var wwwAuthenticateHeaderValue = response.Headers.GetValues("WWW-Authenticate").FirstOrDefault();

            var realm = GetChallengeValueFromHeader("realm", wwwAuthenticateHeaderValue);
            var nonce = GetChallengeValueFromHeader("nonce", wwwAuthenticateHeaderValue);
            var qop = GetChallengeValueFromHeader("qop", wwwAuthenticateHeaderValue);

            var clientNonce = new Random().Next(123400, 9999999).ToString();
            var opaque = GetChallengeValueFromHeader("opaque", wwwAuthenticateHeaderValue);

            var digestHeader = new DigestAuthHeader(realm, username, password, nonce, qop, nonceCount: 1, clientNonce, opaque);
            var digestRequestHeader = GetDigestHeader(digestHeader, newRequest.RequestUri.ToString(), request.Method);

            newRequest.Headers.Add("Authorization", digestRequestHeader);
            var authRes = await client.SendAsync(newRequest, httpCompletionOption);
            return authRes;
        }

        private static string GetChallengeValueFromHeader(string challengeName, string fullHeaderValue)
        {
            var regHeader = new Regex($@"{challengeName}=""([^""]*)""");
            var matchHeader = regHeader.Match(fullHeaderValue);

            if (matchHeader.Success) return matchHeader.Groups[1].Value;

            throw new ApplicationException($"Header {challengeName} not found");
        }

        private static string GenerateHMACSHA1Hash(string input)
        {
            using (var hash = new HMACSHA1(Encoding.UTF8.GetBytes(_sharedSecret)))
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(input))
                     .Select(x => x.ToString("x2")));
            };
        }

        private static string GetDigestHeader(DigestAuthHeader digest, string digestUri, HttpMethod method)
        {
            var ha1 = GenerateHMACSHA1Hash($"{digest.Username}:{digest.Realm}:{digest.Password}");
            var ha2 = GenerateHMACSHA1Hash($"{method}:{digestUri}");
            var digestResponse =
                GenerateHMACSHA1Hash($"{ha1}:{digest.Nonce}:{digest.NonceCount:00000000}:{digest.ClientNonce}:{digest.QualityOfProtection}:{ha2}");

            var headerString =
                $"Digest username=\"{digest.Username}\", realm=\"{digest.Realm}\", nonce=\"{digest.Nonce}\", uri=\"{digestUri}\", " +
                $"algorithm=MD5, qop={digest.QualityOfProtection}, nc={digest.NonceCount:00000000}, cnonce=\"{digest.ClientNonce}\", " +
                $"response=\"{digestResponse}\", opaque=\"{digest.Opaque}\"";

            return headerString;
        }

        private static HttpRequestMessage CloneBeforeContentSet(this HttpRequestMessage req)
        {
            HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);

            clone.Content = req.Content;
            clone.Version = req.Version;

            foreach (KeyValuePair<string, object> prop in req.Properties)
            {
                clone.Properties.Add(prop);
            }

            foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clone;
        }
    }
}
