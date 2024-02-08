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
	public class PaymentTransactionRepository : Repository<PaymentTransaction>, IPaymentTransactionRepository
	{
		public PaymentTransactionRepository(AppDbContext db) : base(db)
		{
		}

		public void Update(PaymentTransaction PaymentTransaction)
		{
			_db.PaymentTransactions.Update(PaymentTransaction);
		}
	}
}
