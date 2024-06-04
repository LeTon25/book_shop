using BookyStore.DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public ICategoryRepository CategoryRepo { get; private set; }
        public IProductRepository ProductRepo { get; private set; }
        public IShoppingCartRepository ShoppingCartRepo { get; private set; }
        public IApplicationUserRepository ApplicationUserRepo { get; private set; }
        public IOrderRepository OrderRepo { get; private set; } 
        public IOrderDetailRepository   OrderDetailRepo { get; private set; }

        public IProductImageRepository ProductImageRepo { get; private set; }   

        public IProductCategoryRepository ProductCategoryRepo { get; private set; }
        public ICollectionRepository CollectionRepo { get; private set; } 

        public IPublisherRepository PublisherRepo { get; private set; }
		public UnitOfWork(AppDbContext db)
        {
            _db = db;
            CategoryRepo = new CategoryRepository(_db);
            ProductRepo = new ProductRepository(_db);
            ShoppingCartRepo = new ShoppingCartRepository(_db); 
            ApplicationUserRepo = new ApplicationUserRepository(_db);   
            OrderDetailRepo = new OrderDetailRepository(_db);
            OrderRepo = new OrderRepository(_db);
            ProductImageRepo = new ProductImageRepository(_db);
            ProductCategoryRepo = new ProductCategoryRepository(_db);
            CollectionRepo = new CollectionRepository(_db);
            PublisherRepo = new PublisherRepository(_db);   
        }

        public int Save()
        {
           return _db.SaveChanges();
        }
    }
}
