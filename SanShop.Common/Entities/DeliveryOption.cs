using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanShop.Common.Entities
{
    public class DeliveryOption
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DeliveryDays { get; set; }
    }
}