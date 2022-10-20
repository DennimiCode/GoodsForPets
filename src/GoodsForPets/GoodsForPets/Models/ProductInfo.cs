using System.Collections.Generic;

namespace GoodsForPets.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public List<Product> Products { get; set; }
        public List<SubProduct> SubProducts { get; set; }
    }
}