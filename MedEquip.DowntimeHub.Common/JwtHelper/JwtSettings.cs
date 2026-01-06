namespace MedEquip.DowntimeHub.Common.JwtHelper
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiryInHours { get; set; }
        public int ExpiryInMinutes { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int RefreshTokenExpiryDays { get; set; }
    }
}
