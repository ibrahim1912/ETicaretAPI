﻿using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUserRequest createUserRequest);

        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    }
}
