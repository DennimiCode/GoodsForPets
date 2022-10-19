using System.Collections.Generic;

namespace GoodsForPets.Models
{
    public class Mixture
    {
        public int Id { get; set; }

        public int BaseMaterialId { get; set; }
        public BaseMaterial BaseMaterial { get; set; }
        public int RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }
    }
}
