using BikeRental.DTO.DTO;
using BikeRental.DTO.DTO.Search;
using BikeRental.DTO.Response;

namespace BikeRental.Business.Interfaces
{
    public interface IBikeService
    {
        Task<DatabaseResponse> Get();
        /// <summary>
        /// Get  Bike By Id With Relational Entities.
        /// </summary>
        /// <param name="Id"> Bike Id<</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> GetById(Guid Id);
        /// <summary>
        /// Add  Bike.
        /// </summary>
        /// <param name="dto">BikeAddDTO</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> Add(BikeAddDTO dto);
        /// <summary>
        /// Update  Bike.
        /// </summary>
        /// <param name="dto">Object Of BikeUpdateDTO</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> Update(BikeUpdateDTO dto);
        /// <summary>
        /// Delete  Bike.
        /// </summary>
        /// <param name="Id"> Bike Id<</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> Delete(Guid Id);
        Task<DatabaseResponse> Search(BikeSearchDTO dto);
        Task<DatabaseResponse> BikeReport();
        Task<DatabaseResponse> AvailableBikes(AvailableBikeDTO dto);
    }
}