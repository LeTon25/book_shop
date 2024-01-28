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
	public class PaymentSignatureRepository : Repository<PaymentSignature>, IPaymentSignatureRepository
	{
		public PaymentSignatureRepository(AppDbContext db) : base(db)
		{
		}

		public void Update(PaymentSignature PaymentSignature)
		{
			_db.PaymentSignatures.Update(PaymentSignature);
		}
	}
}
