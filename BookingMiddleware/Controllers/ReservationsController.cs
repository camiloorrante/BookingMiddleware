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
using Newtonsoft.Json;

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
                //HTTP GET
                var responseTask = client.GetAsync("weather?id=2172797&lang=es&APPID=98034ba9627d66e3fd35cf05e0d42ea1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(content.Result.ToString());
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
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
