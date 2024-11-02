using AuthControl.Application.Interfaces;
using AuthControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthControl.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly IUserRepository _userRepository;

        public DatabaseSeeder(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SeedAsync()
        {
            // Check if the default admin user exists
            var existingUser = await _userRepository.GetUserByUsernameAsync("admin");

            if (existingUser == null)
            {
                // Create a new default user
                var adminUser = new User
                {
                    Username = "admin",
                    PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("admin")), 
                    Email = "admin@example.com",
                    Role = 0 
                };

                await _userRepository.AddUserAsync(adminUser);
            }
        }
    }
}
