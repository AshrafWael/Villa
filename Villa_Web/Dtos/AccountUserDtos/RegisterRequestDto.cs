﻿namespace Villa_Web.Dtos.AccountUserDtos
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public string Email { get; set; }

    }
}
