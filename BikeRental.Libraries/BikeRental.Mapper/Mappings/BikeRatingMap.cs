using BikeRental.DTO.DTO;
using BikeRental.Entities.DBEtities;

namespace BikeRental.Mapper.Mappings
{
    public class BikeRatingMap
    {
        public BikeRatingDTO GetDTOResponse(BikeRating entity) => new()
        {
            Rating = entity.Rating,
            User = new UserMap().GetDTOResponse(entity.User),
            Bike = new BikeMap().GetDTOResponse(entity.Bike),
        };

        public BikeRating Add(BikeRatingAddDTO dto, Guid UserId) => new()
        {
            Rating = dto.Rating,
            UserId = UserId,
            BikeId = dto.BikeId,
            CreatedDate = DateTime.UtcNow
        };
    }
}
