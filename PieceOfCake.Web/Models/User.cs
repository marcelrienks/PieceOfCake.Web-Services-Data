using System.Collections.Generic;

namespace PieceOfCake.Web.Models
{
    public class User
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<Role> Roles { get; set; }

        #endregion
    }
}