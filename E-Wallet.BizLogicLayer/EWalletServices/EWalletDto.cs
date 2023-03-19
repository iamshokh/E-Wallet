
namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public class EWalletDto
    {
        public string Count { get; set; }
        public string User { get; set; }

        public string AccountNumber { get; set; } 

        public string Balance { get; set; }

        new public List<EWalletTransactionDto> EWalletTransactions { get; set; }  = new(); 
    }
}
