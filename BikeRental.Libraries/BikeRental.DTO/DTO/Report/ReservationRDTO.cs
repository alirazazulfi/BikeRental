namespace BikeRental.DTO.DTO.Report
{
    public class ReservationRDTO
    {
        public decimal PerDayRate { get; set; }
        public decimal Total { get; set; }
        public DateTime BookedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BookingDays { get; set; }
        public bool Cancled { get; set; }
        public string? CancelReason { get; set; }
    }
}