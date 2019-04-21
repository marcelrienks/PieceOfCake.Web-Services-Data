using AutoMapper;
using Data.Models;
using Services.Representors;

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
            Mapper.CreateMap<RoleRepresentor, Role>()
                //Maps RoleRepresenter.UserRepresenter => RoleDataModel.UserDataModel
                .ForMember(roleDataModel => roleDataModel.Users,
                    options => options.MapFrom(roleRepresenter => roleRepresenter.UserRepresentors));

            //RoleDataModel => RoleRepresenter
            Mapper.CreateMap<Role, RoleRepresentor>()
                //Maps RoleDataModel.UserDataModel => RoleRepresenter.UserRepresenter
                .ForMember(roleRepresenter => roleRepresenter.UserRepresentors,
                    options => options.MapFrom(roleDataModel => roleDataModel.Users));
        }

        /// <summary>
        ///     Configures User mappings between Data Model and Representer
        /// </summary>
        private static void ConfigureUserModelMappings()
        {
            //UserRepresenter => UserDataModel
            Mapper.CreateMap<UserRepresentor, User>()
                //Maps UserRepresenter.RoleRepresenter => UserDataModel.RoleDataModel
                .ForMember(userDataModel => userDataModel.Roles,
                    options => options.MapFrom(userRepresenter => userRepresenter.RoleRepresentors));

            //UserDataModel => UserRepresenter
            Mapper.CreateMap<User, UserRepresentor>()
                //Maps UserDataModel.RoleDataModel => UserRepresenter.RoleRepresenter
                .ForMember(userRepresenter => userRepresenter.RoleRepresentors,
                    options => options.MapFrom(userDataModel => userDataModel.Roles));
        }
    }
}