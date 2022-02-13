namespace BikeRental.Entities.DBEtities
{
    public partial class Bike : BaseEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public decimal PerDayRate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<BikeRating> BikeRatings { get; set; }
    }
}
