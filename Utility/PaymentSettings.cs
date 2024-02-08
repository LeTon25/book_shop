using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
	public class PaymentSettings
	{
		public string? MerchantIpnUrl { get; set; } = string.Empty;
		public string? MerchantReturnUrl { get; set; } = string.Empty;
		public string? SecretKey { get; set; } = string.Empty;
	}
}
