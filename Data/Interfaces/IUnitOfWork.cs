using Data.Models;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
    }
}