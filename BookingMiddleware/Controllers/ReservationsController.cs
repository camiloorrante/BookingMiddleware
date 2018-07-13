using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BookingMiddleware.Database;
using BookingMiddleware.Models;
using System.Net.Http;
using BookingMiddleware.Usables;
using System.Threading.Tasks;
using BookingMiddleware.Services;

namespace BookingMiddleware.Controllers
{
    public class ReservationsController : Controller
    {
        private BookingDbContext db = new BookingDbContext();
        private ReservationServices service = new ReservationServices();
        private ReservationViewModel reservationViewModel = new ReservationViewModel();
        static HttpClient client = new HttpClient();
        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = service.GetAll();
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int id)
        {
                       
            ReservationViewModel reservationDetail = new ReservationViewModel();
            Reservation reservation = service.GetById(id);
            reservationDetail.Reservation = reservation;

            return View(reservationDetail);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            reservationViewModel.Reservation = new Reservation();
            reservationViewModel.Cities = db.Cities.ToList();

            return View(reservationViewModel);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,ClientName,ClientLastName,Email,Duration,CityID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {

                bool result = service.Post(reservation);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                return RedirectToAction("Details");
            }                        

            return RedirectToAction("Create");
        }






        // POST: Reservations/Edit/5
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
