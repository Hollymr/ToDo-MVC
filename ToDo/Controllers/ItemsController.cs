using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ItemsController : Controller
    {
        private ToDodbEntities2 db = new ToDodbEntities2();

        // GET: Items
        public ActionResult Index()
        {   // **.Include(i => i.List)** figure out what list they are apart of; foreach item get the list (not necessary!!) But will make it run
            //faster for larger apps 
            //var items = db.Items.Include(i => i.List); //takes items and passes them into the view 
          

            //ORDER BY ListID, DueDateTime
            // LINQ!!!!!!! allows you to use SQL like syntax in c#
            // n represents each row can also use i
            var items = from n in db.Items
                orderby n.ListID, n.DueDateTime
                select n;

            return View(items.ToList()); //ToList- takes it from database set and turns it into a c# list 
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.ListID = new SelectList(db.Lists, "ListID", "ListTitle");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,TaskName,ListID,DueDateTime,Details,IsDone")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListID = new SelectList(db.Lists, "ListID", "ListTitle", item.ListID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListID = new SelectList(db.Lists, "ListID", "ListTitle", item.ListID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,TaskName,ListID,DueDateTime,Details,IsDone")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListID = new SelectList(db.Lists, "ListID", "ListTitle", item.ListID);
            return View(item);
        }

        //public ActionResult SetDone(int? id, bool newDoneValue)
        //{

        //}


        //to check IsDone box
        public ActionResult ToggleDone(int? id)
        {
            if (id == null)// if you just go to webpage ToggleDone sends error 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);// looks for item with that id 
            if (item == null)
            {
                return HttpNotFound();
            }
            if (item.IsDone) //treating it as a standard c# object 
            {
                item.IsDone = false;
            }
            else
            {
                item.IsDone = true;
            }                
                db.SaveChanges(); //save back into database; the save all
                return RedirectToAction("Index"); //refresh page
            }
           
        
        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
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
