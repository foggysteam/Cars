using Cars.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cars.Controllers
{
    public class HomeController : Controller
    {
        private CarsEntities1 db = new CarsEntities1();
        public ActionResult Index(string carMake, string searchString)
        {
            var MakeList = new List<string>();
            var MakeQuery = from m in db.Cars
                           orderby m.Make
                           select m.Make;
            MakeList.AddRange(MakeQuery.Distinct());
            ViewBag.carMake = new SelectList(MakeList);
            
            var cars = from c in db.Cars
                         select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(s => s.Model.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(carMake))
            {
                cars = cars.Where(x => x.Make == carMake);
            }
            return View(cars);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Picture,Make,Model,Year,Engine,Price")] Car car)
        {
            if (car.Picture == null)
            {
                car.Picture = "http://www.ahmadalasadi.co.uk/images/car.jpg";
            }

            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture,Make,Model,Year,Engine,Price")] Car car)
        {
            if (car.Picture == null)
            {
                car.Picture = "http://www.ahmadalasadi.co.uk/images/car.jpg";
            }
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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



  
            
            

            

            

