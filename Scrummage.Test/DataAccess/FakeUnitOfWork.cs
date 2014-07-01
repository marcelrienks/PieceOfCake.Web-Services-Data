using Scrummage.DataAccess.Models;
using Scrummage.Interfaces;

namespace Scrummage.Test.DataAccess
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        #region Properties

        public IRepository<Role> RoleRepository { get; private set; }
        public IRepository<Member> MemberRepository { get; private set; }
        public IRepository<Avatar> AvatarRepository { get; private set; }

        #endregion

        public FakeUnitOfWork()
        {
            RoleRepository = new FakeRepository<Role>();
            MemberRepository = new FakeRepository<Member>();
            AvatarRepository = new FakeRepository<Avatar>();
        }
    }
}
