using AuthControl.Application.DTOs;
using AuthControl.Domain.Entities;
using System.Threading.Tasks;

namespace AuthControl.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> ValidateUserCredentialsAsync(string username, string password);
        Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto); 
    }
}
