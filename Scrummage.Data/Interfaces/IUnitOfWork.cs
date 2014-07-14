using Scrummage.Data.Models;

namespace Scrummage.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<Member> MemberRepository { get; }
        IRepository<Avatar> AvatarRepository { get; }
    }
}
