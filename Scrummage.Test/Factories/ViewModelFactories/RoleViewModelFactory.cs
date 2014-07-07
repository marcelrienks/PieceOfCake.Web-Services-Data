using Scrummage.ViewModels;
using System.Collections.Generic;

namespace Scrummage.Test.Factories.ViewModelFactories
{
    class RoleViewModelFactory
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
                RoleId = 0,
                Title = null,
                Description = null
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
        ///     Creates an Invalid Role with empty Title and Description
        /// </summary>
        /// <returns></returns>
        public RoleViewModelFactory WithInvalidTitleDescription()
        {
            _roleViewModel.Title = "";
            _roleViewModel.Description = "";
            return this;
        }
    }
}
