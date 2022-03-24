using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanShop.Common.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string SellerId { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateAdd { get; set; }
        public bool IsPromoted { get; set; }
        public List<Comment> Comments { get; set; }
        public double AverageNote
        {
            get 
            {
                if (Comments != null && Comments.Any())
                {
                    return Comments.Average(c => c.Note);
                }
                else
                    return 0d;
            }
        }

        public List<string> DeliveryOptionsIds { get; set; }
        public List<DeliveryOption> DeliveryOptions { get; set; }
    }
}