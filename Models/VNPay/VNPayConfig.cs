using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.VNPay
{
	public class VNPayConfig
	{
		public static void Initialize(IConfiguration configuration)
		{
			VNPayConfig.Version = configuration["Version"];
			VNPayConfig.TmnCode = configuration["TmnCode"];
			VNPayConfig.HashSecret = configuration["HashSecret"];
			VNPayConfig.ReturnUrl = configuration["ReturnUrl"];
			VNPayConfig.PaymentUrl = configuration["PaymentUrl"];
		}
		public static string Configname => "Vnpay";
		public static string TmnCode { get; set; } = string.Empty;
		public static string HashSecret { get; set; } = string.Empty;
		public static string ReturnUrl { get; set; } = string.Empty;
		public static string PaymentUrl { get; set; } = string.Empty;
		public static string Version { get; set;} = string.Empty;

	}
}
