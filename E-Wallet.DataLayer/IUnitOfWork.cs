using E_Wallet.DataLayer.Repositories;

namespace E_Wallet.DataLayer
{
    public interface IUnitOfWork : 
        IDisposable
    {
        IUserAccountRepository UserAccounts { get; }
    }
}
