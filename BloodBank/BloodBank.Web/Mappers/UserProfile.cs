using AutoMapper;
using BloodBank.Models;
using BloodBank.Web.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Mappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserDTO, User>();
            CreateMap<AddDonatorDTO, User>();
            CreateMap<User, ReturnDonatorDTO>();
            CreateMap<User, ReturnUserDTO>();
        }
    }
}
