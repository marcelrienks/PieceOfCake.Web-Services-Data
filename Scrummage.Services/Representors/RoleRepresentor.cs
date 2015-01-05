using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PieceOfCake.Services.Representors
{
    public class RoleRepresentor
    {
        #region Properties
        public int Id { get; set; }
        
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [StringLength(180, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Description { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<UserRepresentor> Users { get; set; }

        #endregion
    }
}