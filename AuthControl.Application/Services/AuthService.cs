using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthControl.Application.DTOs;
using AuthControl.Application.Interfaces;
using AuthControl.Application.Configurations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace AuthControl.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthService(IUserService userService, IOptions<JwtSettings> jwtSettingsOptions)
        {
            _userService = userService;

            var jwtSettings = jwtSettingsOptions.Value;
            _secretKey = jwtSettings.SecretKey;
            _issuer = jwtSettings.Issuer;
            _audience = jwtSettings.Audience;
        }

        public async Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            return await _userService.RegisterUserAsync(registrationDto);
        }

        public async Task<string> LoginAsync(LoginRequestDto request)
        {
            var user = await _userService.ValidateUserCredentialsAsync(request.Username, request.Password);
            if (user == null)
            {
                return null; 
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
