using System.ComponentModel.DataAnnotations;

namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public class ReplenishWalletRequestDto
    {
        public int UserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
