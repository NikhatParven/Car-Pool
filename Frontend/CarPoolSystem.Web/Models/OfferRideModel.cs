﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarPoolSystem.Web.Models
{
    public class OfferRideModel
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string Destination { get; set;}
        public string Car_Name { get; set; }
       
        public int Seat_Available { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone Number must be 10 digits.")]
        public string Phone_no { get; set; }

      /*
        [DataType(DataType.DateTime)]*/
        public DateTime DepartureTime { get; set; }
    }
}
