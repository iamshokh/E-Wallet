using E_Wallet.BizLogicLayer.EWalletServices;
using E_Wallet.BizLogicLayer.UserAccountServices;
using E_Wallet.DataLayer.Repositories;
using E_Wallet.DataLayer.Repositories.EWalletTransaction;
using E_Wallet.DataLayer.Repositories.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Wallet.WebApi.Controllers
{
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
        public async Task<IActionResult> ReplanishBalance([FromBody] EWalletTransactionDlDto dto,int userId, string digest)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.ReplenishWallet(dto, userId, digest);

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
