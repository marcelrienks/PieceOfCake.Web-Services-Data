using AutoMapper;
using Data.Models;
using Services.Representers;

namespace Services
{
    public static class AutoMapper
    {
        public static IMapper Mapper;

        /// <summary>
        ///     Configures Mappings between Data Model and Representer
        /// </summary>
        static AutoMapper()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoleRepresenter, Role>().ForMember(roleDataModel => roleDataModel.Users, options => options.MapFrom(roleRepresenter => roleRepresenter.UserRepresentors));
                cfg.CreateMap<Role, RoleRepresenter>().ForMember(roleRepresenter => roleRepresenter.UserRepresentors, options => options.MapFrom(roleDataModel => roleDataModel.Users));
                cfg.CreateMap<UserRepresenter, User>().ForMember(userDataModel => userDataModel.Roles, options => options.MapFrom(userRepresenter => userRepresenter.RoleRepresentors));
                cfg.CreateMap<User, UserRepresenter>().ForMember(userRepresenter => userRepresenter.RoleRepresentors, options => options.MapFrom(userDataModel => userDataModel.Roles));
            }));
        }
    }
}