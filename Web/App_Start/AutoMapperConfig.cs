using AutoMapper;
using Web.Models;
using Web.Representer;

namespace Web
{
    public static class AutoMapper
    {
        public static IMapper Mapper;

        static AutoMapper()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoleRepresenter, Role>().ForMember(roleDataModel => roleDataModel.Users, options => options.MapFrom(roleRepresenter => roleRepresenter.UserRepresenters));
                cfg.CreateMap<Role, RoleRepresenter>().ForMember(roleRepresenter => roleRepresenter.UserRepresenters, options => options.MapFrom(roleDataModel => roleDataModel.Users));
                cfg.CreateMap<UserRepresenter, User>().ForMember(userDataModel => userDataModel.Roles, options => options.MapFrom(userRepresenter => userRepresenter.RoleRepresenters));
                cfg.CreateMap<User, UserRepresenter>().ForMember(userRepresenter => userRepresenter.RoleRepresenters, options => options.MapFrom(userDataModel => userDataModel.Roles));
            }));
        }
    }
}