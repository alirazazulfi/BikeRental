using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BikeRental.Entities.DBEtities
{
    public partial class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<BikeRating> BikeRatings { get; set; }


    }
}
