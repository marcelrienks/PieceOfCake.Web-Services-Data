
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Scrummage.Models;

namespace Scrummage.Migrations {
	using System.Data.Entity.Migrations;
	using DataAccess;

	internal sealed class Configuration : DbMigrationsConfiguration<Context> {
		public Configuration() {
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(Context context) {
			SeedRoles(context);
			SeedMembers(context);
		}

		/// <summary>
		/// This method seeds the database with Roles
		/// </summary>
		/// <param name="context"></param>
		private void SeedRoles(Context context) {
			if (context.Roles.Any()) return;

			var roles = new List<Role> {
				new Role { Title = "Administrator", Description = "God like access" },
				new Role { Title = "Scrum Master", Description = "Person responsible for guiding the team in the Scrum methodologies, near God like access" },
				new Role { Title = "Product Owner", Description = "Person responsible for the products. Access to Products Management, Epic Management and Product Backlog" },
				new Role { Title = "Team Member", Description = "Person responsible for converting requirements into functional software, Access to Backlog Items and Tasks" },
			};

			roles.ForEach(role => context.Roles.AddOrUpdate(roleType => roleType.RoleId, role));
			context.SaveChanges();
		}

		/// <summary>
		/// This method seeds the database with Members
		/// </summary>
		/// <param name="context"></param>
		private void SeedMembers(Context context) {
			if (context.Members.Any()) return;

			var members = new List<Member> {
				new Member {
					Name = "Marcel Rienks",
					ShortName = "mr",
					Username = "marcelr",
					ClearPassword = "E3mc2rd!",
					Email = "marcelrienks@gmail.com",
					Roles = context.Roles.Where(role => role.Title == "Administrator").ToList(),
					Avatar = new Avatar() {
						Image = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\", @"Images\default_avatar.jpg"))
					}
				}
			};

			members.ForEach(member => context.Members.AddOrUpdate(memberType => memberType.MemberId, member));
			context.SaveChanges();
		}
	}
}