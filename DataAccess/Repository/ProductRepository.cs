using BookyStore.DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext _db) : base(_db) { }
        public void Update(Product product)
        {
            var productFromDB = base.GetFirstOrDefault(c => c.ID == product.ID);
            if (productFromDB != null) 
            { 
                productFromDB.Title = product.Title;
                productFromDB.Description = product.Description;
                productFromDB.ISBN = product.ISBN;
                productFromDB.Author = product.Author;
                productFromDB.CategoryId = product.CategoryId;
                productFromDB.Price = product.Price;
                if (product.ImageUrl != null)
                {
                    productFromDB.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
