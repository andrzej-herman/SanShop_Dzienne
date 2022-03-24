using SanShop.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanShop.Common.Models
{
    public class RegisterResult
    {
        public User User { get; set; }
        public string Result { get; set; }
    }   
}