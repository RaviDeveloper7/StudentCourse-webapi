using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;
using StudentCourseAPI.Services;
using System.Security.Cryptography;

namespace StudentCourseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        private readonly AppDbContext _context;

        public AuthController(ITokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            if (await _context.users.AnyAsync(u => u.UserName == registerRequestDto.UserName))
                return BadRequest("Username already exists.");

            var user = new User
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                PasswordHash = HashPassword(registerRequestDto.Password),
                Role = "User"
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _context.users.SingleOrDefaultAsync(u=>u.UserName==loginRequestDto.UserName);

            if (user == null || !VerifyPassword(loginRequestDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var accessToken = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            refreshToken.UserId = user.Id;
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                AccessToken = accessToken,
                AccessTokenExpiresAt = _tokenService.GetAccessTokenExpiry(),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAt = refreshToken.ExpiresAt
            });

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody]string refreshToken)
        {
            var existingToken = await _context.RefreshTokens.Include(rt=>rt.User)
                .SingleOrDefaultAsync(rt => rt.Token == refreshToken);

            if(existingToken==null||existingToken.IsUsed||existingToken.IsRevoked)
                return Unauthorized("Invalid refresh token.");

            if (existingToken.ExpiresAt<DateTime.UtcNow)
                return Unauthorized("Refresh token has expired.");

            existingToken.IsUsed = true;
            _context.RefreshTokens.Update(existingToken);

            var newAccessToken = _tokenService.CreateToken(existingToken.User);
            var newRefreshToken = _tokenService.CreateRefreshToken();
            newRefreshToken.UserId = existingToken.UserId;
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
            return Ok(new AuthResponseDto
            {
                AccessToken = newAccessToken,
                AccessTokenExpiresAt = _tokenService.GetAccessTokenExpiry(),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiresAt = newRefreshToken.ExpiresAt
            });
        }

        private static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);
            var result = new byte[49]; // 16 + 32 + 1
            Buffer.BlockCopy(salt, 0, result, 1, 16);
            Buffer.BlockCopy(hash, 0, result, 17, 32);
            result[0] = 0x01; // version
            return Convert.ToBase64String(result);
        }

        private static bool VerifyPassword(string password, string stored)
        {
            var bytes = Convert.FromBase64String(stored);
            if (bytes[0] != 0x01) return false;
            var salt = new byte[16];
            Buffer.BlockCopy(bytes, 1, salt, 0, 16);
            var hash = new byte[32];
            Buffer.BlockCopy(bytes, 17, hash, 0, 32);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            var testHash = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(testHash, hash);
        }

    }
}
