using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class CartTotal
    {
        public decimal ShoppingCartTotal { get; set; }
        public List<Cart> CartItems { get; set; }
    }
}
