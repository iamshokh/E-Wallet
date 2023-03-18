using E_Wallet.BizLogicLayer.UserAccountServices;
using E_Wallet.DataLayer.Repositories.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Wallet.WebApi.Controllers
{
    //[Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountService _service;
        public AccountController(IUserAccountService servise)
        {
            _service = servise;
        }

        [HttpPost]
        public async Task<IActionResult> Registrate([FromBody] RegisterateUserDlDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Registrate(dto);

                if (_service.IsValid)
                {
                    return Ok(result);
                }

                //_service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserLoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(dto);

                if (_service.IsValid)
                {
                    return Ok(result);
                }

                //_service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }
    }
}
