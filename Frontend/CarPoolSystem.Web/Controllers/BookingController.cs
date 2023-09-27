using CarPoolSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;

namespace CarPoolSystem.Web.Controllers
{
    public class BookingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public BookingController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost]
        public async Task<IActionResult> BookRide(BookModel BookRideModel)
        {
            if (ModelState.IsValid)
            {
                string BookingApiUrl = _configuration["ServiceUrls:BookingAPI"];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BookingApiUrl);
                    // Send the booking data to your Booking API
                    var response = await client.PostAsJsonAsync("api/booking/BookRide", BookRideModel);

                    if (response.IsSuccessStatusCode)
                    {
                        // Booking  was successful. You can redirect or display a success message.
                        return RedirectToAction("Book", "BookRide"); // Redirect to the "Available" action in the "OfferRide" controller
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors if available in the response content.
                        var content = await response.Content.ReadAsStringAsync();
                        // You can log or display the content to diagnose the issue.
                        ModelState.AddModelError(string.Empty, "Booking  failed due to server errors.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Booking  failed. Please try again.");
                    }
                }
            }
            return Ok("Not Booked");
        }

        
    }
}
