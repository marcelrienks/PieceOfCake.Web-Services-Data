using System.Collections.Generic;
using Scrummage.Web.ViewModels;

namespace Scrummage.Test.Factories.ViewModelFactories
{
    public class UserViewModelFactory
    {
        private readonly UserViewModel _UserViewModel;
        private readonly List<UserViewModel> _UserViewModels;

        /// <summary>
        ///     Create default RoleViewModel and RoleViewModel list objects
        /// </summary>
        public UserViewModelFactory()
        {
            _UserViewModel = new UserViewModel
            {
                Id = 0,
                Name = "Name",
                ShortName = "sn",
                Username = "Username",
                Email = "email@address.com",
                Password = "password",
                ConfirmPassword = "password",
                RoleViewModels = new RoleViewModelFactory().BuildList(),
                AvatarViewModel = new AvatarViewModel
                {
                    Id = 0,
                    Image = new byte[0]
                }
            };

            _UserViewModels = new List<UserViewModel>
            {
                _UserViewModel
		    };
        }

        /// <summary>
        ///     Return constructed RoleViewModel
        /// </summary>
        /// <returns>Role</returns>
        public UserViewModel Build()
        {
            return _UserViewModel;
        }

        /// <summary>
        ///     Return constructed RoleViewModel List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<UserViewModel> BuildList()
        {
            return _UserViewModels;
        }

        /// <summary>
        ///     Creates an Invalid User view model with null fields
        /// </summary>
        /// <returns></returns>
        public UserViewModelFactory WithNullRequiredFields()
        {
            _UserViewModel.Name = null;
            _UserViewModel.ShortName = null;
            _UserViewModel.Username = null;
            _UserViewModel.Email = null;
            _UserViewModel.Password = null;
            _UserViewModel.ConfirmPassword = null;
            return this;
        }

        /// <summary>
        ///     Creates an Invalid User view model
        /// </summary>
        /// <returns></returns>
        public UserViewModelFactory WithInvalidFields()
        {
            _UserViewModel.Name = "a";
            _UserViewModel.ShortName = "a";
            _UserViewModel.Username = "a";
            _UserViewModel.Email = "a";
            _UserViewModel.Password = "a";
            _UserViewModel.ConfirmPassword = "b";
            return this;
        }
    }
}
