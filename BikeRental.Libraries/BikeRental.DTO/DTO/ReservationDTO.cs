using System.ComponentModel.DataAnnotations;

namespace BikeRental.DTO.DTO
{
    public class ReservationDTO : BaseDTO
    {
        public ReservationDTO()
        {
            User = new UserDTO();
            Bike = new BikeDTO();
        }

        public decimal PerDayRate { get; set; }
        public decimal Total { get; set; }
        public DateTime BookedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BookingDays { get; set; }
        public bool Cancled { get; set; }
        public string? CancelReason { get; set; }
        public UserDTO User { get; set; }
        public BikeDTO Bike { get; set; }
    }

    public class ReservationAddDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter reservation start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter reservation end date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Required]
        public Guid BikeId { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("End Date must be greater than Start Date");
            }
        }
    }

    public class ReservationCancelDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter reason")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Reason must be between 2 to 100 characters")]
        public string Reason { get; set; }
    }
}