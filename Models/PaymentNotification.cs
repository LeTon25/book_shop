using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	[Table("ThongBaoThanhToan")]
	public class PaymentNotification
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public string PaymentRefId { get; set; } = string.Empty;
		public DateTime? NotiDate { get; set; }
		public string? NotiContent { get; set; } = string.Empty;
		public decimal NotiAmount { get; set; }
		public string? NotiMessage { get; set; } = string.Empty;
		public string? NotiSignature { get; set; } = string.Empty;
		public string? NotiPaymentId { get; set; } = string.Empty;
		[ForeignKey("NotiPayementId")]
		public Payment? PaymentNoti { get; set; }	
		public string? NotiNotiStatus { get; set; } = string.Empty;
		public DateTime? NotiResDate { get; set; }
		public string? NotiResMessage { get; set; } = string.Empty;
		public string? NotiResHttpCode { get; set; } = string.Empty;
	}
}
