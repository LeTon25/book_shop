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
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext db) : base(db)
        {
        }

        void IPublisherRepository.Update(Publisher obj)
        {
            _db.Update(obj);
        }
    }
}
