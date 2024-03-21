using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class HomeModel
    {
        public IEnumerable<Product> Products { get; set; }  
        public PagingModel Paging { get; set; }
    }
}
