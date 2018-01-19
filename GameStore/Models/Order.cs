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
        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Identity Number")]
        [DataType(DataType.Text)]
        public string IdentityNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public decimal Total { get; set; }
        [Display(Name = "Order Date")]
        [DataType(DataType.Text)]
        public DateTime OrderCreationDate { get; set; }
    }
}
