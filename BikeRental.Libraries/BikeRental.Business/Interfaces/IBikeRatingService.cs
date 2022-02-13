using BikeRental.DTO.DTO;
using BikeRental.DTO.Response;

namespace BikeRental.Business.Interfaces
{
    public interface IBikeRatingService
    {
        Task<DatabaseResponse> GetByBikeId(Guid BikeId);

        Task<DatabaseResponse> GetByUserId(Guid UserId);

        Task<DatabaseResponse> Rate(BikeRatingAddDTO dto);
    }
}
