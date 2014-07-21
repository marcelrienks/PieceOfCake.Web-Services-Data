using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.Web.ViewModels
{
    public class RoleViewModel
    {
        //Note:
        //Using View Models in an attempt to keep poco model classes clean and un cluttered.
        //This prevents the poco classes from requiring 'NotMapped' attributes on additional properties required by the views atc.

        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [StringLength(180, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Description { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<UserViewModel> UserViewModels { get; set; }

        #endregion
    }
}