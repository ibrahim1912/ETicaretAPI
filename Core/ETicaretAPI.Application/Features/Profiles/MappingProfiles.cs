using AutoMapper;
using ETicaretAPI.Application.Dtos.Order;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI.Application.Features.Queries.Order.GetByIdOrder;

namespace ETicaretAPI.Application.Features.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserRequest, CreateUserCommandRequest>().ReverseMap();
            CreateMap<CreateUserResponse, CreateUserCommandResponse>().ReverseMap();

            CreateMap<SingleOrder, GetByIdOrderQueryResponse>().ReverseMap();
        }
    }
}
