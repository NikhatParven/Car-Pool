using CarPoolSystem.Services.BookingAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolSystem.Services.BookingAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Ride")]
        public int Offer_Id { get; set; }

    }
}
