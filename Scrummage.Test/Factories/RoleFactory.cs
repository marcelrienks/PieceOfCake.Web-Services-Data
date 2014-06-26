using System.Collections.Generic;
using Scrummage.Models;

namespace Scrummage.Test.Factories
{
    public static class RoleFactory
    {
        /// <summary>
        ///     Default Role model
        /// </summary>
        private static readonly Role DefaultRole = new Role
        {
            RoleId = 0,
            Title = "Title",
            Description = "Description"
        };

        private static readonly Role DefaultRole1 = new Role
        {
            RoleId = 1,
            Title = "Title1",
            Description = "Description1"
        };

        /// <summary>
        ///     Returns a list containing one default Role model
        /// </summary>
        /// <returns></returns>
        public static List<Role> CreateDefaultRoleList()
        {
            return new List<Role>
            {
                DefaultRole
            };
        }

        public static List<Role> CreateExtendedRoleList()
        {
            return new List<Role>
            {
                DefaultRole,
                DefaultRole1
            };
        }

        /// <summary>
        ///     Returns a default Role model
        /// </summary>
        /// <returns></returns>
        public static Role CreateDefaultRole()
        {
            return DefaultRole;
        }
    }
}