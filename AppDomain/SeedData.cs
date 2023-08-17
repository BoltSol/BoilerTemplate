using AppDomain.Common;
using AppDomain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "User" });
            modelBuilder.Entity<User>().HasData(new User { Id = 1, EmailAddress = "admin@gmail.com", Password = EncryptionHelper.Encrypt("admin123") }, new User { Id = 2, EmailAddress = "user@gmail.com", Password = EncryptionHelper.Encrypt("user123") });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = 1, RoleId = 1, UserId = 1 }, new UserRole { Id = 2, RoleId = 2, UserId = 1 }, new UserRole { Id = 3, RoleId = 2, UserId = 2 });
        }

    }
}
