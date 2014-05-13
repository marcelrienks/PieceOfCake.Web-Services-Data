namespace Scrummage.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class MemberMigration : DbMigration {
		public override void Up() {
			CreateTable(
					"dbo.Members",
					c => new {
						MemberId = c.Int(nullable: false, identity: true),
						Name = c.String(nullable: false, maxLength: 30),
						ShortName = c.String(nullable: false, maxLength: 3),
						Username = c.String(nullable: false, maxLength: 30),
						Password = c.String(nullable: false),
						Email = c.String(nullable: false),
					})
					.PrimaryKey(t => t.MemberId);

			CreateTable(
					"dbo.RoleMembers",
					c => new {
						Role_RoleId = c.Int(nullable: false),
						Member_MemberId = c.Int(nullable: false),
					})
					.PrimaryKey(t => new { t.Role_RoleId, t.Member_MemberId })
					.ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
					.ForeignKey("dbo.Members", t => t.Member_MemberId, cascadeDelete: true)
					.Index(t => t.Role_RoleId)
					.Index(t => t.Member_MemberId);

		}

		public override void Down() {
			DropForeignKey("dbo.RoleMembers", "Member_MemberId", "dbo.Members");
			DropForeignKey("dbo.RoleMembers", "Role_RoleId", "dbo.Roles");
			DropIndex("dbo.RoleMembers", new[] { "Member_MemberId" });
			DropIndex("dbo.RoleMembers", new[] { "Role_RoleId" });
			DropTable("dbo.RoleMembers");
			DropTable("dbo.Members");
		}
	}
}
