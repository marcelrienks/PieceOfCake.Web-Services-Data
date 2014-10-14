using System.Collections.Generic;
using VmAvatar = Scrummage.Services.Representors.AvatarRepresentor;

namespace Scrummage.Services.Test.Factories
{
    public class AvatarFactory
    {
        private VmAvatar _avatar;
        private List<VmAvatar> _avatars;

        /// <summary>
        ///     Create default Avatar and AvatarList objects
        /// </summary>
        public AvatarFactory()
        {
            CreateDefaultUserAndUserList(0);
        }

        /// <summary>
        ///     Create default Avatar and AvatarList objects with given UserViewModel Id
        /// </summary>
        /// <param name="avatarId"></param>
        public AvatarFactory(int avatarId)
        {
            CreateDefaultUserAndUserList(avatarId);
        }

        /// <summary>
        ///     Create default Avatar and AvatarList objects with given UserViewModel Id
        /// </summary>
        /// <param name="avatarId"></param>
        private void CreateDefaultUserAndUserList(int avatarId)
        {
            _avatar = new VmAvatar
            {
                Id = avatarId,
                Image = new byte[0]
            };

            _avatars = new List<VmAvatar>
            {
                _avatar
            };
        }

        /// <summary>
        ///     Return constructed Avatar
        /// </summary>
        /// <returns>Avatar</returns>
        public VmAvatar Build()
        {
            return _avatar;
        }

        /// <summary>
        ///     Return constructed Avatar List
        /// </summary>
        /// <returns>List<Avatar></returns>
        public List<VmAvatar> BuildList()
        {
            return _avatars;
        }

        /// <summary>
        ///     Create an Extended Avatar List with two items
        /// </summary>
        /// <returns></returns>
        public AvatarFactory WithExtendedList()
        {
            _avatars.Add(new VmAvatar
            {
                Id = 1,
                Image = new byte[0]
            });
            return this;
        }
    }
}