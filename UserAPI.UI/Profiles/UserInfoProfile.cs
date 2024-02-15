using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Service.Profiles
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<Data.Entities.UserInfo, Models.UserInfoDto>();
            CreateMap<Data.Entities.UserInfo, Models.UserInfoForCreationDto>();
            CreateMap<Models.UserInfoForCreationDto, Data.Entities.UserInfo>();
        }
    }
}
