using System.Collections.Generic;

namespace GoodsForPets.Models
{
    public class GrocerySet
    {
        public int Id { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string SubProductId { get; set; }
        public SubProduct SubProduct { get; set; }
    }
}