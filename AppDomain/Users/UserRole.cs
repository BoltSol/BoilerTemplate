using AppDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Users
{
    public class UserRole : Entity<long>
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
