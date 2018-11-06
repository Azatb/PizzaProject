using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Pizza_opgave.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("admin/pizza")]
    public class PizzasController : Controller
    {
        private readonly PizzaEntities _db = new PizzaEntities();

        [Route("list")]
        public ActionResult Index()
        {
            return View(_db.pizzas.ToList());
        }

        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,topping,alm,fam")] pizzas pizzas)
        {
            if (ModelState.IsValid)
            {
                _db.pizzas.Add(pizzas);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pizzas);
        }

        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pizzas pizzas = _db.pizzas.Find(id);
            if (pizzas == null)
            {
                return HttpNotFound();
            }
            return View(pizzas);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,topping,alm,fam")] pizzas pizzas)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(pizzas).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizzas);
        }

        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pizzas pizzas = _db.pizzas.Find(id);
            if (pizzas == null)
            {
                return HttpNotFound();
            }
            return View(pizzas);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pizzas pizzas = _db.pizzas.Find(id);
            _db.pizzas.Remove(pizzas);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
