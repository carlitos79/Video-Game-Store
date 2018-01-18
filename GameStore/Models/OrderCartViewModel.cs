using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class OrderCartViewModel
    {
        [Display(Name = "Recipient Name")]
        [DataType(DataType.Text)]
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastname { get; set; }
        [Display(Name = "Order Date")]
        [DataType(DataType.Text)]
        public DateTime OrderCreationDate { get; set; }
        public decimal Total { get; set; }
        public List<Cart> Carts { get; set; }
        public string PreviousOrderId { get; set; }
    }
}
