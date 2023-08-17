using AppDomain.Users;
using AppService.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Users.Dto
{
    [AutoMap(typeof(User))]
    public class UserDto
    {
        public UserDto()
        {
            UserRoles = new List<UserRoleDto>();
        }
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDateTime { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
