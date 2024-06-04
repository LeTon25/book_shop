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
    internal class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext db) : base(db)
        {
        }

        public void Update(OrderDetail obj)
        {
            _db.Update(obj);
        }
    }
}
