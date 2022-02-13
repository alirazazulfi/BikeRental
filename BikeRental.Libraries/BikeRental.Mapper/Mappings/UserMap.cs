using BikeRental.Common;
using BikeRental.DTO.DTO;
using BikeRental.DTO.DTO.Account;
using BikeRental.DTO.DTO.Report;
using BikeRental.Entities.DBEtities;

namespace BikeRental.Mapper.Mappings
{
    public class UserMap
    {
        #region --- User Login /SignUp Maping ---

        public AuthResponseDTO GetAuthDTOResponse(User entity, string AccessToken) => new()
        {
            Name = entity.FullName,
            UserName = entity.Username,
            Email = entity.Email,
            Mobile = entity.Mobile,
            UserRoleId = entity.UserRole,
            UserRole = entity.UserRole == 1 ? "Manager" : entity.UserRole == 2 ? "User" : "",
            JWToken = AccessToken
        };


        public User SignUp(SignUpDTO dto) => new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username,
            Password = EncryptionManager.Encrypt(dto.Password),
            Email = dto.Email.ToLower(),
            Mobile = dto.Mobile,
            UserRole = 2//--User
        };
        #endregion

        #region --- Manage User Maping ---

        public UserDTO GetDTOResponse(User entity) => new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            FullName = entity.FullName,
            Username = entity.Username,
            Email = entity.Email,
            Mobile = entity.Mobile,
            UserRoleId = entity.UserRole,
            UserRole = entity.UserRole == 1 ? "Manager" : entity.UserRole == 2 ? "User" : ""
        };

        public User Add(UserAddDTO dto) => new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username,
            Password = EncryptionManager.Encrypt(dto.Password),
            Email = dto.Email.ToLower(),
            Mobile = dto.Mobile,
            UserRole = dto.UserRole,
            CreatedDate = DateTime.UtcNow
        };

        public User Update(User entity, UserUpdateDTO dto)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Username = dto.Username;
            entity.Mobile = dto.Mobile;
            entity.Email = dto.Email.ToLower();
            entity.UserRole = dto.UserRole;
            entity.ModifiedDate = DateTime.UtcNow;
            return entity;
        }

        #endregion

        #region --- Report Maping ---

        public UserReportDTO GetUserReportResponse(User entity)
        {
            UserReportDTO dto = new();
            dto.User = new UserRDTO()
            {
                FullName = entity.FullName,
                Email = entity.Email,
                Mobile = entity.Mobile
            };
            foreach (var item in entity.Reservations)
            {
                dto.BikeReservations.Add(new BikeReservationDTO()
                {
                    Bike = new BikeRDTO()
                    {
                        Name = item.Bike.Name,
                        Model = item.Bike.Model,
                        Color = item.Bike.Color,
                        Location = item.Bike.Location,
                        AverageRating = item.Bike.AverageRating,
                        Available = item.Bike.IsAvailable,
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

        #endregion
    }
}
