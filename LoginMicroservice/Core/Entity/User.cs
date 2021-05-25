using System;

namespace UserMicroservice.Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}