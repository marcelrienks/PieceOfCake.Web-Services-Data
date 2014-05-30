using Scrummage.DataAccess.Models;
using Scrummage.Interfaces;

namespace Scrummage.DataAccess {
	public class UnitOfWork : IUnitOfWork {

		#region Properties
		public IRepository<Role> RoleRepository { get; private set; }
		public IRepository<Member> MemberRepository { get; private set; }
		public IRepository<Avatar> AvatarRepository { get; private set; }
		#endregion

		public UnitOfWork() {
			var context = new Context();
			RoleRepository = new Repository<Role>(context);
			MemberRepository = new Repository<Member>(context);
			AvatarRepository = new Repository<Avatar>(context);
		}
	}
}