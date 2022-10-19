using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GoodsForPets.Models
{
    [Index(nameof(Street), IsUnique = true)]
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required, MaxLength(300)]
        public string Street { get; set; }

        public List<Location> Locations { get; set; }
    }
}
