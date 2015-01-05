using AutoMapper;
using PieceOfCake.Data.Models;
using PieceOfCake.Services.Representors;

namespace PieceOfCake.Services
{
    public class AutoMapperConfig
    {
        /// <summary>
        ///     Configures Mappings between Data Model and View Model
        /// </summary>
        public static void ConfigureMappings()
        {
            ConfigureRoleModelMappings();
            ConfigureUserModelMappings();
            ConfigureAvatarModelMappings();
        }

        /// <summary>
        ///     Configures Role mappings between Data Model and View Model
        /// </summary>
        private static void ConfigureRoleModelMappings()
        {
            //ViewModelRole => DataModelRole
            Mapper.CreateMap<RoleRepresentor, Role>()
                //Maps ViewModelRole.ViewModelUser => DataModelRole.DataModelUsers
                .ForMember(dmRole => dmRole.Users,
                    options => options.MapFrom(vmRole => vmRole.Users));

            //RoleDataModel => ViewModelRole
            Mapper.CreateMap<Role, RoleRepresentor>()
                //Maps DataModelRole.DataModelUsers => ViewModelRole.ViewModelUser
                .ForMember(vmRole => vmRole.Users,
                    options => options.MapFrom(dmRole => dmRole.Users));
        }

        /// <summary>
        ///     Configures User mappings between Data Model and View Model
        /// </summary>
        private static void ConfigureUserModelMappings()
        {
            //ViewModelUser => UserDataModel
            Mapper.CreateMap<UserRepresentor, User>()
                //Maps ViewModelUser.ViewModelRole => DataModelUser.DataModelRoles
                .ForMember(dmUser => dmUser.Roles,
                    options => options.MapFrom(vmUser => vmUser.Roles))
                //Maps ViewModelUser.ViewModelAvatar => DataModelUser.DataModelAvatar
                .ForMember(dmUser => dmUser.Avatar,
                    options => options.MapFrom(vmUser => vmUser.Avatar));

            //UserDataModel => ViewModelUser
            Mapper.CreateMap<User, UserRepresentor>()
                //Maps DataModelUser.DataModelRole => ViewModelUser.ViewModelRole
                .ForMember(vmUser => vmUser.Roles,
                    options => options.MapFrom(dmUser => dmUser.Roles))
                //Maps DataModelUser.DataModelAvatar => ViewModelUser.ViewModelAvatar
                .ForMember(vmUser => vmUser.Avatar,
                    options => options.MapFrom(dmUser => dmUser.Avatar));
        }

        /// <summary>
        ///     Configures Avatar mappings between Data Model and View Model
        /// </summary>
        private static void ConfigureAvatarModelMappings()
        {
            //ViewModelAvatar => DataModelAvatar
            Mapper.CreateMap<AvatarRepresentor, Avatar>()
                //Maps ViewModelAvatar.ViewModelUser => DataModelAvatar.DataModelUser
                .ForMember(dmAvatar => dmAvatar.User,
                    options => options.MapFrom(vmAvater => vmAvater.User));

            //DataModelAvatar => ViewModelAvatar
            Mapper.CreateMap<Avatar, AvatarRepresentor>()
                //Maps DataModelAvatar.DataModelUser => ViewModelAvatar.ViewModelUser
                .ForMember(vmAvater => vmAvater.User,
                    options => options.MapFrom(dmAvatar => dmAvatar.User));
        }
    }
}