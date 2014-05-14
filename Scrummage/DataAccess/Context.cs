using System.Data.Entity;
using Scrummage.Models;

namespace Scrummage.DataAccess {
	public class Context : DbContext {
		public Context()
			: base("ScrummageDB") {
		}

		public DbSet<Role> Roles { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<Avatar> Avatars { get; set; }
	}
}