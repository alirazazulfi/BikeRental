using System.ComponentModel.DataAnnotations;

namespace BikeRental.DTO.DTO
{
    public class BikeDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public decimal PerDayRate { get; set; }
        public double AverageRating { get; set; }
        public bool Available { get; set; }
    } 

    public class BikeAddDTO
    {
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 to 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters are not allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter model")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Model must be between 2 to 50 characters")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please enter color")]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "Color must be between 2 to 20 characters")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters are not allowed")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Please enter location")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Location must be between 2 to 100 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter rate")]
        public decimal PerDayRate { get; set; }
        public bool Available { get; set; }

        public bool Active { get; set; }
    }

    public class BikeUpdateDTO : BikeAddDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}