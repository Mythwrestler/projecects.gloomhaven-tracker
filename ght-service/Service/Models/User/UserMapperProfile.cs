
using AutoMapper;
using GloomhavenTracker.Database.Models;

namespace GloomhavenTracker.Service.Models;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserDAO, User>().ReverseMap();
        CreateMap<User, UserDTO>();
    }
}