using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CcAcca.BaseLibrary;
using CcAcca.Library;

namespace Library.Web.Controllers
{
    public class LookupsController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Lookups
        public async Task<ActionResult> Index()
        {
            return View(await db.Lookups.ToListAsync());
        }

        // GET: Lookups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookup = await db.Lookups.FindAsync(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        // GET: Lookups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lookups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                db.Lookups.Add(lookup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lookup);
        }

        // GET: Lookups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookup = await db.Lookups.FindAsync(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        // POST: Lookups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lookup);
        }

        // GET: Lookups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookup = await db.Lookups.FindAsync(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        // POST: Lookups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lookup lookup = await db.Lookups.FindAsync(id);
            db.Lookups.Remove(lookup);
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
