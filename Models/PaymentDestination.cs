using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	[Table("CongThanhToan")]
	public class PaymentDestination
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public string? DesName { get; set; } = string.Empty;
		public string? DesShortName { get; set; } = string.Empty;
		public string? DesParentId { get; set; } = string.Empty;
		public string? DesLogo { get; set; } = string.Empty;
		public int SortIndex { get; set; }
		public bool IsActive { get; set; }
	}
}
