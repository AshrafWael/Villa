﻿namespace Villa_Web.Dtos.AccountUserDtos
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Username { get; set; } = string.Empty;
    }
}
 