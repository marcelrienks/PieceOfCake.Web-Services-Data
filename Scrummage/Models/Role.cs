using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.Models {

	[Serializable]
	public class Role {

		#region Properties
		//RoleId
		public int RoleId { get; set; }

		//Title
		[Required]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
		public string Title { get; set; }

		//Description
		[StringLength(180, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
		public string Description { get; set; }
		#endregion

		#region Navigation
		public virtual ICollection<Member> Members { get; set; }
		#endregion
	}
}