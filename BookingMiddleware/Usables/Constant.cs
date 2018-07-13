using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingMiddleware.Usables
{
    static public class Constant
    {

        static private string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";
        static private string ApiKey = "&APPID=98034ba9627d66e3fd35cf05e0d42ea1";

        /// <summary>
        /// Este metodo regresa el url base de la api openweathermap, se tiene que colocar una cadena 
        /// de texto que contenga los parametros para mas información mira documentación en: https://openweathermap.org/current
        /// </summary>
        /// <param name="_params"></param>
        /// <returns></returns>
        static public string GetUrl(string _params)
        {
            return BaseUrl + _params + ApiKey;
        }
    }
}