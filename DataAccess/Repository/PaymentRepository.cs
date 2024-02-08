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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        
        public PaymentRepository(AppDbContext db) : base(db)
        {
        }
        public void Update(Payment obj)
        {
            _db.Payments.Update(obj);
        }
    }
}
