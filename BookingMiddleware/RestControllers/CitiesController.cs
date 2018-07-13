using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookingMiddleware.Database;
using BookingMiddleware.Models;
using AutoMapper;
using BookingMiddleware.DTO;

namespace BookingMiddleware.Controllers
{
    public class CitiesController : ApiController
    {
        private BookingDbContext db = new BookingDbContext();

        /// <summary>
        /// Da de alta una lista de ciudades 
        /// </summary>
        /// <param name="_ciudaes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cities/SaveCities")]
        public IHttpActionResult SaveCities(List<City> _ciudaes)
        {     
            
            foreach (var ciudad in _ciudaes)
            {
                City city = new City();
                city.apiId = ciudad.Id;
                city.Name = ciudad.Name;
                city.Country = ciudad.Country;
                db.Cities.Add(city);
                db.SaveChanges(); 

            }
           
            return Ok();
        }


        // GET: api/Cities
        public IHttpActionResult GetCities()
        {
            var getAll = db.Cities.ToList();
            var dto = Mapper.Map<IEnumerable<City>, IEnumerable<CityDTO>>(getAll);
            return Ok(dto) ;
        }

        // GET: api/Cities/5
        [ResponseType(typeof(CityDTO))]
        public IHttpActionResult GetCity(int id)
        {
            City city = db.Cities.Find(id);
            var dto = Mapper.Map<City, CityDTO>(city);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

       
        // POST: api/Cities
        [ResponseType(typeof(CityDTO))]
        public IHttpActionResult PostCity(CityDTO city)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dto = Mapper.Map<CityDTO, City>(city);
            var alta=db.Cities.Add(dto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = city.Id }, alta);
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(CityDTO))]
        public IHttpActionResult DeleteCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            db.Cities.Remove(city);
            db.SaveChanges();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.Id == id) > 0;
        }
    }
}