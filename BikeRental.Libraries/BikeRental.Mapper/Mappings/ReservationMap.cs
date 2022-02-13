using BikeRental.DTO.DTO;
using BikeRental.Entities.DBEtities;

namespace BikeRental.Mapper.Mappings
{
    public class ReservationMap
    {
        public ReservationDTO GetDTOResponse(Reservation entity) => new()
        {
            Id = entity.Id,

            BookedOn = entity.CreatedDate,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,

            PerDayRate = entity.PerDayRate,
            Total = entity.Total,

            BookingDays = entity.BookingDays,

            Cancled = entity.IsCancled,
            CancelReason = entity.CancelDetails,

            User = new UserMap().GetDTOResponse(entity.User),
            Bike = new BikeMap().GetDTOResponse(entity.Bike)
        };

        public Reservation Add(ReservationAddDTO dto, decimal PerDayRate, Guid UserId) => new()
        {
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,

            PerDayRate = PerDayRate,
            Total = (decimal)((dto.EndDate - dto.StartDate).TotalDays + 1) * PerDayRate,

            BikeId = dto.BikeId,
            UserId = UserId,
            CreatedDate = DateTime.UtcNow
        };

        public Reservation Cancel(Reservation entity, ReservationCancelDTO dto)
        {
            entity.IsCancled = true;
            entity.CancelReason = dto.Reason;
            entity.ModifiedDate = DateTime.UtcNow;
            return entity;
        }
    }
}
