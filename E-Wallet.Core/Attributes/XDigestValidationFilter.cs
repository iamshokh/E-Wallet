using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;

namespace E_Wallet.Core.Attributes
{
    public class XDigestValidationFilter : 
        ActionFilterAttribute
    {
        private readonly string _secretKey;

        public XDigestValidationFilter(string secretKey)
        {
            _secretKey = secretKey;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            // Get the X-Digest header value from the request
            string xDigestHeader = actionContext.HttpContext.Request.Headers["X-Digest"];

            // Get the request body as a string
            string requestBody = new StreamReader(actionContext.HttpContext.Request.Body).ReadToEnd();

            // Validate the X-Digest header using the request body and the secret key
            if (!ValidateXDigest(xDigestHeader, requestBody, actionContext.HttpContext.Request.Headers["X-UserId"]))
            {
                // Return a 400 Bad Request response if the X-Digest header is invalid
                actionContext.Result = new BadRequestObjectResult("Invalid X-Digest header");
            }
        }

        private bool ValidateXDigest(string xDigestHeader,
                                     string requestBody,
                                     string userId)
        {
            // If the X-Digest header is not present, return false
            if (string.IsNullOrEmpty(xDigestHeader))
            {
                return false;
            }

            // Compute the HMAC-SHA256 hash of the request body using the secret key
            var key = Encoding.UTF8.GetBytes(userId);
            var data = Encoding.UTF8.GetBytes(requestBody);

            using (var hmac = new HMACSHA1(key))
            {
                var hash = hmac.ComputeHash(data);
                return Convert.ToBase64String(hash) == xDigestHeader;
            }
        }

    }
}
