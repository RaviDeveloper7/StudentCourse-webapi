using Microsoft.IdentityModel.Tokens;
using StudentCourseAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentCourseAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken CreateRefreshToken();
        DateTime GetAccessTokenExpiry();
    }
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly string _key;

        private readonly string _issuer;

        private readonly string _audience;

        private readonly int _expiryMinutes;

        private readonly int _refreshExpiryDays;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = config["Jwt:Key"];
            _issuer = config["Jwt:Issuer"];
            _audience = config["Jwt:Audience"];
            _expiryMinutes = int.Parse( config["Jwt:AccessTokenExpiryMinutes"] ?? "1");
            _refreshExpiryDays = int.Parse( config["Jwt:RefreshTokenExpiryDays"] ?? "7");

        }

        public RefreshToken CreateRefreshToken()
        {
            var randomBytes = new byte[64];
            RandomNumberGenerator.Fill(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresAt = DateTime.UtcNow.AddDays(_refreshExpiryDays),
                IsUsed = false,
                IsRevoked = false
            };
        }

        public string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer : _issuer,
                audience : _audience,
                claims : claims,
                expires : DateTime.Now.AddMinutes(_expiryMinutes),
                signingCredentials : creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public DateTime GetAccessTokenExpiry()
        {
          return  DateTime.UtcNow.AddMinutes(_expiryMinutes);
        }
    }
}