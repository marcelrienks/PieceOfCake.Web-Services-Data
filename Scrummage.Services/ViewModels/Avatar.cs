using System.ComponentModel.DataAnnotations;

namespace Scrummage.Services.ViewModels
{
    public class Avatar
    {
        #region Properties

        [Required]
        public int Id { get; set; }

        [Required]
        public byte[] Image { get; set; }

        #endregion

        #region Navigation

        public virtual User User { get; set; }

        #endregion
    }
}