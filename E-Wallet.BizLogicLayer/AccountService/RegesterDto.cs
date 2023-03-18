using System.ComponentModel.DataAnnotations;

namespace E_Wallet.BizLogicLayer.AccountService
{
    public class RegesterDto
    {
        [Required]
        [StringLength(260)]
        public string Shortname { get; set; } = null!;
        [Required]
        [StringLength(500)]
        public string Fullname { get; set; } = null!;
        [Required]
        [StringLength(250)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(250)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]

        public string Password { get; set; } = null!;
        [StringLength(50)]
        public string? PhoneNumber { get; set; }
        [StringLength(250)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
