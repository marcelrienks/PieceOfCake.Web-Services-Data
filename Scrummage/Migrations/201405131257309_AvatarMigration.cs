using System.Data.Entity.Migrations;

namespace Scrummage.Migrations {
	public partial class AvatarMigration : DbMigration {
		public override void Up() {
			CreateTable(
					"dbo.Avatars",
					c => new {
						MemberId = c.Int(nullable: false),
						Image = c.Binary(),
					})
					.PrimaryKey(t => t.MemberId)
					.ForeignKey("dbo.Members", t => t.MemberId)
					.Index(t => t.MemberId);

		}

		public override void Down() {
			DropForeignKey("dbo.Avatars", "MemberId", "dbo.Members");
			DropIndex("dbo.Avatars", new[] { "MemberId" });
			DropTable("dbo.Avatars");
		}
	}
}
