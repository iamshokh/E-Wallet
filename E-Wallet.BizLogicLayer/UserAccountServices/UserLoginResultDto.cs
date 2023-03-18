using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.BizLogicLayer.UserAccountServices
{
    public class UserLoginResultDto
    {
        public bool TrustedDevice { get; set; }
        public bool RequiredPhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
