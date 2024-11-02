using System.Threading.Tasks;
using AuthControl.Application.DTOs;
using AuthControl.Application.Interfaces;
using AuthControl.Domain.Entities;
using AuthControl.Domain.Enums;

namespace AuthControl.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

       
        public async Task<User> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !VerifyPassword(user.PasswordHash, password))
            {
                return null; 
            }
            return user; 
        }

      
        public async Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            
            var existingUser = await _userRepository.GetUserByUsernameAsync(registrationDto.Username);
            if (existingUser != null)
            {
                
                return false;
            }

          
            string hashedPassword = HashPassword(registrationDto.Password);

            

            
            var newUser = new User
            {
                Username = registrationDto.Username,
                PasswordHash = hashedPassword,
                Email = registrationDto.Email,
                Role = (UserRole)registrationDto.Role
            };

            
            await _userRepository.AddUserAsync(newUser);
            return true;
        }

        
        private string HashPassword(string password)
        {
            
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        
        private bool VerifyPassword(string passwordHash, string password)
        {
            
            return passwordHash == Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
