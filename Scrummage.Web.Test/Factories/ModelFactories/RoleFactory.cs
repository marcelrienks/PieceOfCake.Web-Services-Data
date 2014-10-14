using System.Collections.Generic;
using Scrummage.Data.Models;

namespace Scrummage.Web.Test.Factories.ModelFactories
{
    public class RoleFactory
    {
        private readonly Role _role;
        private readonly List<Role> _roles;

        /// <summary>
        ///     Create default Role and RoleList objects
        /// </summary>
        public RoleFactory()
        {
            _role = new Role
            {
                Id = 0,
                Title = "Title",
                Description = "Description"
            };

            _roles = new List<Role>
            {
                _role
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public Role Build()
        {
            return _role;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<Role> BuildList()
        {
            return _roles;
        }

        /// <summary>
        ///     Create an Extended Role List with two items
        /// </summary>
        /// <returns></returns>
        public RoleFactory WithExtendedList()
        {
            _roles.Add(new Role
            {
                Id = 1,
                Title = "Title1",
                Description = "Description1"
            });
            return this;
        }
    }
}