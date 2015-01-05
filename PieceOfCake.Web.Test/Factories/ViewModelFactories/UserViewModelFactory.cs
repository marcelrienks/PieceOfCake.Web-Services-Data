using System.Collections.Generic;
using PieceOfCake.Web.ViewModels;

namespace PieceOfCake.Web.Test.Factories.ViewModelFactories
{
    public class UserViewModelFactory
    {
        private readonly UserViewModel _userViewModel;
        private readonly List<UserViewModel> _userViewModels;

        /// <summary>
        ///     Create default RoleViewModel and RoleViewModel list objects
        /// </summary>
        public UserViewModelFactory()
        {
            _userViewModel = new UserViewModel
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

            _userViewModels = new List<UserViewModel>
            {
                _userViewModel
            };
        }

        /// <summary>
        ///     Return constructed RoleViewModel
        /// </summary>
        /// <returns>Role</returns>
        public UserViewModel Build()
        {
            return _userViewModel;
        }

        /// <summary>
        ///     Return constructed RoleViewModel List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<UserViewModel> BuildList()
        {
            return _userViewModels;
        }

        /// <summary>
        ///     Creates an Invalid User view model with null fields
        /// </summary>
        /// <returns></returns>
        public UserViewModelFactory WithNullRequiredFields()
        {
            _userViewModel.Name = null;
            _userViewModel.ShortName = null;
            _userViewModel.Username = null;
            _userViewModel.Email = null;
            _userViewModel.Password = null;
            _userViewModel.ConfirmPassword = null;
            return this;
        }

        /// <summary>
        ///     Creates an Invalid User view model
        /// </summary>
        /// <returns></returns>
        public UserViewModelFactory WithInvalidFields()
        {
            _userViewModel.Name = "a";
            _userViewModel.ShortName = "a";
            _userViewModel.Username = "a";
            _userViewModel.Email = "a";
            _userViewModel.Password = "a";
            _userViewModel.ConfirmPassword = "b";
            return this;
        }
    }
}