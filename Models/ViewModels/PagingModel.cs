using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class PagingModel
    {
        public int currentPage { get; set; }    
        public int totalPages { get; set; }
        public Func<int?, string> urlGenerator { get; set; }
    }
}
