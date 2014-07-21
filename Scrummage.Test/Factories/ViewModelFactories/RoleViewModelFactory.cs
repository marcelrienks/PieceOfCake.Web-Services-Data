using System.Collections.Generic;
using Scrummage.Web.ViewModels;

namespace Scrummage.Test.Factories.ViewModelFactories
{
    internal class RoleViewModelFactory
    {
        private readonly RoleViewModel _roleViewModel;
        private readonly List<RoleViewModel> _roleViewModels;

        /// <summary>
        ///     Create default RoleViewModel and RoleViewModel list objects
        /// </summary>
        public RoleViewModelFactory()
        {
            _roleViewModel = new RoleViewModel
            {
                Id = 0,
                Title = "Title",
                Description = "Description"
            };

            _roleViewModels = new List<RoleViewModel>
            {
                _roleViewModel
            };
        }

        /// <summary>
        ///     Return constructed RoleViewModel
        /// </summary>
        /// <returns>Role</returns>
        public RoleViewModel Build()
        {
            return _roleViewModel;
        }

        /// <summary>
        ///     Return constructed RoleViewModel List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<RoleViewModel> BuildList()
        {
            return _roleViewModels;
        }

        /// <summary>
        ///     Creates an Invalid Role view model with null fields
        /// </summary>
        /// <returns></returns>
        public RoleViewModelFactory WithNullRequiredFields()
        {
            _roleViewModel.Title = null;
            return this;
        }

        /// <summary>
        ///     Creates an Invalid Role view model
        /// </summary>
        /// <returns></returns>
        public RoleViewModelFactory WithInvalidFields()
        {
            _roleViewModel.Title = "a";
            _roleViewModel.Description = "a";
            return this;
        }
    }
}