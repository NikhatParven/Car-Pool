using CarPoolSystem.Services.BookingAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolSystem.Services.BookingAPI.Models
{
	public class Booking
	{

		[Key]
		public int BookingId { get; set; }
		public int? UserId { get; set; }
		[ForeignKey("UserId")]
		public int? RideId { get; set; }
		[ForeignKey("RideId")]

		[NotMapped]
		public UserDto User { get; set; }

		[NotMapped]
		public RideDto Ride { get; set; }	


	}
}
