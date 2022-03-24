using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanShop.Common.Entities
{
    public class ProductDelivery
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string DeliveryOptionId { get; set; }
    }
}