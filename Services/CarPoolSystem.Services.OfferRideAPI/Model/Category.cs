using System.ComponentModel.DataAnnotations;

namespace CarPoolSystem.Services.OfferRideAPI.Model
{
    public class Category
    {
        IEnumerable<Offer> Offer { get; set; }
        public Category()
        {
            Offer = new HashSet<Offer>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Car_Name { get; set; }
    }
}
