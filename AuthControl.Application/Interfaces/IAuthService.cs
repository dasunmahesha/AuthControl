using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthControl.Application.DTOs;

namespace AuthControl.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestDto request);
        Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto);
    }
}
    