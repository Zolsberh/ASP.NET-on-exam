﻿using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using RaceContext;
using Service;
using System.Net;

namespace WebCarRace.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        RaceCarContext db;
        private IService _service = null;

       
        public AdminController(IService service, RaceCarContext context)
        {
            _service = service;
            db = context;
        }
        //
        // GET: /Admin/Admin/
        public ActionResult ListOfRaces()
        {
            return View(_service.GetAllRaces());
        }

        public ActionResult CreateRace(int? id)
        {
            if (id != null)
            {
                Race race = db.Races.FirstOrDefault(r => r.RaceID == id);
                return View(race);
            }
            else
            {
                return View();
            }
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRace(Race race, string action)
        {
            if(ModelState.IsValid)
            {
                if (action == "Create")
                {
                    db.Races.Add(race);
                    db.SaveChanges();
                    return RedirectToAction("CreateRace", new { id = race.RaceID });
                }
                else if (action == "Start Race")
                {
                    return RedirectToAction("ListOfRaces");
                } 
            }
            return View(race);
        }

        [HttpPost]
        public ActionResult FindRace(string nameRace)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("SearchRaceView", _service.GetNameRace(nameRace));
            }
            else
            {
                return View("ListOfRaces", _service.GetAllRaces());
            }

        }


        //
        [HttpPost]
        public ActionResult DetailsCar(int? carID)
        {
            if(carID != null)
            {
                return PartialView(_service.GetCar((int)carID));
            }
            else
            {
                return PartialView();
            }
           
        }

        //
        // GET: /Admin/Admin/Create
        public ActionResult CreateCar()
        {
            //if(id != null)
            //{
            //    db.Races.Where(r => r.RaceID == id);
            //}
            return View();
        }

        //
        // POST: /Admin/Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCar(Car car, int id)
        {
            if(ModelState.IsValid)
            {
                Race race = _service.GetRace(id); //db.Races.FirstOrDefault(r => r.RaceID == id);
                if (race.Cars == null)
                {
                    race.Cars = new List<Car>();
                }
                race.Cars.Add(car);
                //db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("CreateRace", new { id = race.RaceID });
            }
            return View(car);
        }

        //
        // GET: /Admin/Admin/Edit/5
        public ActionResult Edit(int? carID)
        {
            if (carID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car nextCar = _service.GetCar((int)carID);
            if (nextCar == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", nextCar);
        }

        //
        // POST: /Admin/Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Object nextCar)
        {
            if (ModelState.IsValid)
            {
                var newCar= db.Cars.Where(s => s.CarID == (nextCar as Car).CarID).FirstOrDefault();
                newCar.NameCar = (nextCar as Car).NameCar;
                newCar.Speed = (nextCar as Car).Speed;
                newCar.DeltaAcceleration = (nextCar as Car).DeltaAcceleration;
                newCar.AccelerationInterval = (nextCar as Car).AccelerationInterval;
                newCar.DurationOfAcceleration = (nextCar as Car).DurationOfAcceleration;
                db.SaveChanges();
                return RedirectToAction("ListOfRaces");
            }
            return View(nextCar);
        }

        //
        // GET: /Admin/Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
