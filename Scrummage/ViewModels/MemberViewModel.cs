using Scrummage.DataAccess.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.ViewModels {
	public class MemberViewModel {

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

        //Clear version of hashed out password property (Note this properties set method, populates the hashed password property)
        private string _clearPassword;
        [DataType(DataType.Password)]
        public string ClearPassword
        {
            get { return _clearPassword; }
            set
            {
                _clearPassword = value;
                Password = Libraries.Encryption.Hash(value);
            }
        }

        //Confirm Password field used to validate that passwords match
        [Required]
        [DataType(DataType.Password)]
        [Compare("ClearPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassowrd { get; set; }
		#endregion

		#region Navigation
		[Required]
		public virtual ICollection<RoleViewModel> RoleModelViews { get; set; }

		[Required]
		public virtual AvatarViewModel AvatarModelView { get; set; }
		#endregion
	}
}