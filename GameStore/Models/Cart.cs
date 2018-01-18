using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class Cart
    {
        [Display(Name = "Cart Id")]
        [DataType(DataType.Text)]
        public int CartId { get; set; }
        [Required]
        [Display(Name = "Cart Id")]
        [DataType(DataType.Text)]
        public string ShoppingCartId { get; set; }
        [Display(Name = "Game Id")]
        [DataType(DataType.Text)]
        public int GameId { get; set; }
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string GameTitle { get; set; }
        [Display(Name = "Price")]
        [DataType(DataType.Text)]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Sub-total")]
        [DataType(DataType.Text)]
        public decimal Total { get; set; }
        [Display(Name = "Order Date")]
        [DataType(DataType.Text)]
        public DateTime OrderDate { get; set; }
        public Game GameInfo { get; set; }
        public decimal FinalTotal { get; set; }
        public int Count { get; set; }
        public bool CanAdd { get; set; }
    }
}
