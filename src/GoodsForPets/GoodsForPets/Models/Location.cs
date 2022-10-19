using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace GoodsForPets.Models
{
    [Index(nameof(PostCode), IsUnique = true)]
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public int PostCode { get; set; }
        public int HouseNumber { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public List<Order> Orders { get; set; }
    }
}