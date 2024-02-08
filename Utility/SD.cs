using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public const string Role_Company = "Company";

        public const string StatusPending = "Đang xác nhận";
        public const string StatusApproved = "Đã xác nhận";
        public const string StatusInProcess = "Đang giao";
        public const string StatusShipped = "Đã giao";
        public const string StatusCancelled = "Đã hủy";
        public const string StatusRefunded = "Trả hàng";

        public const string PaymentStatusPending = "Đang xác nhận";
        public const string PaymentStatusApproved = "Đã thanh toán";
        public const string PaymentStatusDelayed = "Đang tạm hoãn";
        public const string PaymentStatusRejected = "Bị từ chối";

        public const string CART_KEY = "CART";
    }
}
