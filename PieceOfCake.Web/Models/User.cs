using PieceOfCake.Web.Interfaces;
using System.Collections.Generic;

namespace PieceOfCake.Web.Models
{
    public class User : IModel
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

        /// <summary>
        /// Returns this models Resource URI name
        /// </summary>
        /// <returns></returns>
        public string Resource()
        {
            return "users";
        }
    }
}