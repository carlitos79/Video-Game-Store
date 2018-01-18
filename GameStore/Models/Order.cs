using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderShoppingCartId { get; set; }
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Display(Name = "Identity Number")]
        [DataType(DataType.Text)]
        public string IdentityNo { get; set; }
        public string Address { get; set; }
        [Display(Name = "Postal Code")]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        [Display(Name = "Order Date")]
        [DataType(DataType.Text)]
        public DateTime OrderCreationDate { get; set; }
    }
}
