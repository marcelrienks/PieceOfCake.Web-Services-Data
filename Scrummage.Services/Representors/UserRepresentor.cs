using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.Services.Representors
{
    public class UserRepresentor
    {
        #region Properties

        public int Id { get; set; }
        
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Name { get; set; }
        
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }
        
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "The email address is invalid.")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<RoleRepresentor> Roles { get; set; }
        public virtual AvatarRepresentor Avatar { get; set; }

        #endregion
    }
}