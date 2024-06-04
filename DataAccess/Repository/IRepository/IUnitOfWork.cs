using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        IShoppingCartRepository ShoppingCartRepo { get; }
        IApplicationUserRepository ApplicationUserRepo { get; }
        IOrderDetailRepository OrderDetailRepo { get; }
        IOrderRepository OrderRepo { get; }   
        IProductImageRepository ProductImageRepo { get; }
        IProductCategoryRepository ProductCategoryRepo { get; }
        ICollectionRepository CollectionRepo { get; }
        IPublisherRepository PublisherRepo { get;}
        public int Save();
    }
}
