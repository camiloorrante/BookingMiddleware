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
using BookingMiddleware.Usables;

namespace BookingMiddleware.Controllers
{
    public class ReservationsController : Controller
    {
        private BookingDbContext db = new BookingDbContext();
        private ReservationViewModel reservationViewModel = new ReservationViewModel();
        static HttpClient client = new HttpClient();
        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.City);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
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

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:64888/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Reservation>("api/ReservationsApi", reservation);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
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
