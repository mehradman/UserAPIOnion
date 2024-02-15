using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Data.Entities;
using UserAPI.Service.Models;

namespace UserAPI.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserWithoutInfoDto>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserForCreationDto>();
            CreateMap<UserForCreationDto, User>();
        }
    }
}
