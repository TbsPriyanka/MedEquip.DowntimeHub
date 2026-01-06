using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text;

namespace MedEquip.DowntimeHub.Common.JwtHelper
{
    public class JwtTokenHelper
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenHelper(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string GenerateJwtToken(int userId)
        {
            if (string.IsNullOrWhiteSpace(_jwtSettings.SecretKey))
                throw new InvalidOperationException("JWT SecretKey is missing.");

            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey); 

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())}),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiryInHours),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}