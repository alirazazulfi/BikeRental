namespace BikeRental.Entities.DBEtities
{
    public class BikeRating : BaseEntity
    {
        public int Rating { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid BikeId { get; set; }
        public Bike Bike { get; set; }
    }
}
