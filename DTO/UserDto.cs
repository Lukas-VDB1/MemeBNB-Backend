﻿namespace Programming_Web_API.DTO
{
    public class UserDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
