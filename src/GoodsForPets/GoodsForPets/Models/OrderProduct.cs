using System.ComponentModel.DataAnnotations;

namespace GoodsForPets.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        [Required]
        public int ProductAmount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

    }
}