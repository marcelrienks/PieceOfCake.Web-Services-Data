using Scrummage.Models;

namespace Scrummage.Interfaces {
	public interface IUnitOfWork {
		IRepository<Role> RoleRepository { get; }
		IRepository<Member> MemberRepository { get; }
		IRepository<Avatar> AvatarRepository { get; }
	}
}
