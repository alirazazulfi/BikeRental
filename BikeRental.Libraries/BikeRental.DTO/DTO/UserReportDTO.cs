using BikeRental.DTO.DTO.Report;

namespace BikeRental.DTO.DTO
{
    public class UserReportDTO
    {
        public UserReportDTO()
        {
            BikeReservations = new List<BikeReservationDTO>();
        } 
        public UserRDTO User { get; set; }
        public List<BikeReservationDTO> BikeReservations { get; set; }
    }

    public class BikeReservationDTO
    {
        public BikeRDTO Bike { get; set; }
        public ReservationRDTO Reservation { get; set; }
    }
}