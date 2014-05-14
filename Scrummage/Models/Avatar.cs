using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrummage.Models {

	[Serializable]
	public class Avatar {

		#region Properties
		//MemberId
		[Key, ForeignKey("Member")]
		public int MemberId { get; set; }

		//Image
		public byte[] Image { get; set; }
		#endregion

		#region Navigation
		[Required]
		public virtual Member Member { get; set; }
		#endregion
	}
}