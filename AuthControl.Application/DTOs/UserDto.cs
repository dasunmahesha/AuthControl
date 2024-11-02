﻿using AuthControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthControl.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; } 
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}