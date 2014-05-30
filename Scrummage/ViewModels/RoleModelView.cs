using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Scrummage.DataAccess.Models;

namespace Scrummage.ViewModels {
	public class RoleModelView {

		//Note:
		//Using View Models in an attempt to keep poco model classes clean and un cluttered.
		//This prevents the poco classes from requiring 'NotMapped' attributes on additional properties required by the views atc.

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
		public virtual ICollection<MemberModelView> MemberModelViews { get; set; }
		#endregion

		#region Model Mapping
		public static explicit operator RoleModelView(Role role) {
			//Todo: investigate if i can use a flag to determine one level deep of related model conversion (apply this to all explicit conversions)
			return new RoleModelView() {
				RoleId = role.RoleId,
				Title = role.Title,
				Description = role.Description,
				MemberModelViews = null
			};
		}

		public static explicit operator Role(RoleModelView roleModelView) {
			return new Role() {
				RoleId = roleModelView.RoleId,
				Title = roleModelView.Title,
				Description = roleModelView.Description,
				Members = null
			};
		}
		#endregion
	}
}