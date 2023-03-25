using E_Wallet.BizLogicLayer.AccountService;
using E_Wallet.BizLogicLayer.EWalletServices;
using E_Wallet.Core.Attributes;
using E_Wallet.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace E_Wallet.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class EWalletController : ControllerBase
    {
        private readonly IEwalletService _service;
        private readonly IAccountService accountService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EWalletController(
            IEwalletService servise,
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _service = servise;
            this.accountService = accountService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [XDigestValidationFilter("X-UserId")]
        public async Task<IActionResult> ReplanishBalance([FromBody] ReplenishWalletRequestDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.ReplenishWallet(dto);

                if (_service.IsValid)
                    return Ok(result);

                _service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }

        [HttpGet]
        public IActionResult MonthlyOperations(string accountNumber)
        {
            if (ModelState.IsValid)
            {
                var result = _service.MonthlyOperations(accountNumber);

                if (_service.IsValid)
                    return Ok(result);

                _service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }

        [HttpGet]
        public IActionResult AccountExists(string accountNumber)
        {
            if (ModelState.IsValid)
            {
                var result = _service.AccountExists(accountNumber);

                if (_service.IsValid)
                    return Ok(result);

                _service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }

        [HttpGet]
        public IActionResult GetBalance(string accountNumber)
        {
            if (ModelState.IsValid)
            {
                var result = _service.GetBalance(accountNumber);

                if (_service.IsValid)
                    return Ok(result);

                _service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }
    }
}
