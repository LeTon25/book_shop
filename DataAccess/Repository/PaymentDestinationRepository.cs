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
	public class PaymentDestinationRepository : Repository<PaymentDestination>, IPaymentDestinationRepository
	{
		public PaymentDestinationRepository(AppDbContext db) : base(db)
		{
		}

		public void Update(PaymentDestination PaymentDestination)
		{
			_db.PaymentDestinations.Update(PaymentDestination);
		}
	}
}
