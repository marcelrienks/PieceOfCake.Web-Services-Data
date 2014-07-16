using System.IO;
using Scrummage.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Scrummage.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        ///     Overrides the Seed method with custom seeds
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(Context context)
        {
            SeedRoles(context);
            SeedMembers(context);
        }

        /// <summary>
        ///     This method seeds the database with Roles
        /// </summary>
        /// <param name="context"></param>
        private void SeedRoles(Context context)
        {
            if (context.Roles.Any()) return;

            var roles = new List<Role>
            {
                new Role {Title = "Administrator", Description = "God like access"},
                new Role
                {
                    Title = "Scrum Master",
                    Description =
                        "Person responsible for guiding the team in the Scrum methodologies, near God like access"
                },
                new Role
                {
                    Title = "Product Owner",
                    Description =
                        "Person responsible for the products. Access to Products Management, Epic Management and Product Backlog"
                },
                new Role
                {
                    Title = "Team Member",
                    Description =
                        "Person responsible for converting requirements into functional software, Access to Backlog Items and Tasks"
                },
			};

            roles.ForEach(role => context.Roles.AddOrUpdate(roleType => roleType.RoleId, role));
            context.SaveChanges();
        }

        /// <summary>
        ///     This method seeds the database with Members
        /// </summary>
        /// <param name="context"></param>
        private void SeedMembers(Context context)
        {
            if (context.Members.Any()) return;

            var members = new List<Member>
            {
                new Member
                {
					Name = "Marcel Rienks",
					ShortName = "mr",
					Username = "marcelr",
					Password = "E3mc2rd!",
					Email = "marcelrienks@gmail.com",
                    Roles = context.Roles.Where(role => role.Title == "Administrator" || role.Title == "Scrum Master").ToList(),
                    Avatar = new Avatar
                    {
                        Image = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug\", @"Migrations\default_avatar.jpg"))
					}
				}
			};

            members.ForEach(member => context.Members.AddOrUpdate(memberType => memberType.MemberId, member));
            context.SaveChanges();
        }
    }
}