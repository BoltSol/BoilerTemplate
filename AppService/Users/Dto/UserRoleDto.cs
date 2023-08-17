using AppDomain.Users;
using AppService.Common;
using AppService.Roles.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Users.Dto
{
    [AutoMap(typeof(UserRole))]
    public class UserRoleDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
