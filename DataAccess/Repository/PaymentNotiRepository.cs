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
	public class PaymentNotiRepository : Repository<PaymentNotification>, IPaymentNotificationRepository
	{
		public PaymentNotiRepository(AppDbContext db) : base(db)
		{
		}

		public void Update(PaymentNotification paymentNoti)
		{
			_db.PaymentNotifications.Update(paymentNoti);
		}
	}
}
