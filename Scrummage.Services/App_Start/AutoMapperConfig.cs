using AutoMapper;
using DmAvatar = Scrummage.Data.Models.Avatar;
using DmRole = Scrummage.Data.Models.Role;
using DmUser = Scrummage.Data.Models.User;
using VmAvatar = Scrummage.Services.ViewModels.Avatar;
using VmRole = Scrummage.Services.ViewModels.Role;
using VmUser = Scrummage.Services.ViewModels.User;

namespace Scrummage.Services
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
            Mapper.CreateMap<VmRole, DmRole>()
                //Maps ViewModelRole.ViewModelUser => DataModelRole.DataModelUsers
                .ForMember(dmRole => dmRole.Users,
                    options => options.MapFrom(vmRole => vmRole.Users));

            //RoleDataModel => ViewModelRole
            Mapper.CreateMap<DmRole, VmRole>()
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
            Mapper.CreateMap<VmUser, DmUser>()
                //Maps ViewModelUser.ViewModelRole => DataModelUser.DataModelRoles
                .ForMember(dmUser => dmUser.Roles,
                    options => options.MapFrom(vmUser => vmUser.Roles))
                //Maps ViewModelUser.ViewModelAvatar => DataModelUser.DataModelAvatar
                .ForMember(dmUser => dmUser.Avatar,
                    options => options.MapFrom(vmUser => vmUser.Avatar));

            //UserDataModel => ViewModelUser
            Mapper.CreateMap<DmUser, VmUser>()
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
            Mapper.CreateMap<VmAvatar, DmAvatar>()
                //Maps ViewModelAvatar.ViewModelUser => DataModelAvatar.DataModelUser
                .ForMember(dmAvatar => dmAvatar.User,
                    options => options.MapFrom(vmAvater => vmAvater.User));

            //DataModelAvatar => ViewModelAvatar
            Mapper.CreateMap<DmAvatar, VmAvatar>()
                //Maps DataModelAvatar.DataModelUser => ViewModelAvatar.ViewModelUser
                .ForMember(vmAvater => vmAvater.User,
                    options => options.MapFrom(dmAvatar => dmAvatar.User));
        }
    }
}