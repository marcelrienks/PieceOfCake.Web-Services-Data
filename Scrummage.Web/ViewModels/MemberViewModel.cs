using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.Web.ViewModels
{
    public class MemberViewModel
    {

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
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        //UserName
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        //Email
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "The email address is invalid.")]
        public string Email { get; set; }

        //Password (Note the ClearPassword property populates this property)
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //Confirm Password field used to validate that passwords match
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        #endregion

        #region Navigation
        [Required]
        public virtual ICollection<RoleViewModel> RoleViewModels { get; set; }

        [Required]
        public virtual AvatarViewModel AvatarViewModel { get; set; }
        #endregion
    }
}