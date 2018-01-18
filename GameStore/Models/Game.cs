using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string GameImage { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Display(Name = "Units in stock")]
        [DataType(DataType.Text)]
        public int UnitsInStock { get; set; }
    }
}
