using RocketEshop.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.ApplicationCore.Models
{
    public class ShoppingCartItem
    {

        [Key]
        public int Id { get; set; }

        public Game Game { get; set; }
        public int Amount { get; set; }


        public string ShoppingCartId { get; set; }

    }
}
