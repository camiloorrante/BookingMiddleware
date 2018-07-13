using BookingMiddleware.Models;
using BookingMiddleware.Usables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BookingMiddleware.Services
{
    public class ReservationServices
    {
        private string UrlApi = Constant.GetPersonalApi();
        private string ServiceName = "ReservationsApi";

        public IEnumerable<Reservation> GetAll()
        {
            IEnumerable<Reservation> reservations = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                //HTTP GET
                var responseTask = client.GetAsync(ServiceName);
                responseTask.Wait();               
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Reservation>>();
                    readTask.Wait();

                    reservations = readTask.Result;
                }
                
            }
            return reservations;
        }

        public Reservation GetById(int id)
        {
           Reservation reservation = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                //HTTP GET
                var responseTask = client.GetAsync(ServiceName + "/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Reservation>();
                    readTask.Wait();

                    reservation = readTask.Result;
                    
                }
                
            }
            return reservation;
        }

        public bool Post(Reservation reservation)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Reservation>(ServiceName, reservation);
                postTask.Wait();

                var result = postTask.Result;

                if (!result.IsSuccessStatusCode) {
                    return false;
                }

                return true;
            }
        }

    }
}