using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CcAcca.Library;

namespace Library.Web.Controllers
{
    public class OrdersController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.OrderStatus).Include(o => o.OrderRecommendation);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.OrderStatusId = GetOrderStatusSelectList();
            ViewBag.OrderRecommendationId = GetOrderRecommendationSelectList();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderDate,CustomerName,OrderStatusId,OrderRecommendationId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderStatusId = GetOrderStatusSelectList(order);
            ViewBag.OrderRecommendationId = GetOrderRecommendationSelectList();
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderStatusId = GetOrderStatusSelectList();
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderDate,CustomerName,OrderStatusId,OrderRecommendationId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderStatusId = GetOrderStatusSelectList(order);
            ViewBag.OrderRecommendationId = GetOrderRecommendationSelectList();
            return View(order);
        }

        private SelectList GetOrderStatusSelectList(Order order = null)
        {
            int? orderStatusId = order != null ? order.OrderStatusId : (int?) null;
            return new SelectList(db.LookupItems.Where(x => x.Lookup.Name == "Order Status"), "Id", "Description", orderStatusId);
        }

        private SelectList GetOrderRecommendationSelectList(Order order = null)
        {
            int? orderRecommendationId = order != null ? order.OrderRecommendationId : (int?)null;
            var query = db.LookupItems.OfType<CustomLookupItem>()
                .Where(x => x.Lookup.Name == "Order Recommendation")
                .OrderBy(i => i.Sequence);
            return new SelectList(query, "Id", "Description", orderRecommendationId);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.Include(o => o.OrderStatus)
                .Include(o => o.OrderRecommendation)
                .SingleOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
