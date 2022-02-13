using BikeRental.DTO.DTO.Report;

namespace BikeRental.DTO.DTO
{
    public class BikeReportDTO
    {
        public BikeReportDTO()
        {
            UserReservations = new List<UserReservationDTO>();
        }
        public BikeRDTO Bike { get; set; }
        public List<UserReservationDTO> UserReservations { get; set; }
    }

    public class UserReservationDTO
    {
        public UserRDTO User { get; set; }
        public ReservationRDTO Reservation { get; set; }
    }
}