using System.Collections.Generic;

namespace Scrummage.Data.Models {
	public class Member {

		#region Properties

		public int MemberId { get; set; }
		public string Name { get; set; }
        public string ShortName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
		public string Email { get; set; }
		
        #endregion

		#region Navigation

        public virtual ICollection<Role> Roles { get; set; }
		public virtual Avatar Avatar { get; set; }
		
        #endregion
	}
}