using System.ComponentModel.DataAnnotations;

namespace BikeRental.DTO.DTO
{
    public class BikeRatingDTO
    {
        public BikeRatingDTO()
        {
            User = new UserDTO();
            Bike = new BikeDTO();
        }
        public int Rating { get; set; }
        public UserDTO User { get; set; }
        public BikeDTO Bike { get; set; }
    }

    public class BikeRatingAddDTO
    {
        [Required(ErrorMessage = "Please enter rating from 1 to 5")]
        [Range(1, 5, ErrorMessage = "Please enter rating from 1 to 5")]
        public int Rating { get; set; }

        [Required]
        public Guid BikeId { get; set; }
    }
}