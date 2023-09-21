using CarPoolSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace CarPoolSystem.Web.Controllers;

public class OfferRideController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
   
    public OfferRideController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOfferRide(OfferRideModel offerRideViewModel)
    {
        if (ModelState.IsValid)
        {
            string offerRideApiUrl = _configuration["ServiceUrls:OfferRideAPI"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(offerRideApiUrl);
                // Send the offer data to your OfferRide API
                var response = await client.PostAsJsonAsync("api/OfferRide/Offer", offerRideViewModel);
           
                if (response.IsSuccessStatusCode)
                {    
                    // Offer submission was successful. You can redirect or display a success message.
                    return RedirectToAction("Index", "Home"); // Redirect to the "Index" action in the "Home" controller
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    // Handle validation errors if available in the response content.
                    var content = await response.Content.ReadAsStringAsync();
                // You can log or display the content to diagnose the issue.
                  ModelState.AddModelError(string.Empty, "Offer submission failed due to validation errors.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Offer submission failed. Please try again.");
                }
            }
        }

        // If the model state is not valid, redisplay the form with errors
        return View(offerRideViewModel);
    }

    public async Task<ActionResult> CreateOfferRide()
    {
        return View();
    }

    [HttpGet]

    public async Task<IActionResult> AvailableRide()
    {
        string Apiurl = _configuration["ServiceUrls:OfferRideAPI"];
        List<OfferRideModel> avilable = new();

        var client = new HttpClient();
        client.BaseAddress = new Uri(Apiurl);

        var response = await client.GetAsync("api/OfferRide/All"); 
        if(response!=null)
        {
            avilable = await response.Content.ReadFromJsonAsync<List<OfferRideModel>>();
        }
        return View(avilable);
    }

   
   
}
