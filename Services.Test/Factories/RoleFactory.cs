using System.Collections.Generic;
using Services.Representers;

namespace Services.Test.Factories
{
    public class RoleFactory
    {
        private readonly RoleRepresenter _role;
        private readonly List<RoleRepresenter> _roles;

        /// <summary>
        ///     Create default Role and RoleList objects
        /// </summary>
        public RoleFactory()
        {
            _role = new RoleRepresenter
            {
                Id = 0,
                Title = "Title",
                Description = "Description"
            };

            _roles = new List<RoleRepresenter>
            {
                _role
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public RoleRepresenter Build()
        {
            return _role;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<RoleRepresenter> BuildList()
        {
            return _roles;
        }

        /// <summary>
        ///     Create an Extended Role List with two items
        /// </summary>
        /// <returns></returns>
        public RoleFactory WithExtendedList()
        {
            _roles.Add(new RoleRepresenter
            {
                Id = 1,
                Title = "Title1",
                Description = "Description1"
            });
            return this;
        }
    }
}