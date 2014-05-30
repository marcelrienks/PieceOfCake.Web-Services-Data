using System.ComponentModel.DataAnnotations;

namespace Scrummage.DataAccess.Models {
	public class Avatar {

		#region Properties
		public int MemberId { get; set; }
		public byte[] Image { get; set; }
		#endregion

		#region Navigation
		public virtual Member Member { get; set; }
		#endregion
	}
}