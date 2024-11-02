using AuthControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthControl.Application.DTOs
{
    public class UserRegistrationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Passwordconfirmation { get; set; }

        public string Email { get; set; }
        public int Role { get; set; }
    }
}
