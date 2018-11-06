using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pizza_opgave.Controllers;

namespace Pizza_opgave.Models
{
    public class PizzaViewModel
    {
        public List<pizzas> Pizzas { get; set; }
        public List<OrderPizza> OrderdPizza { get; set; }
    }
}