using AppDomain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
           .HasOne(o => o.User)
           .WithMany(s => s.UserRoles)
           .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<UserRole>()
           .HasOne(o => o.Role)
           .WithMany(s => s.UserRoles)
           .HasForeignKey(o => o.RoleId);

            base.OnModelCreating(modelBuilder);
            SeedData.Seed(modelBuilder);
        }
    }
}
