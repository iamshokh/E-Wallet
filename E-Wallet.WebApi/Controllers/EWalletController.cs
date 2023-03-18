using E_Wallet.BizLogicLayer.EWalletServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Wallet.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class EWalletController : ControllerBase
    {
        private readonly IEwalletService _service;
        public EWalletController(IEwalletService servise)
        {
            _service = servise;
        }

        [HttpPost]
        public async Task<IActionResult> ReplanishBalance([FromBody] ReplenishWalletRequestDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.ReplenishWallet(dto);

                if (_service.IsValid)
                {
                    return Ok(result);
                }
                //_service.CopyErrorsToModelState(ModelState);
            }

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        public IActionResult MonthlyOperations(int userId, string digest)
        {
            if (ModelState.IsValid)
            {
                var result = _service.MonthlyOperations(userId, digest);

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
