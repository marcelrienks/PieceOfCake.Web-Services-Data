using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrummage.Models {

	[Serializable]
	public class Member {

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
		[NotMapped]
		public string ClearPassword {
			get { return _clearPassword; }
			set {
				_clearPassword = value;
				Password = Libraries.Encryption.Hash(value);
			}
		}

		//Password field used to validate that passwords match
		[NotMapped]
		[CompareAttribute("ClearPassword", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassowrd { get; set; }
		#endregion

		#region Navigation
		[Required]
		public virtual ICollection<Role> Roles { get; set; }

		[Required]
		public virtual Avatar Avatar { get; set; }
		#endregion
	}
}