﻿namespace HR.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> Roles { get; set; } = new List<string>();
    }

}