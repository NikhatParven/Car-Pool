using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace CarPoolSystem.Services.OfferRideAPI.Model
{
    public class Offer
    {
        [Key]
        public  int Offer_Id { get; set; }// Primary key 
        [Required]
        public required string Name { get; set; }
        public required string Source { set; get; }
        public required string Destination { get; set; }
        public int CategoryId { get; set; }
        public Category ? Category { get; set; }// Connecting with the Category table
        public required int Seat_Available { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public required DateTime DepartureTime { get; set; }
        public required long Phone_no { get; set; }

    }
}
