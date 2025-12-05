using ECommerce.API.Common;
using ECommerce.API.Common.Exceptions;
using ECommerce.API.Data;
using ECommerce.API.DTOs;
using ECommerce.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Services
{
    public class UserSerivces
    {
        private readonly ILogger<UserSerivces> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly TokenHelper _tokenHelper;

        public UserSerivces(ILogger<UserSerivces> logger, IConfiguration configuration, AppDbContext context, IPasswordHasher<User> passwordHasher, TokenHelper tokenHelper)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenHelper = tokenHelper;
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Invalid admin data");
            }
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new BadRequestException("Username and password are required");
            }
            string? normalizedUsername = dto.Username.Trim().ToLower();
            string? normalizedEmail = dto.Email?.Trim().ToLower();

            bool isUserExist = await _context.Users.AnyAsync(u => u.Username != null && u.Username.ToLower() == normalizedUsername);
            bool isEmailExist = dto.Email != null && await _context.Users.AnyAsync(u => u.Email != null && u.Email.ToLower() == normalizedEmail);

            if (isUserExist || isEmailExist)
            {
                throw new ConflictException("Username or Email already exists");
            }
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username.Trim().ToLower(),
                Email = dto.Email?.Trim().ToLower(),
                FullName = dto.FullName?.Trim(),
                PhoneNumber = dto.PhoneNumber?.Trim(),
                Role = Models.Enums.RoleEnum.Admin,
                Status = Models.Enums.UserStatus.Active,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.HashedPassword = hashedPassword;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            RegisterResponseDTO response = new RegisterResponseDTO
            {
                Id = user.Id,
                Username = user.Username ?? string.Empty,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
                Status = user.Status.ToString(),
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
            return response;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto, string? ip)
        {
            if (dto == null || ip == null)
            {
                throw new BadRequestException("Invalid login data");
            }
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new BadRequestException("Username and password are required");
            }
            string? normalizedUsername = dto.Username.Trim().ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username != null && u.Username.ToLower() == normalizedUsername);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid username or password");
            }
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword ?? string.Empty, dto.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Invalid username or password");
            }
            string accessToken = _tokenHelper.GenerateAccessToken(user);
            string refreshToken = _tokenHelper.GenerateRefreshToken();

            user.RefreshTokens!.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(7),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedByIp = ip
            });

            await _context.SaveChangesAsync();

            JwtSettings settings = _configuration.GetSection("JwtSettings").Get<JwtSettings>()!;


            LoginResponseDTO response = new LoginResponseDTO
            {
                AccessToken = accessToken,
                AccessTokenExpiresAt = DateTimeOffset.UtcNow.AddMinutes(settings.ExpiresMinutes),
                AccessTokenExpiresInMinutes = settings.ExpiresMinutes,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddDays(settings.RefreshTokenExpiresDays),
                RefreshTokenExpiresInDays = settings.RefreshTokenExpiresDays
            };

            return response;
        }
    }
}