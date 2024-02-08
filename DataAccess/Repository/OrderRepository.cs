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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext db) : base(db)
        {
        }

        public void Update(Order obj)
        {
            _db.Update(obj);
        }

        public void UpdateStatus(int ID, string? orderStatus = null, string? paymentStatus = null)
        {
           Order order =  _db.Orders.FirstOrDefault(c=>c.ID ==ID);
           if (orderStatus != null) 
           {
                order.OrderStatus = orderStatus; 
           }
           if (paymentStatus != null) 
           {
                order.PaymentStatus = paymentStatus;
           }
            _db.Update(order);
        }

    }
}
