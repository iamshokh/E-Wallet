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
            string xDigestHeader = actionContext.HttpContext.Request.Headers["X-Digest"];
            string xUserId = actionContext.HttpContext.Request.Headers["X-UserId"];

            string requestBody = new StreamReader(actionContext.HttpContext.Request.Body).ReadToEnd();

            if (!ValidateXDigest(xDigestHeader, requestBody, xUserId))
            {
                actionContext.Result = new BadRequestObjectResult("X-Digest не равен хэш-сумме тела запроса");
            }
        }

        private bool ValidateXDigest(string xDigestHeader,
                                     string requestBody,
                                     string userId)
        {
            if (string.IsNullOrEmpty(xDigestHeader))
            {
                return false;
            }

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
