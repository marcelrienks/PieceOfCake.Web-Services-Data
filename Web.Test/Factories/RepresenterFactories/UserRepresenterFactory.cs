using System.Collections.Generic;
using Web.Representer;

namespace Web.Test.Factories.RepresenterFactories
{
    public class UserRepresenterFactory
    {
        private readonly UserRepresenter _userViewModel;
        private readonly List<UserRepresenter> _userViewModels;

        /// <summary>
        ///     Create default RoleViewModel and RoleViewModel list objects
        /// </summary>
        public UserRepresenterFactory()
        {
            _userViewModel = new UserRepresenter
            {
                Id = 0,
                Name = "Name",
                Username = "Username",
                Email = "email@address.com",
                Password = "password",
                ConfirmPassword = "password",
                RoleRepresenters = new RoleRepresenterFactory().BuildList(),
            };

            _userViewModels = new List<UserRepresenter>
            {
                _userViewModel
            };
        }

        /// <summary>
        ///     Return constructed RoleViewModel
        /// </summary>
        /// <returns>UserRepresenter</returns>
        public UserRepresenter Build()
        {
            return _userViewModel;
        }

        /// <summary>
        ///     Return constructed RoleViewModel List
        /// </summary>
        /// <returns>List<UserRepresenter></returns>
        public List<UserRepresenter> BuildList()
        {
            return _userViewModels;
        }

        /// <summary>
        ///     Creates an Invalid User view model with null fields
        /// </summary>
        /// <returns></returns>
        public UserRepresenterFactory WithNullRequiredFields()
        {
            _userViewModel.Name = null;
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
        public UserRepresenterFactory WithInvalidFields()
        {
            _userViewModel.Name = "a";
            _userViewModel.Username = "a";
            _userViewModel.Email = "a";
            _userViewModel.Password = "a";
            _userViewModel.ConfirmPassword = "b";
            return this;
        }
    }
}