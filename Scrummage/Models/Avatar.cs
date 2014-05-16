using System;
using System.ComponentModel.DataAnnotations;

namespace Scrummage.Models {

	[Serializable]
	public class Avatar {

		#region Properties
		//MemberId
		public int MemberId { get; set; }

		//Image
		[Required]
		public byte[] Image { get; set; }
		#endregion

		#region Navigation
		[Required]
		public virtual Member Member { get; set; }
		#endregion
	}
}