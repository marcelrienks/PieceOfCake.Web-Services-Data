using System.Collections.Generic;
using Services.Representers;

namespace Services.Test.Factories
{
    public class UserFactory
    {
        private UserRepresenter _user;
        private List<UserRepresenter> _users;

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
        /// <param name="userId"></param>
        public UserFactory(int userId)
        {
            CreateDefaultUserAndUserList(userId);
        }

        /// <summary>
        ///     Create default UserViewModel and UserList objects with given UserViewModel Id
        /// </summary>
        /// <param name="userId"></param>
        private void CreateDefaultUserAndUserList(int userId)
        {
            _user = new UserRepresenter
            {
                Id = userId,
                Name = "Name",
                Username = "Username",
                Email = "email@address.com",
                RoleRepresentors = new RoleFactory().BuildList()
            };

            _users = new List<UserRepresenter>
            {
                _user
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public UserRepresenter Build()
        {
            return _user;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<UserRepresentor></returns>
        public List<UserRepresenter> BuildList()
        {
            return _users;
        }

        /// <summary>
        ///     Create an Extended User List with two items
        /// </summary>
        /// <returns></returns>
        public UserFactory WithExtendedList(int userId)
        {
            _users.Add(new UserRepresenter
            {
                Id = userId,
                Name = "Name2",
                Username = "Username2",
                Email = "email2@address.com",
                RoleRepresentors = new RoleFactory().BuildList()
            });
            return this;
        }
    }
}