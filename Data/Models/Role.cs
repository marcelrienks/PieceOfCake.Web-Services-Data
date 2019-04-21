using System.Collections.Generic;

namespace Data.Models
{
    public class Role
    {
        #region Properties

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<User> Users { get; set; }

        #endregion
    }
}