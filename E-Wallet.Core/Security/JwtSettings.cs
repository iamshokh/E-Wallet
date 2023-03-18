namespace E_Wallet.Core.Security
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public int ExpiresInMinutes { get; set; }
    }
}
