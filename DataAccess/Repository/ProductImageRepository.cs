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
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        
        public ProductImageRepository(AppDbContext db) : base(db)
        {
        }
        public void Update(ProductImage obj)
        {
            _db.ProductImages.Update(obj);
        }
    }
}
