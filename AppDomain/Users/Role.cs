using AppDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Users
{
    public class Role:Entity<long>
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
