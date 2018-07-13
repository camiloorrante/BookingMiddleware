using BookingMiddleware.Models;
using BookingMiddleware.Usables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BookingMiddleware.Services
{
    public class WeatherService
    {
        private string ServiceParams = "ReservationsApi";       

        public WeatherResponse GetDetail(string id)
        {
            WeatherResponse weather = new WeatherResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constant.GetUrlOpenWeatherMapApi(id));
                //HTTP GET
                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<WeatherResponse>();
                    readTask.Wait();

                    weather = readTask.Result;
                }

            }
            return weather;

        }
    }
}