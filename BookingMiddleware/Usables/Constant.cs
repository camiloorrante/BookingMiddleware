using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingMiddleware.Usables
{
    static public class Constant
    {

        static private string OpenWeatherMapApiUrl = "http://api.openweathermap.org/data/2.5/weather?lang=es&units=metric&id=";
        static private string ApiKey = "&APPID=98034ba9627d66e3fd35cf05e0d42ea1";
        static public string BaseUrl = string.Format("{0}://{1}{2}{3}",
              System.Web.HttpContext.Current.Request.Url.Scheme,
              System.Web.HttpContext.Current.Request.Url.Host,
              System.Web.HttpContext.Current.Request.Url.Port == 80 ? string.Empty : ":" + System.Web.HttpContext.Current.Request.Url.Port,
              System.Web.HttpContext.Current.Request.ApplicationPath);

        /// <summary>
        /// Este metodo regresa el url base de la api openweathermap, se tiene que colocar una cadena 
        /// de texto que contenga los parametros para mas información mira documentación en: https://openweathermap.org/current
        /// </summary>
        /// <param name="_params"></param>
        /// <returns></returns>
        static public string GetUrlOpenWeatherMapApi(string _params)
        {
            return OpenWeatherMapApiUrl + _params + ApiKey;
        }

        /// <summary>
        /// Esta es la ruta base de nuestra api 
        /// </summary>
        /// <param name="_controller"></param>
        /// <returns></returns>
        static public string GetPersonalApi()
        {
            return BaseUrl+"api/";
        }
    }
}