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
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; } = null!;
        [StringLength(250)]
        [DataType(DataType.EmailAddress)]
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
