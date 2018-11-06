using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza_opgave.Repository
{
    public class PizzaRepository
    {
        public List<pizzas> GetAll()
        {
            using (var db = new PizzaEntities())
            { 
                return db.pizzas.ToList();
            }
        
          

        } 
    }
}