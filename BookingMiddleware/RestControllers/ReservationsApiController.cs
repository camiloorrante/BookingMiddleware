using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using BookingMiddleware.Database;
using BookingMiddleware.Models;
using AutoMapper;
using BookingMiddleware.DTO;

namespace BookingMiddleware.Controllers
{
    public class ReservationsApiController : ApiController
    {
        private BookingDbContext db = new BookingDbContext();

        // GET: api/Reservations
        public IHttpActionResult GetReservations()
        {
            var getAll = db.Reservations.Include("City").ToList();
            var dto = Mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationDTO>>(getAll);
            return Ok(dto);
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(ReservationDTO))]
        public IHttpActionResult GetReservation(int? id)
        {
            Reservation reservation = 
                db.Reservations.Include("City")
                    .Where(s => s.ReservationId == id)
                    .FirstOrDefault<Reservation>();
            if (reservation == null)
            {
                return NotFound();
            }
            var dto = Mapper.Map<Reservation, ReservationDTO>(reservation);

            return Ok(dto);
        }

      
        // POST: api/Reservations
        [ResponseType(typeof(ReservationDTO))]
        public IHttpActionResult PostReservation(ReservationDTO reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dto = Mapper.Map<ReservationDTO, Reservation>(reservation);
            var result=db.Reservations.Add(dto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservation.ReservationId }, result);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(ReservationDTO))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.ReservationId == id) > 0;
        }
    }
}