using System.Collections.Generic;
using VmRole = Scrummage.Services.ViewModels.Role;

namespace Scrummage.Services.Test.Factories
{
    public class RoleFactory
    {
        private readonly VmRole _role;
        private readonly List<VmRole> _roles;

        /// <summary>
        ///     Create default Role and RoleList objects
        /// </summary>
        public RoleFactory()
        {
            _role = new VmRole
            {
                Id = 0,
                Title = "Title",
                Description = "Description"
            };

            _roles = new List<VmRole>
            {
                _role
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public VmRole Build()
        {
            return _role;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<VmRole> BuildList()
        {
            return _roles;
        }

        /// <summary>
        ///     Create an Extended Role List with two items
        /// </summary>
        /// <returns></returns>
        public RoleFactory WithExtendedList()
        {
            _roles.Add(new VmRole
            {
                Id = 1,
                Title = "Title1",
                Description = "Description1"
            });
            return this;
        }
    }
}