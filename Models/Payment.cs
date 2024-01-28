using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	[Table("ThanhToan")]
	public class Payment
	{
		public string Id { get; set; } = string.Empty;
		public string PaymentContent { get; set; } = string.Empty;
		public string PaymentCurrency { get; set; } = string.Empty;
		public string PaymentRefId { get; set; } = string.Empty;
		public decimal? RequiredAmount { get; set; }
		public DateTime? PaymentDate { get; set; } = DateTime.Now;
		public DateTime? ExpireDate { get; set; }
		public string? PaymentLanguage { get; set; } = string.Empty;
		public string? PaymentDestinationId { get; set; } = string.Empty;
		[ForeignKey("PaymentDestinationId")]
		public PaymentDestination? PaymentDestination { get; set; }	
		public decimal? PaidAmount { get; set; }
		public string? PaymentStatus { get; set; } = string.Empty;
		public string? PaymentLastMessage { get; set; } = string.Empty;
	}
}
