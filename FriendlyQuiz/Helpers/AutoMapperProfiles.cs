using AutoMapper;
using FriendlyQuiz.DTOs;
using FriendlyQuiz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendlyQuiz.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterUserDTO, User>(); 
        }
    }
}
