using E_Wallet.Core;
using E_Wallet.Core.Security;
using E_Wallet.DataLayer.EfClasses;
using E_Wallet.DataLayer.EfCode;
using E_Wallet.DataLayer.Repositories.UserAccount;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.Repositories
{
    public class UserAccountRepository :
        CommandContext<User>,
        IUserAccountRepository
    {
        private readonly EfCoreContext _context;
        public UserAccountRepository(EfCoreContext context)
        {
            _context = context;
        }

        public User ByUserName(string userName)
        {
            var entity = _context.Set<User>()
                            .FirstOrDefault(a => a.UserName == userName);
            
            if (entity == null)
                throw new Exception("По вашему запросу запись не найдено");
            
            return entity;
        }

        public User Registrate(RegisterateUserDlDto dto)
        {
            Random rd = new Random();
            
            var entity = new User();
            entity.UserName = dto.UserName;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Email = dto.Email;
            entity.Fullname = dto.Fullname;
            entity.Shortname = dto.Shortname;
            entity.StateId = StateIdConst.ACTIVE;
            entity.PasswordSalt = PasswordHasher.GenerateSalt();
            entity.PasswordHash = PasswordHasher.GenerateHash(dto.Password, entity.PasswordSalt);
            entity.IsIdentificate = rd.Next(0, 9) > 5 ? true : false; 

            return entity;
        }
    }
}
