using System.Collections.Generic;
using Scrummage.DataAccess.Models;

namespace Scrummage.Test.Factories {
	public static class MemberFactory {
		
		private static int memberId = 0;

		/// <summary>
		/// Default Member model
		/// </summary>
		private static readonly Member DefaultMember = new Member {
			MemberId = memberId,
			Name = "Name",
			ShortName = "sn",
			Username = "Username",
			//ClearPassword = "password",
			Email = "email@address.com",
			Roles = RoleFactory.CreateDefaultRoleList(),
			Avatar = new Avatar {
				MemberId = memberId,
				Image = new byte[0]
			}
		};

		/// <summary>
		/// Returns a list containing one default Member model
		/// </summary>
		/// <returns></returns>
		public static List<Member> CreateDefaultMemberList() {
			return new List<Member> {
				DefaultMember
			};
		}

		/// <summary>
		/// Returns a default Member model
		/// </summary>
		/// <returns></returns>
		public static Member CreateDefaultMember() {
			return DefaultMember;
		}
	}
}
