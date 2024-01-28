using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Base
{
	public class ResultWithData<T> : BaseResult
	{
		public T? Data { get; set; }
	}
}
