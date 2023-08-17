using AppDomain;
using AppDomain.Users;
using AppService.Common;
using AppService.Roles.Dto;
using AppService.Users.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Roles
{
    public interface IRoleAppService
    {
        Task<List<RoleDto>> GetAllRoles();
    }
    public class RoleAppService : IRoleAppService
    {
        private readonly IRepository<Role, long> _roleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IMapper _mapper;
        #region Parameter
        public RoleAppService(IMapper mapper, IRepository<Role, long> roleRepository, IRepository<User, long> userRepository)
        {
            this._mapper = mapper;
            this._roleRepository = roleRepository;
            this._userRepository = userRepository;
        }
        #endregion
        public async Task<List<RoleDto>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAll().ToListAsync();
                var users= await _userRepository.GetAll().Include(x=>x.UserRoles).Where(x=>x.Id==1).FirstOrDefaultAsync();
            users.UserRoles = null;
            users.Password = "user123";
            _userRepository.InsertOrUpdateAndGetId(users);

            var testNestedObj = _mapper.Map<List<UserDto>>(users);
            return _mapper.Map<List<RoleDto>>(roles);
        }
    }
}
