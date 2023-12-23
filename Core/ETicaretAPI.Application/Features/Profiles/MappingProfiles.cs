using AutoMapper;
using ETicaretAPI.Application.Dtos.Order;
using ETicaretAPI.Application.Dtos.Product;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI.Application.Features.Commands.AppUser.UpdateUser;
using ETicaretAPI.Application.Features.Queries.AppUser.GetByIdUser;
using ETicaretAPI.Application.Features.Queries.Order.GetByIdOrder;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProductWithImage;

namespace ETicaretAPI.Application.Features.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserRequest, CreateUserCommandRequest>().ReverseMap();
            CreateMap<CreateUserResponse, CreateUserCommandResponse>().ReverseMap();

            CreateMap<SingleOrder, GetByIdOrderQueryResponse>().ReverseMap();
            CreateMap<SingleProduct, GetByIdProductWithImageQueryResponse>().ReverseMap();
            CreateMap<SingleUser, GetByIdUserQueryResponse>().ReverseMap();

            CreateMap<UpdateUserRequest, UpdateUserCommandRequest>().ReverseMap();
            CreateMap<UpdateUserResponse, UpdateUserCommandResponse>().ReverseMap();

        }
    }
}
