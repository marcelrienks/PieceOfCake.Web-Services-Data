using AutoMapper;
using Scrummage.DataAccess.Models;
using Scrummage.ViewModels;

namespace Scrummage
{
    public class AutoMapperConfig
    {
        /// <summary>
        ///     Configures Mappings between models and view models
        /// </summary>
        public static void ConfigureMappings()
        {
            ConfigureRoleModelMappings();
            ConfigureMemberModelMappings();
            ConfigureAvatarModelMappings();
        }

        /// <summary>
        ///     Configures mappings between Role and RoleViewModel
        /// </summary>
        private static void ConfigureRoleModelMappings()
        {
            //Role => RoleViewModel
            Mapper.CreateMap<Role, RoleViewModel>()
                //Maps Role.Member => RoleViewModel.MemberViewModels
                .ForMember(roleViewModel => roleViewModel.MemberViewModels,
                    options => options.MapFrom(role => role.Members));

            //RoleViewModel => Role
            Mapper.CreateMap<RoleViewModel, Role>()
                //Maps RoleViewModel.MemberViewModels => Role.Member
                .ForMember(role => role.Members,
                    options => options.MapFrom(roleViewModel => roleViewModel.MemberViewModels));
        }

        /// <summary>
        ///     Configures mappings between Member and MemberViewModel
        /// </summary>
        private static void ConfigureMemberModelMappings()
        {
            //Member => MemberViewModel
            Mapper.CreateMap<Member, MemberViewModel>()
                //Maps Member.Role => MemberViewModel.RoleViewModel
                .ForMember(memberViewModel => memberViewModel.RoleViewModels,
                    options => options.MapFrom(member => member.Roles))
                //Maps Member.Avatar => MemberViewModel.AvatarViewModel
                .ForMember(memberViewModel => memberViewModel.AvatarViewModel,
                    options => options.MapFrom(member => member.Avatar))
                //Maps Member.Password => MemberViewModel.ConfirmPassword
                .ForMember(memberViewModel => memberViewModel.ConfirmPassword,
                    options => options.MapFrom(member => member.Password));

            //MemberViewModel => Member
            Mapper.CreateMap<MemberViewModel, Member>()
                //Maps MemberViewModel.RoleViewModel => Member.Role
                .ForMember(member => member.Roles,
                    options => options.MapFrom(memberViewModel => memberViewModel.RoleViewModels))
                //Maps MemberViewModel.AvatarViewModel => Member.Avatar
                .ForMember(member => member.Avatar,
                    options => options.MapFrom(memberViewModel => memberViewModel.AvatarViewModel));
        }

        /// <summary>
        ///     Configures mappings between Avatar and AvatarViewModel
        /// </summary>
        private static void ConfigureAvatarModelMappings()
        {
            //Avatar => AvatarViewModel
            Mapper.CreateMap<Avatar, AvatarViewModel>()
                //Maps Avatar.Member => AvatarViewModel.AvatarViewModel
                .ForMember(avatarViewModel => avatarViewModel.MemberModelView,
                    options => options.MapFrom(avater => avater.Member));

            //AvatarViewModel => Avatar
            Mapper.CreateMap<AvatarViewModel, Avatar>()
                //Maps AvatarViewModel.AvatarViewModel => Avatar.Member
                .ForMember(avater => avater.Member,
                    options => options.MapFrom(avatarViewModel => avatarViewModel.MemberModelView));
        }
    }
}