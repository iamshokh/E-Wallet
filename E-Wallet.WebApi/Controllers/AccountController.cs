using E_Wallet.BizLogicLayer.AccountService;
using E_Wallet.BizLogicLayer.UserAccountServices;
using E_Wallet.Core.Attributes;
using E_Wallet.Core.Helpers;
using E_Wallet.DataLayer.Repositories.UserAccount;
using Microsoft.AspNetCore.Mvc;

namespace E_Wallet.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : 
        ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService servise)
        {
            _service = servise;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] RegisterateUserDlDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Regester(dto);

                if (_service.IsValid)
                    return Ok(result);

                _service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [XDigestValidationFilter("X-UserId")]
        public async Task<IActionResult> SignIn([FromBody] LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(dto);

                if (_service.IsValid)
                    return Ok(result);

                _service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }
    }
}
