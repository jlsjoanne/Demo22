using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Demo22.Models;
using Demo22.ViewModels;
using Demo22.Areas.Admin.Data;

namespace Demo22.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ViewMember, Member>()
                .ForMember(dest => dest.Identity, opt => opt.Ignore()).ReverseMap();
            CreateMap<MemberMgmt, Member>().ReverseMap();

        }
    }
}