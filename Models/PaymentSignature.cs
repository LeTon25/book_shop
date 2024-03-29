﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	[Table("ChuKyThanhToan")]
	public class PaymentSignature
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public string? PaymentId { get; set; } = string.Empty;
		[ForeignKey("PaymentId")]
		public Payment? Payment { get; set; }
		public string? SignValue { get; set; } = string.Empty;
		public string? SignAlgo { get; set; } = string.Empty;
		public string? SignOwn { get; set; } = string.Empty;
		public DateTime? SignDate { get; set; }
		public bool IsValid { get; set; }
	}
}
