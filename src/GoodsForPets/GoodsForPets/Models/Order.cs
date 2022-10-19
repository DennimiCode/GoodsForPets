using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsForPets.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}