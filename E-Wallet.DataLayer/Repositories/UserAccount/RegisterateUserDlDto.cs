using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.Repositories.UserAccount
{
    public class RegisterateUserDlDto
    {
        [Required]
        [StringLength(250)]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(250)]
        public string Password { get; set; } = null!;
        [StringLength(250)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? PhoneNumber { get; set; }
        [Required]
        [StringLength(260)]
        public string Shortname { get; set; } = null!;
        [Required]
        [StringLength(500)]
        public string Fullname { get; set; } = null!;
    }
}
