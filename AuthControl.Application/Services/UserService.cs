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

        // Existing method for validating user credentials
        public async Task<User> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !VerifyPassword(user.PasswordHash, password))
            {
                return null; // Invalid credentials
            }
            return user; // Valid user
        }

        // New method for user registration
        public async Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetUserByUsernameAsync(registrationDto.Username);
            if (existingUser != null)
            {
                // Username already exists
                return false;
            }

            // Hash the password (you may want to use a secure hashing function)
            string hashedPassword = HashPassword(registrationDto.Password);

            

            // Create a new user entity
            var newUser = new User
            {
                Username = registrationDto.Username,
                PasswordHash = hashedPassword,
                Email = registrationDto.Email,
                Role = (UserRole)registrationDto.Role
            };

            // Add the new user to the repository
            await _userRepository.AddUserAsync(newUser);
            return true;
        }

        // Private method to hash passwords
        private string HashPassword(string password)
        {
            // Implement your password hashing logic here
            // Replace with a proper hashing algorithm (e.g., BCrypt, SHA256, etc.)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        // Private method to verify hashed passwords
        private bool VerifyPassword(string passwordHash, string password)
        {
            // Implement proper password verification (e.g., using BCrypt)
            return passwordHash == Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
