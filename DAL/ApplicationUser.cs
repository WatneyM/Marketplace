﻿using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public class ApplicationUser() : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}