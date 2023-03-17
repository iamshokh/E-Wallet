using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.EfCode
{
    public interface ICommandContext<T> where T : class
    {
        T GetById(object id);
        void Create(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
