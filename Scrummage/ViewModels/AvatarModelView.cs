using System.ComponentModel.DataAnnotations;

namespace Scrummage.ViewModels {
	public class AvatarModelView {

		//Note:
		//Using View Models in an attempt to keep poco model classes clean and un cluttered.
		//This prevents the poco classes from requiring 'NotMapped' attributes on additional properties required by the views atc.

		#region Properties
		//MemberId
		public int MemberId { get; set; }

		//Image
		[Required]
		public byte[] Image { get; set; }
		#endregion

		#region Navigation
		[Required]
		public virtual MemberModelView MemberModelView { get; set; }
		#endregion
	}
}