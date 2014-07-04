using System.Collections.Generic;
using Scrummage.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.ViewModels
{
    public class RoleViewModel
    {

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

        #region Model Mapping
        /// <summary>
        ///     Explicitly converts a Role to a RoleModelView
        /// </summary>
        /// <param name="role"></param>
        /// <returns>RoleViewModel</returns>
        public static explicit operator RoleViewModel(Role role)
        {
            return role != null
                ? new RoleViewModel()
                {
                    RoleId = role.RoleId,
                    Description = role.Description,
                    Title = role.Title
                }
                : null;
        }

        /// <summary>
        ///     Explicitly converts a RoleModelView to a Role
        /// </summary>
        /// <param name="roleModelView"></param>
        /// <returns>Role</returns>
        public static explicit operator Role(RoleViewModel roleModelView)
        {
            return roleModelView != null
                ? new Role()
                {
                    RoleId = roleModelView.RoleId,
                    Description = roleModelView.Description,
                    Title = roleModelView.Title
                }
                : null;
        }
        #endregion
    }
}