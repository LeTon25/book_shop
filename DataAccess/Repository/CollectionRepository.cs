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
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(AppDbContext db) : base(db)
        {
        }

        public void Update(Collection collection)
        {
           _db.Update(collection);
        }
    }
}
