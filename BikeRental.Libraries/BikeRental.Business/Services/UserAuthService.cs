using BikeRental.Business.Interfaces;
using BikeRental.Common;
using BikeRental.DTO.DTO.Account;
using BikeRental.DTO.Enums;
using BikeRental.DTO.Response;
using BikeRental.Entities;
using BikeRental.Entities.DBEtities;
using BikeRental.Mapper.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BikeRental.Business.Services
{
    public class UserAuthService : IUserAuthService
    {
        #region ----- Properties -----
        private readonly IEfRepository<User> UserRepo;
        private readonly IJWTAuthService JWTService;
        #endregion

        #region ----- Constructor -----

        public UserAuthService(IEfRepository<User> _UserRepo, IJWTAuthService _JWTService)
        {
            UserRepo = _UserRepo;
            JWTService = _JWTService;
        }
        #endregion

        #region ----- Public Methods -----
        public async Task<DatabaseResponse> Login(LoginDTO dto)
        {
            var EncryptedPass = EncryptionManager.Encrypt(dto.Password);

            var exp = new QueryExpression<User>()
            {
                Predicate = i => (EF.Functions.Like(i.Username, dto.UserName.Trim())
                                ||
                                EF.Functions.Like(i.Email, dto.UserName.Trim()))
                                && i.Password == EncryptedPass && !i.IsDeleted,
                AsNoTracking = true
            };

            var user = await UserRepo.GetFirstAsync(exp);
            if (user == null)
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.UNameorPassinCorrect,
                    DbReturnValue = DbReturnValue.UNameorPassinCorrect,
                    HasSucceeded = false
                };
            }

            Claim[] claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var AccessToken = JWTService.GenerateTokens(user.Username, claims, DateTime.Now);

            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.RecordExists,
                DbReturnValue = DbReturnValue.RecordExists,
                HasSucceeded = true,
                Results = new UserMap().GetAuthDTOResponse(user, AccessToken)
            };
        }

        public async Task<DatabaseResponse> SignUp(SignUpDTO dto)
        {
            if (await UserRepo.GetAnyAsync(x => x.Username.Trim().ToLower() == dto.Username.Trim().ToLower() && x.IsDeleted == false))
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.UsernameAlreadyExists,
                    DbReturnValue = DbReturnValue.UsernameAlreadyExists,
                    HasSucceeded = false
                };
            }

            else if (await UserRepo.GetAnyAsync(x => x.Email.Trim().ToLower() == dto.Email.Trim().ToLower() && x.IsDeleted == false))
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.EmailAlreadyExists,
                    DbReturnValue = DbReturnValue.EmailAlreadyExists,
                    HasSucceeded = false
                };
            }

            User obj = new UserMap().SignUp(dto);

            await UserRepo.AddAsync(obj);
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.CreateSuccess,
                DbReturnValue = DbReturnValue.CreateSuccess,
                HasSucceeded = true
            };
        }
        #endregion
    }
}
