using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.VNPay
{
	public class VNPayComparer : IComparer<string>
	{
		public int Compare(string x, string y)
		{
			if (x==y ) return 0;
			if(y==null) return 1;
			if (x == null) return -1;
			var vnCompare = CompareInfo.GetCompareInfo("en-US");
			return vnCompare.Compare(x, y, CompareOptions.Ordinal);
		}
	}
}
