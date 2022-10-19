using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsForPets.Models
{
    public class BaseMaterial
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Required, MaxLength(20)]
        public string Unit { get; set; }

        public List<Mixture> Mixtures { get; set; }
        public List<SubProduct> SubProducts { get; set; }
    }
}