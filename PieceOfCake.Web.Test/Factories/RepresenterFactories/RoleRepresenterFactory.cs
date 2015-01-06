using System.Collections.Generic;
using PieceOfCake.Web.Representer;

namespace PieceOfCake.Web.Test.Factories.RepresenterFactories
{
    internal class RoleRepresenterFactory
    {
        private readonly RoleRepresenter _roleViewModel;
        private readonly List<RoleRepresenter> _roleViewModels;

        /// <summary>
        ///     Create default RoleViewModel and RoleViewModel list objects
        /// </summary>
        public RoleRepresenterFactory()
        {
            _roleViewModel = new RoleRepresenter
            {
                Id = 0,
                Title = "Title",
                Description = "Description"
            };

            _roleViewModels = new List<RoleRepresenter>
            {
                _roleViewModel
            };
        }

        /// <summary>
        ///     Return constructed RoleViewModel
        /// </summary>
        /// <returns>Role</returns>
        public RoleRepresenter Build()
        {
            return _roleViewModel;
        }

        /// <summary>
        ///     Return constructed RoleViewModel List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<RoleRepresenter> BuildList()
        {
            return _roleViewModels;
        }

        /// <summary>
        ///     Creates an Invalid Role view model with null fields
        /// </summary>
        /// <returns></returns>
        public RoleRepresenterFactory WithNullRequiredFields()
        {
            _roleViewModel.Title = null;
            return this;
        }

        /// <summary>
        ///     Creates an Invalid Role view model
        /// </summary>
        /// <returns></returns>
        public RoleRepresenterFactory WithInvalidFields()
        {
            _roleViewModel.Title = "a";
            _roleViewModel.Description = "a";
            return this;
        }
    }
}