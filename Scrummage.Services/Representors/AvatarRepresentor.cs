using System.ComponentModel.DataAnnotations;

namespace Scrummage.Services.Representors
{
    public class AvatarRepresentor
    {
        #region Properties

        [Required]
        public int Id { get; set; }

        [Required]
        public byte[] Image { get; set; }

        #endregion

        #region Navigation

        public virtual UserRepresentor User { get; set; }

        #endregion
    }
}