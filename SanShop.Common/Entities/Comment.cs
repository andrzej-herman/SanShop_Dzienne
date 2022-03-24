using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanShop.Common.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Note { get; set; }
        public string Text { get; set; }
    }
}