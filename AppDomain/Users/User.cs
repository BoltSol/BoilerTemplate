using AppDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Users
{
    public class User : Entity<long>
    {
        private string _encryptedPassword;
        public User()
        {
            CreationDateTime = DateTime.Now;
            IsDeleted = false;
            UserRoles = new List<UserRole>();
        }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
