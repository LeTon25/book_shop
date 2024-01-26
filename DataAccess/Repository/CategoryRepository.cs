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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        
        public CategoryRepository(AppDbContext db) : base(db)
        {
        }
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
