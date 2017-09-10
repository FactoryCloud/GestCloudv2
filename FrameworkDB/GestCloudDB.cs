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

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<Expansion> Expansions { get; set; }
        public DbSet<MTGCard> MTGCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestCloud;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MTGCard>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<MTGCard>()
                .HasOne(a => a.expansion)
                .WithMany(b => b.MTGCards)
                .HasForeignKey(a => a.ExpansionID)
                .HasConstraintName("FK_MTGCards_ExpansionID_Expansions");

            modelBuilder.Entity<UserAccessControl>()
                .HasKey(a => new { a.UserAccessControlID });

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

            modelBuilder.Entity<UserPermission>()
                .HasKey(a => a.UserPermissionID);

            modelBuilder.Entity<UserPermission>()
                .HasOne(a => a.user)
                .WithMany(b => b.UserPermissions)
                .HasForeignKey(a => a.UserID)
                .HasConstraintName("FK_UserPermissions_UserID_Users");

            modelBuilder.Entity<UserPermission>()
                .HasOne(a => a.userType)
                .WithMany(b => b.UserPermissions)
                .HasForeignKey(a => a.UserTypeID)
                .HasConstraintName("FK_UserPermissions_UserTypeID_UserTypes");

            modelBuilder.Entity<UserPermission>()
                .HasOne(a => a.permissionType)
                .WithMany(b => b.UserPermissions)
                .HasForeignKey(a => a.PermissionTypeID)
                .HasConstraintName("FK_UserPermissions_permissionTypeID_permissionTypes");
        }
    }

    
}
