using CarPoolSystem.Services.OfferRideAPI.Model;
using Microsoft.EntityFrameworkCore;


namespace CarPoolSystem.Services.OfferRideAPI.Data;

public class MyContext:DbContext
{

   
    public MyContext(DbContextOptions<MyContext>options):base(options)
    { 
    }
    public DbSet<Offer> Offer { get; set; }
   

}



