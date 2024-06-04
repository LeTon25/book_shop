using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ShoppingCartVM
    {
        [ValidateNever]
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public Order Order { get; set; }
    }
}
