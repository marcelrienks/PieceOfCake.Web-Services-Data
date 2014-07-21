using Scrummage.Data.Interfaces;
using Scrummage.Data.Models;

namespace Scrummage.Test.DataAccess
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        #region Properties

        public IRepository<Role> RoleRepository { get; private set; }
        public IRepository<User> UserRepository { get; private set; }
        public IRepository<Avatar> AvatarRepository { get; private set; }

        #endregion

        public FakeUnitOfWork()
        {
            RoleRepository = new FakeRepository<Role>();
            UserRepository = new FakeRepository<User>();
            AvatarRepository = new FakeRepository<Avatar>();
        }
    }
}
