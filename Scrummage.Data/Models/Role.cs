using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scrummage.Data.Models {
	public class Role {

		#region Properties

		public int RoleId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		
        #endregion

		#region Navigation

		public virtual ICollection<Member> Members { get; set; }
		
        #endregion
	}
}