using AppDomain.Users;
using AppService.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
