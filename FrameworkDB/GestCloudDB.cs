using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Windows;

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

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestCloud;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MTGCard>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Product>()
                .HasKey(m => m.ProductID);

            modelBuilder.Entity<MTGCard>()
                .HasOne(a => a.expansion)
                .WithMany(b => b.MTGCards)
                .HasForeignKey(a => a.ExpansionID)
                .HasConstraintName("FK_MTGCards_ExpansionID_Expansions");

            modelBuilder.Entity<Product>()
                .HasOne(a => a.productType)
                .WithMany(b => b.Products)
                .HasForeignKey(a => a.ProductTypeID)
                .HasConstraintName("FK_Products_ProductTypeID_ProductTypes");

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

        public void UpdateFromMKM()
        {
            RequestHelper req = new RequestHelper();
            req.expansionsMakeRequest();
            req.singlesMakeRequest();
        }

        public void UpdateProductsList()
        {
            /*foreach(Product p in Products)
            {
                if(p.productType.Name == "MTGCard")
                {
                    List<MTGCard> temp = MTGCards.Where(c => c.ProductID == p.ExternalID).ToList();
                    if (temp.Count == 0)
                    {
                        // Accion para cartas que ya no están en la base de MKM
                    }
                }
            }*/
            List<MTGCard> cards = MTGCards.Include(c => c.expansion).OrderBy(c=> c.expansion.ExpansionID).ToList();
            foreach (MTGCard card in cards)
            {
                List<Product> temp = Products.Where(p => card.ProductID == p.ExternalID).Include(p => p.productType).ToList();
                if (temp.Count == 0)
                {
                    // Accion para cartas que aun no están en la base de Productos
                    /*Products.Add(new Product
                    {
                        Name = $"{card.EnName} ({card.expansion.Abbreviation})",
                        ProductTypeID = ProductTypes.First(t => t.Name == "MTGCard").ProductTypeID,
                        ExternalID = card.ProductID,
                        DateLaunch = card.expansion.ReleaseDate
                    });*/
                }

                else
                {
                    // Accion para cartas que ya están en la base de Productos
                    if (temp[0].Name != $"{card.EnName} ({card.expansion.Abbreviation})" || 
                        temp[0].DateLaunch != card.expansion.ReleaseDate)
                    {
                        temp[0].Name = $"{card.EnName} ({card.expansion.Abbreviation})";
                        temp[0].DateLaunch = card.expansion.ReleaseDate;
                        Products.Update(temp[0]);
                    }
                }
            }
            this.SaveChanges();
        }
    }

    
}
