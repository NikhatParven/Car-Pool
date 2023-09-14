using CarPoolSystem.Services.OfferRideAPI.Model.OfferDTO;
using CarPoolSystem.Services.OfferRideAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CarPoolSystem.Services.OfferRideAPI.Controllers;


[ApiController]
[Route("api/OfferRide")]
public class OfferRideController:ControllerBase
{

    private readonly OfferRideService _service;

    public OfferRideController(OfferRideService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // Delegate to the service layer for data retrieval and processing
        var dtoData = _service.GetSomeData();

        return Ok(dtoData); // Return DTO data as a JSON response
    }




    [HttpPost]
    public IActionResult CreateRide([FromBody] OfferDTO offerDTO)
    {
        if (offerDTO == null)
        {
            return BadRequest("Invalid input: offerDTO is null.");
        }


        // You can implement the logic to create a ride here, assuming OfferRideService has a CreateRide method.
        var result = _service.CreateRide(offerDTO);

        if (result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create the ride.");
        }

        // Return a 201 Created response with the created data.
        return CreatedAtAction(nameof(Get), new { id = result.Offer_Id }, result);
    }


    [HttpGet("All")]
    public IActionResult GetAllOfferRides()
    {
        // Delegate to the service layer to retrieve all offer rides
        var allOfferRides = _service.GetAllOfferRides();

        if (allOfferRides == null || allOfferRides.Count == 0)
        {
            return NotFound("No offer rides found.");
        }
     return Ok(allOfferRides); // Return all offer rides as a JSON response
    }

}
