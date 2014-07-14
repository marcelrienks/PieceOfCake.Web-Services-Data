using Scrummage.Data.Models;
using System.Collections.Generic;

namespace Scrummage.Test.Factories.ModelFactories
{
    public class MemberFactory
    {
        private Member _member;
        private List<Member> _members;

        /// <summary>
        ///     Create default Member and MemberList objects
        /// </summary>
        public MemberFactory()
        {
            CreateDefaultMemberAndMemberList(0);
        }

        /// <summary>
        ///     Create default Member and MemberList objects with given Member Id
        /// </summary>
        /// <param name="memberId"></param>
        public MemberFactory(int memberId)
        {
            CreateDefaultMemberAndMemberList(memberId);
        }

        /// <summary>
        ///     Create default Member and MemberList objects with given Member Id
        /// </summary>
        /// <param name="memberId"></param>
        private void CreateDefaultMemberAndMemberList(int memberId)
        {
            _member = new Member
            {
                MemberId = memberId,
                Name = "Name",
                ShortName = "sn",
                Username = "Username",
                Email = "email@address.com",
                Roles = new RoleFactory().BuildList(),
                Avatar = new Avatar
                {
                    MemberId = memberId,
                    Image = new byte[0]
                }
            };

            _members = new List<Member>
            {
                _member
            };
        }

        /// <summary>
        ///     Return constructed Role
        /// </summary>
        /// <returns>Role</returns>
        public Member Build()
        {
            return _member;
        }

        /// <summary>
        ///     Return constructed Role List
        /// </summary>
        /// <returns>List<Member></returns>
        public List<Member> BuildList()
        {
            return _members;
        }
    }
}
