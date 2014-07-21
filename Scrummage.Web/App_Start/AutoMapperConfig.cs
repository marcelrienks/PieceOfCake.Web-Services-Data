using AutoMapper;
using Scrummage.Data.Models;
using Scrummage.Web.ViewModels;

namespace Scrummage.Web
{
    public class AutoMapperConfig
    {
        /// <summary>
        ///     Configures Mappings between models and view models
        /// </summary>
        public static void ConfigureMappings()
        {
            ConfigureRoleModelMappings();
            ConfigureUserModelMappings();
            ConfigureAvatarModelMappings();
        }

        /// <summary>
        ///     Configures mappings between Role and RoleViewModel
        /// </summary>
        private static void ConfigureRoleModelMappings()
        {
            //Role => RoleViewModel
            Mapper.CreateMap<Role, RoleViewModel>()
                //Maps Role.User => RoleViewModel.UserViewModels
                .ForMember(roleViewModel => roleViewModel.UserViewModels,
                    options => options.MapFrom(role => role.Users));

            //RoleViewModel => Role
            Mapper.CreateMap<RoleViewModel, Role>()
                //Maps RoleViewModel.UserViewModels => Role.User
                .ForMember(role => role.Users,
                    options => options.MapFrom(roleViewModel => roleViewModel.UserViewModels));
        }

        /// <summary>
        ///     Configures mappings between User and UserViewModel
        /// </summary>
        private static void ConfigureUserModelMappings()
        {
            //User => UserViewModel
            Mapper.CreateMap<User, UserViewModel>()
                //Maps User.Role => UserViewModel.RoleViewModel
                .ForMember(userViewModel =>userViewModel.RoleViewModels,
                    options => options.MapFrom(user => user.Roles))
                //Maps User.Avatar => UserViewModel.AvatarViewModel
                .ForMember(userViewModel => userViewModel.AvatarViewModel,
                    options => options.MapFrom(user => user.Avatar))
                //Maps User.Password => UserViewModel.ConfirmPassword
                .ForMember(userViewModel => userViewModel.ConfirmPassword,
                    options => options.MapFrom(user => user.Password));

            //UserViewModel => User
            Mapper.CreateMap<UserViewModel, User>()
                //MapsUserViewModel.RoleViewModel => User.Role
                .ForMember(user => user.Roles,
                    options => options.MapFrom(userViewModel => userViewModel.RoleViewModels))
                //Maps UserViewModel.AvatarViewModel => User.Avatar
                .ForMember(user => user.Avatar,
                    options => options.MapFrom(userViewModel => userViewModel.AvatarViewModel));
        }

        /// <summary>
        ///     Configures mappings between Avatar and AvatarViewModel
        /// </summary>
        private static void ConfigureAvatarModelMappings()
        {
            //Avatar => AvatarViewModel
            Mapper.CreateMap<Avatar, AvatarViewModel>()
                //Maps Avatar.User => AvatarViewModel.AvatarViewModel
                .ForMember(avatarViewModel => avatarViewModel.UserModelView,
                    options => options.MapFrom(avater => avater.User));

            //AvatarViewModel => Avatar
            Mapper.CreateMap<AvatarViewModel, Avatar>()
                //Maps AvatarViewModel.AvatarViewModel => Avatar.User
                .ForMember(avater => avater.User,
                    options => options.MapFrom(avatarViewModel => avatarViewModel.UserModelView));
        }
    }
}