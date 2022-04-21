using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanShop.Common.Models
{
    public class LoginResult
    {
        public bool Result { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
    }
}
