
namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public class EWalletDto
    {
        public string User { get; set; }

        public string AccountNumber { get; set; } 

        public decimal Balance { get; set; }

        public string State { get; set; }

        public List<EWalletTransactionDto> EWalletTransactions = new(); 
    }
}
