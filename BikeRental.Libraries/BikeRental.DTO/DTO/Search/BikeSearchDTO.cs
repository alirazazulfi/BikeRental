using System.ComponentModel.DataAnnotations;

namespace BikeRental.DTO.DTO.Search
{
    public class BikeSearchDTO: IValidatableObject
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public decimal StartRate { get; set; }
        public decimal EndRate { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndRate < StartRate)
            {
                yield return new ValidationResult("End Rate must be greater than Start Rate");
            }
        }
    }
}