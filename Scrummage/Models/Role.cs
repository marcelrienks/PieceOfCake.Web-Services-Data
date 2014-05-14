using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrummage.Models {

	[Serializable]
	public class Role {

		#region Properties
		//RoleId
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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