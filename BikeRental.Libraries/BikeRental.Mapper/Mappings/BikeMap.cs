using BikeRental.DTO.DTO;
using BikeRental.DTO.DTO.Report;
using BikeRental.Entities.DBEtities;

namespace BikeRental.Mapper.Mappings
{
    public class BikeMap
    {
        public BikeDTO GetDTOResponse(Bike entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Model = entity.Model,
            Color = entity.Color,
            Location = entity.Location,
            PerDayRate = entity.PerDayRate,
            AverageRating = entity.AverageRating,
            Available = entity.IsAvailable
        };

        public Bike Add(BikeAddDTO dto) => new()
        {
            Name = dto.Name,
            Model = dto.Model,
            Color = dto.Color,
            Location = dto.Location,
            PerDayRate = dto.PerDayRate,
            IsAvailable = dto.Available,
            CreatedDate = DateTime.UtcNow
        };

        public Bike Update(Bike entity, BikeUpdateDTO dto)
        {
            entity.Name = dto.Name;
            entity.Model = dto.Model;
            entity.Color = dto.Color;
            entity.Location = dto.Location;
            entity.PerDayRate = dto.PerDayRate;
            entity.ModifiedDate = DateTime.UtcNow;
            entity.IsAvailable = dto.Available;
            return entity;
        }

        public BikeReportDTO GetBikeReportResponse(Bike entity)
        {
            BikeReportDTO dto = new();
            dto.Bike = new BikeRDTO()
            {
                Name = entity.Name,
                Model = entity.Model,
                Color = entity.Color,
                Location = entity.Location,
                AverageRating = entity.AverageRating,
                Available = entity.IsAvailable,
            };

            foreach (var item in entity.Reservations)
            {
                dto.UserReservations.Add(new UserReservationDTO()
                {
                    User = new UserRDTO()
                    {
                        FullName = item.User.FullName,
                        Email = item.User.Email,
                        Mobile = item.User.Mobile
                    },
                    Reservation = new ReservationRDTO()
                    {
                        PerDayRate = item.PerDayRate,
                        Total = item.Total,
                        BookedOn = item.CreatedDate,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        BookingDays = item.BookingDays,
                        Cancled = item.IsCancled,
                        CancelReason = item.CancelDetails
                    }
                });
            }
            return dto;
        }
    }
}
