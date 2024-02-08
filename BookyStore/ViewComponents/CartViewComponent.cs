using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utility;

namespace BookyStore.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private IUnitOfWork unitOfWork;
        public CartViewComponent(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string cartCount = "0";
            if (claim != null) 
            {
                if (HttpContext.Session.GetString(SD.CART_KEY) == null)
                {
                    cartCount =  unitOfWork.ShoppingCartRepo.GetAll(c=> c.ApplicationUserId == claim.Value).Count().ToString();
                    HttpContext.Session.SetString(SD.CART_KEY, cartCount);
                }    
            }
            else
            {
                HttpContext.Session.Clear();
            }
            return View("Default",cartCount);
        }
    }
}
