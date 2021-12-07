using E_Shop.Models.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Data.Models
{
    public class Order
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Cart> CartItems { get; set; } = new List<Cart>();
    }
}
