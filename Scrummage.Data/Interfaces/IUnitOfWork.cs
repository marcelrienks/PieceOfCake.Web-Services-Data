using Scrummage.Data.Models;

namespace Scrummage.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Avatar> AvatarRepository { get; }
    }
}
