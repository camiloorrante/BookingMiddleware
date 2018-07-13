using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingMiddleware.Database;
using BookingMiddleware.Models;

namespace BookingMiddleware.Controllers
{
    public class ReservationsController : Controller
    {
        private BookingDbContext db = new BookingDbContext();
        private ReservationViewModel reservationViewModel = new ReservationViewModel();

        // GET: Reservations1
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.City);
            return View(reservations.ToList());
        }

        // GET: Reservations1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations1/Create
        public ActionResult Create()
        {
            reservationViewModel.Reservation = new Reservation();
            reservationViewModel.Cities = db.Cities.ToList();

            return View(reservationViewModel);
        }

        // POST: Reservations1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,ClientName,ClientLastName,Email,Duration,CityID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "Id", "Name", reservation.CityId);
            return View(reservation);
        }

        // POST: Reservations1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,ClientName,ClientLastName,Email,Duration,CityID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "Id", "Name", reservation.CityId);
            return View(reservation);
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
