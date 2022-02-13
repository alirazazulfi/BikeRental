namespace BikeRental.Entities.DBEtities
{
    public partial class Reservation : BaseEntity
    {
        public decimal PerDayRate { get; set; }
        public decimal Total { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCancled { get; set; } = false;
        public string? CancelReason { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid BikeId { get; set; }
        public Bike Bike { get; set; }
    }
}
