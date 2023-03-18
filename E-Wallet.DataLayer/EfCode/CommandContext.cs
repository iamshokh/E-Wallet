using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.EfCode
{
    public class CommandContext<T> : 
        ICommandContext<T> where T : class
    {
        private EfCoreContext _context = null;
        private DbSet<T> table = null;

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Create(T obj)
            => table.Add(obj);
        
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
            => _context.SaveChanges();
    }
}
