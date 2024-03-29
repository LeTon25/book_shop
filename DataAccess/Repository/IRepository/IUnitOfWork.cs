﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        ICompanyRepository CompanyRepo { get; }
        IShoppingCartRepository ShoppingCartRepo { get; }
        IApplicationUserRepository ApplicationUserRepo { get; }
        IOrderDetailRepository OrderDetailRepo { get; }
        IOrderRepository OrderRepo { get; }   
        IPaymentDestinationRepository PaymentDestinationRepo { get; }
        IPaymentRepository PaymentRepo { get; }
        IPaymentTransactionRepository PaymentTransactionRepo { get; }   
        IPaymentSignatureRepository PaymentSignatureRepo { get; }
        IPaymentNotificationRepository PaymentNotificationRepo { get; }  
        IProductImageRepository ProductImageRepo { get; }
        public void Save();
    }
}
