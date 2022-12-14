using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsForPets.Models
{
    public class SubProduct
    {
        [Key, MaxLength(100)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Cost { get; set; }
        public int MaxDiscountAmount { get; set; }
        public int CurrentDiscountAmount { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public string Description { get; set; }
        public string Photo { get; set; }

        public SubProduct()
        {
            Photo = null;
            MaxDiscountAmount = 0;
            CurrentDiscountAmount = 0;
        }

        public int BaseMaterialId { get; set; }
        public BaseMaterial BaseMaterial { get; set; }
        public int ProductInfoId { get; set; }
        public ProductInfo ProductInfo { get; set; }

        public List<GrocerySet> GrocerySets { get; set; }
    }
}