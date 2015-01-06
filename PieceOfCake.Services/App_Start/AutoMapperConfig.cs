using AutoMapper;
using PieceOfCake.Data.Models;
using PieceOfCake.Services.Representors;

namespace PieceOfCake.Services
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
                .ForMember(dmRole => dmRole.Users,
                    options => options.MapFrom(vmRole => vmRole.Users));

            //RoleDataModel => RoleRepresenter
            Mapper.CreateMap<Role, RoleRepresentor>()
                //Maps RoleDataModel.UserDataModel => RoleRepresenter.UserRepresenter
                .ForMember(vmRole => vmRole.Users,
                    options => options.MapFrom(dmRole => dmRole.Users));
        }

        /// <summary>
        ///     Configures User mappings between Data Model and Representer
        /// </summary>
        private static void ConfigureUserModelMappings()
        {
            //UserRepresenter => UserDataModel
            Mapper.CreateMap<UserRepresentor, User>()
                //Maps UserRepresenter.RoleRepresenter => UserDataModel.RoleDataModel
                .ForMember(dmUser => dmUser.Roles,
                    options => options.MapFrom(vmUser => vmUser.Roles));

            //UserDataModel => UserRepresenter
            Mapper.CreateMap<User, UserRepresentor>()
                //Maps UserDataModel.RoleDataModel => UserRepresenter.RoleRepresenter
                .ForMember(vmUser => vmUser.Roles,
                    options => options.MapFrom(dmUser => dmUser.Roles));
        }
    }
}