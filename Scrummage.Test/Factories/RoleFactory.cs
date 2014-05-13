using System.Collections.Generic;
using Scrummage.Models;

namespace Scrummage.Tests.Factories {
	public static class RoleFactory {

		/// <summary>
		/// Default Role model
		/// </summary>
		private static readonly Role DefaultRole = new Role {
			RoleId = 0,
			Title = "Title",
			Description = "Description"
		};

		/// <summary>
		/// Returns a list containing one default Role model
		/// </summary>
		/// <returns></returns>
		public static List<Role> CreateDefaultRoleList() {
			return new List<Role> {
				DefaultRole
			};
		}

		/// <summary>
		/// Returns a default Role model
		/// </summary>
		/// <returns></returns>
		public static Role CreateDefaultRole() {
			return DefaultRole;
		}
	}
}
