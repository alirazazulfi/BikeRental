using BikeRental.DTO.DTO;
using BikeRental.DTO.Response;

namespace BikeRental.Business.Interfaces
{
    public interface IUserManagementService
    {
        Task<DatabaseResponse> Get();
        /// <summary>
        /// Get  User By Id With Relational Entities.
        /// </summary>
        /// <param name="Id"> User Id<</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> GetById(Guid Id);
        /// <summary>
        /// Add  User.
        /// </summary>
        /// <param name="dto">UserAddDTO</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> Add(UserAddDTO dto);
        /// <summary>
        /// Update  User.
        /// </summary>
        /// <param name="dto">Object Of UserUpdateDTO</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> Update(UserUpdateDTO dto);
        /// <summary>
        /// Delete  User.
        /// </summary>
        /// <param name="Id"> User Id<</param>
        /// <returns>Object Of DatabaseResponse</returns>
        Task<DatabaseResponse> Delete(Guid Id);

        Task<DatabaseResponse> UserReport();
    }
}