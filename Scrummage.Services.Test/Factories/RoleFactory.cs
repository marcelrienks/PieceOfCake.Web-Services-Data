using System.Collections.Generic;
using PieceOfCake.Services.Representors;

namespace PieceOfCake.Services.Test.Factories
{
    public class RoleFactory
    {
        private readonly RoleRepresentor _role;
        private readonly List<RoleRepresentor> _roles;

        /// <summary>
        ///     Create default Role and RoleList objects
        /// </summary>
        public RoleFactory()
        {
            _role = new RoleRepresentor
            {
                Id = 0,
                Title = "Title",
                Description = "Description"
            };

            _roles = new List<RoleRepresentor>
            {
                _role
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public RoleRepresentor Build()
        {
            return _role;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<Role></returns>
        public List<RoleRepresentor> BuildList()
        {
            return _roles;
        }

        /// <summary>
        ///     Create an Extended Role List with two items
        /// </summary>
        /// <returns></returns>
        public RoleFactory WithExtendedList()
        {
            _roles.Add(new RoleRepresentor
            {
                Id = 1,
                Title = "Title1",
                Description = "Description1"
            });
            return this;
        }
    }
}