using System.Collections.Generic;

namespace GoodsForPets.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductInfo> ProductsInfo { get; set; }
    }
}