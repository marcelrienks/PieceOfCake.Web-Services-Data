using PieceOfCake.Data.Models;

namespace PieceOfCake.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
    }
}