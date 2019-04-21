using System.Collections.Generic;
using Data.Models;

namespace Web.Test.Factories.ModelFactories
{
    public class UserFactory
    {
        private User _user;
        private List<User> _users;

        /// <summary>
        ///     Create default User and UserList objects
        /// </summary>
        public UserFactory()
        {
            CreateDefaultUserAndUserList(0);
        }

        /// <summary>
        ///     Create default User and UserList objects with given User Id
        /// </summary>
        /// <param name="userId"></param>
        public UserFactory(int userId)
        {
            CreateDefaultUserAndUserList(userId);
        }

        /// <summary>
        ///     Create default User and UserList objects with given User Id
        /// </summary>
        /// <param name="userId"></param>
        private void CreateDefaultUserAndUserList(int userId)
        {
            _user = new User
            {
                Id = userId,
                Name = "Name",
                Username = "Username",
                Email = "email@address.com",
                Roles = new RoleFactory().BuildList()
            };

            _users = new List<User>
            {
                _user
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public User Build()
        {
            return _user;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> BuildList()
        {
            return _users;
        }
    }
}