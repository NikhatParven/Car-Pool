using CarPoolSystem.Services.OfferRideAPI.Data;
using CarPoolSystem.Services.OfferRideAPI.Model;
using CarPoolSystem.Services.OfferRideAPI.Model.DTO;
using CarPoolSystem.Services.OfferRideAPI.Model.OfferDTO;
using Microsoft.EntityFrameworkCore;
using System;


namespace CarPoolSystem.Services.OfferRideAPI.Services;

public class OfferRideService
{
    private readonly MyContext _dbContext;
 

    public OfferRideService(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<CategoryDTO> GetSomeData()
    {
        // Perform data retrieval and business logic here


        var dataFromDatabase = _dbContext.Category.ToList();
        var dtoData = dataFromDatabase.Select(category => new CategoryDTO
        {
            // Map properties from the entity to DTO
            Car_Name = category.Car_Name
        }).ToList();

        return dtoData;
    }

    public OfferDTO CreateRide(OfferDTO offerDTO)
    {
        // Validate input (you can add more validation as needed)
        if (offerDTO == null)
        {
            return null; // Return null to indicate invalid input
        }

        TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        // Map OfferDTO to the entity (assuming you have an Offer entity)
        var Offer = new Offer
        {
            Name = offerDTO.Name,
            Source = offerDTO.Source,
            Destination = offerDTO.Destination,
            Seat_Available = offerDTO.Seat_Available,

            // Convert UTC to IST
            DepartureTime =offerDTO.DepartureTime = TimeZoneInfo.ConvertTimeFromUtc(offerDTO.DepartureTime, istTimeZone),
            Phone_no = offerDTO.Phone_no
        };

        try
        {
            // Fetch the corresponding Category entity based on Category_Id
            var category = _dbContext.Category.SingleOrDefault(c => c.Car_Name == offerDTO.CarName);

            if (category == null)
            {
                return null; // Category not found, handle accordingly (e.g., return an error)
            }

            // Assign the Category entity to the Offer entity
            Offer.Category = category;

            // Add the entity to the DbContext and save changes to the database
            _dbContext.Offer.Add(Offer);
            _dbContext.SaveChanges();

            // Map the created entity back to DTO and return it
            offerDTO.Offer_Id = Offer.Offer_Id; // Set the Offer_Id property to the generated ID
            return offerDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<OfferDTO> GetAllOfferRides()
    {
        try
        {
            // Retrieve all offer rides from the database
            var offerRides = _dbContext.Offer
                .Include(o => o.Category) // Include related Category data if needed
                .ToList();

            // Map the Offer entities to OfferDTO
            var offerDTOs = offerRides.Select(offer => new OfferDTO
            {
                Offer_Id = offer.Offer_Id,
                Name = offer.Name,
                Source = offer.Source,
                Destination = offer.Destination,
                CarName = offer.Category?.Car_Name, // Assuming Category is related
                Seat_Available = offer.Seat_Available,
                DepartureTime = TimeZoneInfo.ConvertTimeToUtc(offer.DepartureTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")),
                Phone_no = offer.Phone_no
            }).ToList();

            return offerDTOs;
        }
        catch (Exception ex)
        {
            // Handle any exceptions or errors appropriately
            return null;
        }
    }
}
