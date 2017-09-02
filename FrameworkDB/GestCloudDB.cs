using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;


namespace FrameworkDB.V1
{
    public class GestCloudDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AccessType> AccessTypes { get; set; }
        public DbSet<UserAccessControl> UsersAccessControl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestCloud;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccessControl>()
                .HasKey(a => a.UserAccessControlID);

            modelBuilder.Entity<UserAccessControl>()
                .HasOne(a => a.user)
                .WithMany(b => b.UsersAccessControl)
                .HasForeignKey(a => a.UserID)
                .HasConstraintName("FK_UserID_Users");

            modelBuilder.Entity<UserAccessControl>()
                .HasOne(a => a.accessType)
                .WithMany(b => b.UsersAccessControl)
                .HasForeignKey(a => a.AccessTypeID)
                .HasConstraintName("FK_AcessTypeID_AcessTypes");
        }
    }
}
