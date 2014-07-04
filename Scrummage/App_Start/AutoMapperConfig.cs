using System.Collections.Generic;
using AutoMapper;
using Scrummage.DataAccess.Models;
using Scrummage.ViewModels;

namespace Scrummage
{
    public class AutoMapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Role, RoleViewModel>();
            Mapper.CreateMap<RoleViewModel, Role>();
        }
    }
}