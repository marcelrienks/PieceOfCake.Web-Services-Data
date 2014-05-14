using System.Data.Entity.Migrations;

namespace Scrummage.Migrations {
	public partial class RoleMigration : DbMigration {
		public override void Up() {
			CreateTable(
					"dbo.Roles",
					c => new {
						RoleId = c.Int(nullable: false, identity: true),
						Title = c.String(nullable: false, maxLength: 30),
						Description = c.String(maxLength: 180),
					})
					.PrimaryKey(t => t.RoleId);

		}

		public override void Down() {
			DropTable("dbo.Roles");
		}
	}
}
