using BikeRental.Business.Interfaces;
using BikeRental.Middleware;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BikeRental.Business.Services
{
    public class JWTAuthService : IJWTAuthService
    {
        private readonly byte[] _secret;
        private JWTSettings JWTSettings;

        public JWTAuthService(IOptions<JWTSettings> _JWTSettings)
        {
            JWTSettings = _JWTSettings.Value;
            _secret = Encoding.UTF8.GetBytes(JWTSettings.Secret);
        }
        public string GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = now.AddMinutes(JWTSettings.AccessTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256),
                Audience = JWTSettings.Audience,
                Issuer = JWTSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return accessToken;
        }

    }
}
