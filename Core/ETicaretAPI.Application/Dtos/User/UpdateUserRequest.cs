﻿namespace ETicaretAPI.Application.Dtos.User
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
