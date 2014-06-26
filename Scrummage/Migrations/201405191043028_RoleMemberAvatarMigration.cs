using System.Data.Entity.Migrations;

namespace Scrummage.Migrations
{
    public partial class RoleMemberAvatarMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avatars",
                c => new
                {
                    MemberId = c.Int(false),
                    Image = c.Binary(false),
                })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Members", t => t.MemberId, true)
                .Index(t => t.MemberId);

            CreateTable(
                "dbo.Members",
                c => new
                {
                    MemberId = c.Int(false, true),
                    Name = c.String(false, 30),
                    ShortName = c.String(false, 3),
                    Username = c.String(false, 30),
                    Password = c.String(false),
                    Email = c.String(false),
                })
                .PrimaryKey(t => t.MemberId)
                .Index(t => t.Username, unique: true);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    RoleId = c.Int(false, true),
                    Title = c.String(false, 30),
                    Description = c.String(maxLength: 180),
                })
                .PrimaryKey(t => t.RoleId);

            CreateTable(
                "dbo.MemberRoles",
                c => new
                {
                    Member_MemberId = c.Int(false),
                    Role_RoleId = c.Int(false),
                })
                .PrimaryKey(t => new {t.Member_MemberId, t.Role_RoleId})
                .ForeignKey("dbo.Members", t => t.Member_MemberId, true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, true)
                .Index(t => t.Member_MemberId)
                .Index(t => t.Role_RoleId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Avatars", "MemberId", "dbo.Members");
            DropForeignKey("dbo.MemberRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.MemberRoles", "Member_MemberId", "dbo.Members");
            DropIndex("dbo.MemberRoles", new[] {"Role_RoleId"});
            DropIndex("dbo.MemberRoles", new[] {"Member_MemberId"});
            DropIndex("dbo.Members", new[] {"Username"});
            DropIndex("dbo.Avatars", new[] {"MemberId"});
            DropTable("dbo.MemberRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Members");
            DropTable("dbo.Avatars");
        }
    }
}