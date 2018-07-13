using AutoMapper;
using BookingMiddleware.DTO;
using BookingMiddleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingMiddleware.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDTO>();
            CreateMap<Reservation, ReservationDTO>();

        }
    }
}