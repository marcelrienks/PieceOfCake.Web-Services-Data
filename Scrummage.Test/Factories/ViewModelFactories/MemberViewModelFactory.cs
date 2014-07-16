using System.Collections.Generic;
using Scrummage.Web.ViewModels;

namespace Scrummage.Test.Factories.ViewModelFactories
{
    public class MemberViewModelFactory
    {
        private readonly MemberViewModel _memberViewModel;
        private readonly List<MemberViewModel> _memberViewModels;

        /// <summary>
        ///     Create default RoleViewModel and RoleViewModel list objects
        /// </summary>
        public MemberViewModelFactory()
        {
            _memberViewModel = new MemberViewModel
            {
                MemberId = 0,
                Name = "Name",
                ShortName = "sn",
                Username = "Username",
                Email = "email@address.com",
                Password = "password",
                ConfirmPassword = "password",
                RoleViewModels = new RoleViewModelFactory().BuildList(),
                AvatarViewModel = new AvatarViewModel
                {
                    MemberId = 0,
                    Image = new byte[0]
                }
            };

            _memberViewModels = new List<MemberViewModel>
            {
                _memberViewModel
		    };
        }

        /// <summary>
        ///     Return constructed RoleViewModel
        /// </summary>
        /// <returns>Role</returns>
        public MemberViewModel Build()
        {
            return _memberViewModel;
        }

        /// <summary>
        ///     Return constructed RoleViewModel List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<MemberViewModel> BuildList()
        {
            return _memberViewModels;
        }

        /// <summary>
        ///     Creates an Invalid Member view model with null fields
        /// </summary>
        /// <returns></returns>
        public MemberViewModelFactory WithNullRequiredFields()
        {
            _memberViewModel.Name = null;
            _memberViewModel.ShortName = null;
            _memberViewModel.Username = null;
            _memberViewModel.Email = null;
            _memberViewModel.Password = null;
            _memberViewModel.ConfirmPassword = null;
            return this;
        }

        /// <summary>
        ///     Creates an Invalid Member view model
        /// </summary>
        /// <returns></returns>
        public MemberViewModelFactory WithInvalidFields()
        {
            _memberViewModel.Name = "a";
            _memberViewModel.ShortName = "a";
            _memberViewModel.Username = "a";
            _memberViewModel.Email = "a";
            _memberViewModel.Password = "a";
            _memberViewModel.ConfirmPassword = "b";
            return this;
        }
    }
}
