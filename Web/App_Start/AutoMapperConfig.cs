using AutoMapper;
using Web.Models;
using Web.Representer;

namespace Services
{
    public class AutoMapperConfig
    {
        /// <summary>
        ///     Configures Mappings between Data Model and Representer
        /// </summary>
        public static void ConfigureMappings()
        {
            ConfigureRoleModelMappings();
            ConfigureUserModelMappings();
        }

        /// <summary>
        ///     Configures Role mappings between Data Model and Representer
        /// </summary>
        private static void ConfigureRoleModelMappings()
        {
            //RoleRepresenter => RoleDataModel
            Mapper.CreateMap<RoleRepresenter, Role>()
                //Maps RoleRepresenter.UserRepresenter => RoleDataModel.UserDataModel
                .ForMember(roleDataModel => roleDataModel.Users,
                    options => options.MapFrom(roleRepresenter => roleRepresenter.UserRepresenters));

            //RoleDataModel => RoleRepresenter
            Mapper.CreateMap<Role, RoleRepresenter>()
                //Maps RoleDataModel.UserDataModel => RoleRepresenter.UserRepresenter
                .ForMember(roleRepresenter => roleRepresenter.UserRepresenters,
                    options => options.MapFrom(roleDataModel => roleDataModel.Users));
        }

        /// <summary>
        ///     Configures User mappings between Data Model and Representer
        /// </summary>
        private static void ConfigureUserModelMappings()
        {
            //UserRepresenter => UserDataModel
            Mapper.CreateMap<UserRepresenter, User>()
                //Maps UserRepresenter.RoleRepresenter => UserDataModel.RoleDataModel
                .ForMember(userDataModel => userDataModel.Roles,
                    options => options.MapFrom(userRepresenter => userRepresenter.RoleRepresenters));

            //UserDataModel => UserRepresenter
            Mapper.CreateMap<User, UserRepresenter>()
                //Maps UserDataModel.RoleDataModel => UserRepresenter.RoleRepresenter
                .ForMember(userRepresenter => userRepresenter.RoleRepresenters,
                    options => options.MapFrom(userDataModel => userDataModel.Roles));
        }
    }
}