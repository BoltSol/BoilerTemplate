using AppDomain.Users;
using AppDomain.Common;
using AppService.Users.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomain;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AppService.Common;

namespace AppService.Users
{
    public interface IUserLoggedinAppService
    {
        Task<UserLoggedinOutputDto> UserAuthenticate(UserLoggedinInputDto input);
    }
    public class UserLoggedinAppService : IUserLoggedinAppService
    {
        private readonly IRepository<User, long> _userRepository;
        #region Parameter
        public UserLoggedinAppService(IRepository<User, long> userRepository)
        {
            this._userRepository = userRepository;
        }
        #endregion

        public async Task<UserLoggedinOutputDto> UserAuthenticate(UserLoggedinInputDto input)
        {
            if (!string.IsNullOrEmpty(input.EmailAddress) && string.IsNullOrEmpty(input.Password)) return null;

            var output = new UserLoggedinOutputDto();
            var user = await _userRepository.GetAll().Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.EmailAddress == input.EmailAddress && x.Password == input.EncryptedPassword);
            if (user is null) return null;

            user.Password = null;
            output.Claims = new List<Claim> { new Claim(ClaimTypes.Email, user.EmailAddress), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

            foreach (var uRole in user.UserRoles)
            {
                uRole.User = null;
                uRole.Role.UserRoles = null;
                output.Claims.Add(new Claim(ClaimTypes.Role, uRole.Role.Name));
            }

            output.User = user.MapTo<UserDto>();
            return output;
        }
    }
    public class UserLoggedinInputDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
        public string EncryptedPassword
        {
            get
            {
                return EncryptionHelper.Encrypt(Password);
            }
        }
    }
    public class UserLoggedinOutputDto
    {
        public UserDto User { get; set; }
        public List<Claim> Claims { get; set; }
}

}
