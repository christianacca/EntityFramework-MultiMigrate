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
    public class EntityMetadatasController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: EntityMetadatas
        public async Task<ActionResult> Index()
        {
            return View(await db.EntityMetadatas.ToListAsync());
        }

        // GET: EntityMetadatas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityMetadata entityMetadata = await db.EntityMetadatas.FindAsync(id);
            if (entityMetadata == null)
            {
                return HttpNotFound();
            }
            return View(entityMetadata);
        }

        // GET: EntityMetadatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntityMetadatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EntityName")] EntityMetadata entityMetadata)
        {
            if (ModelState.IsValid)
            {
                db.EntityMetadatas.Add(entityMetadata);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(entityMetadata);
        }

        // GET: EntityMetadatas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityMetadata entityMetadata = await db.EntityMetadatas.FindAsync(id);
            if (entityMetadata == null)
            {
                return HttpNotFound();
            }
            return View(entityMetadata);
        }

        // POST: EntityMetadatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EntityName")] EntityMetadata entityMetadata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entityMetadata).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(entityMetadata);
        }

        // GET: EntityMetadatas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityMetadata entityMetadata = await db.EntityMetadatas.FindAsync(id);
            if (entityMetadata == null)
            {
                return HttpNotFound();
            }
            return View(entityMetadata);
        }

        // POST: EntityMetadatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EntityMetadata entityMetadata = await db.EntityMetadatas.FindAsync(id);
            db.EntityMetadatas.Remove(entityMetadata);
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
