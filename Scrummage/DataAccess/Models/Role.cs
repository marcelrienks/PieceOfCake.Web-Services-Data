using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.DataAccess.Models {
	public class Role {

		#region Properties
		public int RoleId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		#endregion

		#region Navigation
		public virtual ICollection<Member> Members { get; set; }
		#endregion
	}
}