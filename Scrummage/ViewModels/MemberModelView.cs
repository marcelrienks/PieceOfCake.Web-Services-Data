using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Scrummage.DataAccess.Models;

namespace Scrummage.ViewModels {
	public class MemberModelView {

		//Note:
		//Using View Models in an attempt to keep poco model classes clean and un cluttered.
		//This prevents the poco classes from requiring 'NotMapped' attributes on additional properties required by the views atc.

		#region Properties
		//MemberId
		public int MemberId { get; set; }

		//Name
		[Required]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
		public string Name { get; set; }

		//ShortName
		[Required]
		[StringLength(3, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
		public string ShortName { get; set; }

		//UserName
		[Required]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
		public string Username { get; set; }

		//Password (Note the ClearPassword property populates this property)
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; private set; }

		//Email
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		#endregion

		#region Note Mapped
		private string _clearPassword;
		//Clear version of hashed out password property (Note this properties set method, populates the hashed password property)
		public string ClearPassword {
			get { return _clearPassword; }
			set {
				_clearPassword = value;
				Password = Libraries.Encryption.Hash(value);
			}
		}

		//Password field used to validate that passwords match
		[Compare("ClearPassword", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassowrd { get; set; }
		#endregion

		#region Navigation
		[Required]
		public virtual ICollection<RoleModelView> RoleModelViews { get; set; }

		[Required]
		public virtual AvatarModelView AvatarModelView { get; set; }
		#endregion

		#region Model Mapping
		public static explicit operator MemberModelView(Member member) {
			return new MemberModelView() {
				MemberId = member.MemberId,
				Name = member.Name,
				ShortName = member.ShortName,
				Username = member.Username,
				Password = member.Password,
				ClearPassword = null,
				ConfirmPassowrd = null,
				Email = member.Email,
				RoleModelViews = null,
				AvatarModelView = null
			};
		}

		public static explicit operator Member(MemberModelView memberModelView) {
			return new Member() {
				MemberId = memberModelView.MemberId,
				Name = memberModelView.Name,
				ShortName = memberModelView.ShortName,
				Username = memberModelView.Username,
				Password = memberModelView.Password,
				Email = memberModelView.Email,
				Roles = null,
				Avatar = null
			};
		}
		#endregion
	}
}