using Scrummage.Data.Models;
using System.Collections.Generic;

namespace Scrummage.Test.Factories.ModelFactories
{
    public class UserFactory
    {
        private User _User;
        private List<User> _Users;

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
        /// <param name="UserId"></param>
        public UserFactory(int UserId)
        {
            CreateDefaultUserAndUserList(UserId);
        }

        /// <summary>
        ///     Create default User and UserList objects with given User Id
        /// </summary>
        /// <param name="UserId"></param>
        private void CreateDefaultUserAndUserList(int UserId)
        {
            _User = new User
            {
                Id = UserId,
                Name = "Name",
                ShortName = "sn",
                Username = "Username",
                Email = "email@address.com",
                Roles = new RoleFactory().BuildList(),
                Avatar = new Avatar
                {
                    Id = UserId,
                    Image = new byte[0]
                }
            };

            _Users = new List<User>
            {
                _User
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public User Build()
        {
            return _User;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> BuildList()
        {
            return _Users;
        }
    }
}
