using AutoMapper;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;

namespace ETicaretAPI.Application.Features.Profiles.AppUser
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserRequest, CreateUserCommandRequest>().ReverseMap();
            CreateMap<CreateUserResponse, CreateUserCommandResponse>().ReverseMap();

        }
    }
}
