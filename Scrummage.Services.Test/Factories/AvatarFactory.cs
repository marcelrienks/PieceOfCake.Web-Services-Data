using System.Collections.Generic;
using PieceOfCake.Services.Representors;

namespace PieceOfCake.Services.Test.Factories
{
    public class AvatarFactory
    {
        private AvatarRepresentor _avatar;
        private List<AvatarRepresentor> _avatars;

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
            _avatar = new AvatarRepresentor
            {
                Id = avatarId,
                Image = new byte[0]
            };

            _avatars = new List<AvatarRepresentor>
            {
                _avatar
            };
        }

        /// <summary>
        ///     Return constructed Avatar
        /// </summary>
        /// <returns>Avatar</returns>
        public AvatarRepresentor Build()
        {
            return _avatar;
        }

        /// <summary>
        ///     Return constructed Avatar List
        /// </summary>
        /// <returns>List<Avatar></returns>
        public List<AvatarRepresentor> BuildList()
        {
            return _avatars;
        }

        /// <summary>
        ///     Create an Extended Avatar List with two items
        /// </summary>
        /// <returns></returns>
        public AvatarFactory WithExtendedList()
        {
            _avatars.Add(new AvatarRepresentor
            {
                Id = 1,
                Image = new byte[0]
            });
            return this;
        }
    }
}