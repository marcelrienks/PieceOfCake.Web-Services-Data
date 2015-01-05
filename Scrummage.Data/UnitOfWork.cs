using PieceOfCake.Data.Interfaces;
using PieceOfCake.Data.Models;

namespace PieceOfCake.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties

        public IRepository<Role> RoleRepository { get; private set; }
        public IRepository<User> UserRepository { get; private set; }
        public IRepository<Avatar> AvatarRepository { get; private set; }

        #endregion

        public UnitOfWork()
        {
            var context = new Context();
            RoleRepository = new Repository<Role>(context);
            UserRepository = new Repository<User>(context);
            AvatarRepository = new Repository<Avatar>(context);
        }
    }
}