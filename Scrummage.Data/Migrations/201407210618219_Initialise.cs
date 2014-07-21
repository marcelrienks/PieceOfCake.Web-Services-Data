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
                    Id = c.Int(false),
                    Image = c.Binary(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id, true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(false, 30),
                    ShortName = c.String(false, 3),
                    Username = c.String(false, 30),
                    Password = c.String(false),
                    Email = c.String(false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Int(false, true),
                    Title = c.String(false, 180),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.RoleUsers",
                c => new
                {
                    Role_Id = c.Int(false),
                    User_Id = c.Int(false),
                })
                .PrimaryKey(t => new {t.Role_Id, t.User_Id})
                .ForeignKey("dbo.Roles", t => t.Role_Id, true)
                .ForeignKey("dbo.Users", t => t.User_Id, true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Avatars", "Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropIndex("dbo.RoleUsers", new[] {"User_Id"});
            DropIndex("dbo.RoleUsers", new[] {"Role_Id"});
            DropIndex("dbo.Users", new[] {"Username"});
            DropIndex("dbo.Avatars", new[] {"Id"});
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Avatars");
        }
    }
}