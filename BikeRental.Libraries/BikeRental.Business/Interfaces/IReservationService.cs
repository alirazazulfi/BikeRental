using BikeRental.DTO.DTO;
using BikeRental.DTO.Response;

namespace BikeRental.Business.Interfaces
{
    public interface IReservationService
    {
        Task<DatabaseResponse> GetByUserId();
        Task<DatabaseResponse> Add(ReservationAddDTO dto);
        Task<DatabaseResponse> Cancel(ReservationCancelDTO dto);
    }
}