using CarPoolSystem.Services.OfferRideAPI.Model;
using Microsoft.EntityFrameworkCore;


namespace CarPoolSystem.Services.OfferRideAPI.Data;

public class MyContext:DbContext
{

    public DbSet<Offer> Offer { get; set; }
    public DbSet<Category> Category { get; set; }

    public MyContext(DbContextOptions<MyContext>options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
         new Category
         {
               Id = 1,
               Car_Name = "Honda"
         },
         new Category
         {
               Id = 2,
               Car_Name = "Ford"
         },
         new Category
         {
               Id = 3,
               Car_Name = "Tesla Model"
         },
         new Category
         {
               Id = 4,
               Car_Name = "Toyota"
         },
         new Category
         {
               Id = 5,
               Car_Name = "Nissan"
         },
         new Category
         {
               Id = 6,
               Car_Name = "Jeep Grand"
         },
         new Category
         {
               Id = 7,
               Car_Name = "Hyundai"
         },
         new Category
         {
               Id = 8,
               Car_Name = "Subaru"
         },
         new Category
         {
               Id = 9,
               Car_Name = "Crislar Pacifica"
         },
         new Category
         {
               Id = 10,
               Car_Name = "Mazda"
         }          
        );
    }


}
