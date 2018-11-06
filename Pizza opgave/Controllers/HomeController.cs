using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pizza_opgave.Models;
using Pizza_opgave.Repository;

namespace Pizza_opgave.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        
        [Route("home/index")]
        public ActionResult Redirect()
        {
            return Redirect(Url.Content("~/"));
        }

        [Route("")]
        public ActionResult Index()
        {

            // Henter all pizza'erne 
            var pizzaRepository = new PizzaRepository();
            var pizzaList = pizzaRepository.GetAll();

            // Indsætter alle pizza'er inde i viewModel
            var viewModel = new PizzaViewModel();
            viewModel.Pizzas = pizzaList; 
            viewModel.OrderdPizza = Session["shoppingCart"] as List<OrderPizza>;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddToBucket(string pizzaId, string pizzaName, string pizzaTopping, string price)
        {
 
            // Vores session shoppingCart skal kun indeholde en liste af bestilte pizza'er
            var shoppingCart = Session["shoppingCart"] as List<OrderPizza>;

            // Da det er første gang vi kører programmet, vil variablen vær null. Vi omdanner derfor variablen til en tom liste af bestilte pizza'er
            if (shoppingCart == null)
                shoppingCart = new List<OrderPizza>();

            // Vi omdanner vores parametere til et pizza objekt
            var pizza = new OrderPizza
            {
                id = int.Parse(pizzaId),
                name = pizzaName,
                topping = pizzaTopping,
                price = int.Parse(price)
            };

            // Tilføj pizzaen til indkøbskurven
            shoppingCart.Add(pizza);

            // Opdater session med den nyeste kurv.
            Session["shoppingCart"] = shoppingCart;
            return null;

        }

        [HttpPost]
        public ActionResult RemoveFromBucket(int pizzaId)
        {

            // Vores session shoppingCart skal kun indeholde en liste af bestilte pizza'er
            var shoppingCart = Session["shoppingCart"] as List<OrderPizza>;
            if (shoppingCart == null)
                return null;

            var selectedPizza = shoppingCart.FirstOrDefault(x => x.id == pizzaId);
            if (selectedPizza == null)
                return null;

            // Fjern pizzaen fra kurven.
            shoppingCart.Remove(selectedPizza);
             
            // Opdater session med den nyeste kurv.
            Session["shoppingCart"] = shoppingCart;
            return null; 
        }
    }

    public class OrderPizza
    {
        public int id { get; set; }
        public string name { get; set; }
        public string topping { get; set; }
        public int price { get; set; } 
    }
}