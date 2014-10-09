using System.Collections.Generic;
using Scrummage.Services.ViewModels;

namespace Scrummage.Services.Test.Factories
{
    public class UserFactory
    {
        private User _user;
        private List<User> _users;

        /// <summary>
        ///     Create default UserViewModel and UserList objects
        /// </summary>
        public UserFactory()
        {
            CreateDefaultUserAndUserList(0);
        }

        /// <summary>
        ///     Create default UserViewModel and UserList objects with given UserViewModel Id
        /// </summary>
        /// <param name="UserId"></param>
        public UserFactory(int UserId)
        {
            CreateDefaultUserAndUserList(UserId);
        }

        /// <summary>
        ///     Create default UserViewModel and UserList objects with given UserViewModel Id
        /// </summary>
        /// <param name="userId"></param>
        private void CreateDefaultUserAndUserList(int userId)
        {
            _user = new User
            {
                Id = userId,
                Name = "Name",
                ShortName = "sn",
                Username = "Username",
                Email = "email@address.com",
                Roles = new RoleFactory().BuildList(),
                Avatar = new Avatar
                {
                    Id = userId,
                    Image = new byte[0]
                }
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
        /// <returns>List<UserViewModel></returns>
        public List<User> BuildList()
        {
            return _users;
        }

        /// <summary>
        ///     Create an Extended User List with two items
        /// </summary>
        /// <returns></returns>
        public UserFactory WithExtendedList(int userId)
        {
            _users.Add(new User
            {
                Id = userId,
                Name = "Name2",
                ShortName = "sn2",
                Username = "Username2",
                Email = "email2@address.com",
                Roles = new RoleFactory().BuildList(),
                Avatar = new Avatar
                {
                    Id = userId,
                    Image = new byte[0]
                }
            });
            return this;
        }
    }
}