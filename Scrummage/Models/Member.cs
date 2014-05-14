using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrummage.Models {

	[Serializable]
	public class Member {

		#region Properties
		//MemberId
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

		//Password
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; private set; }

		[NotMapped]
		public string ClearPassword {
			get { return Password; }
			set { Password = Libraries.Encryption.Hash(value); }
		}

		//Email
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		#endregion

		#region Navigation
		//Todo: Role should be a required field in Member model
		public virtual ICollection<Role> Roles { get; set; }
		[Required]
		public virtual Avatar Avatar { get; set; }
		#endregion
	}
}