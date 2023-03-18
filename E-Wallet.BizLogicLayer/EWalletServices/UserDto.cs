using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string Shortname { get; set; }

        public string Fullname { get; set; }
    }
}
