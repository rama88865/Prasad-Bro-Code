﻿namespace INMAR.Service.Models
{
    public class AuthResponse
    {
        public string JwtToken { get; set; }
        public bool ValidUser { get; set; }
        public bool IsActive { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
