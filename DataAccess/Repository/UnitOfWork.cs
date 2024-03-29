﻿using BookyStore.DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public ICategoryRepository CategoryRepo { get; private set; }
        public IProductRepository ProductRepo { get; private set; }
        public ICompanyRepository CompanyRepo { get; private set; } 
        public IShoppingCartRepository ShoppingCartRepo { get; private set; }
        public IApplicationUserRepository ApplicationUserRepo { get; private set; }
        public IOrderRepository OrderRepo { get; private set; } 
        public IOrderDetailRepository   OrderDetailRepo { get; private set; }

		public IPaymentDestinationRepository PaymentDestinationRepo { get; private set; }

		public IPaymentRepository PaymentRepo { get; private set; }

		public IPaymentTransactionRepository PaymentTransactionRepo { get; private set; }

		public IPaymentSignatureRepository PaymentSignatureRepo { get; private set; }

		public IPaymentNotificationRepository PaymentNotificationRepo { get; private set; }
        public IProductImageRepository ProductImageRepo { get; private set; }   

		public UnitOfWork(AppDbContext db)
        {
            _db = db;
            CategoryRepo = new CategoryRepository(_db);
            ProductRepo = new ProductRepository(_db);
            CompanyRepo = new CompanyRepository(_db);
            ShoppingCartRepo = new ShoppingCartRepository(_db); 
            ApplicationUserRepo = new ApplicationUserRepository(_db);   
            OrderDetailRepo = new OrderDetailRepository(_db);
            OrderRepo = new OrderRepository(_db);
            PaymentRepo = new PaymentRepository(_db);   
            PaymentDestinationRepo = new PaymentDestinationRepository(_db); 
            PaymentSignatureRepo = new PaymentSignatureRepository(_db); 
            PaymentNotificationRepo = new PaymentNotiRepository(_db);
            ProductImageRepo = new ProductImageRepository(_db); 
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
