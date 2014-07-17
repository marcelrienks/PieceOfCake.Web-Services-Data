using System.Data.Entity.Migrations;

namespace Scrummage.Data.Migrations
{
    public partial class Initialise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        ShortName = c.String(nullable: false, maxLength: 3),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);

            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 180),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.MemberRoles",
                c => new
                    {
                        Member_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_Id, t.Role_Id })
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Member_Id)
                .Index(t => t.Role_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Avatars", "Id", "dbo.Members");
            DropForeignKey("dbo.MemberRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.MemberRoles", "Member_Id", "dbo.Members");
            DropIndex("dbo.MemberRoles", new[] { "Role_Id" });
            DropIndex("dbo.MemberRoles", new[] { "Member_Id" });
            DropIndex("dbo.Members", new[] { "Username" });
            DropIndex("dbo.Avatars", new[] { "Id" });
            DropTable("dbo.MemberRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Members");
            DropTable("dbo.Avatars");
        }
    }
}
