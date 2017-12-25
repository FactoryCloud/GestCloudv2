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

        public DbSet<Condition> Conditions { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<StockAdjust> StockAdjusts { get; set; }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Entity> Entities {get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<ProductTax> ProductTaxes { get; set; }
        public DbSet<ProductTypeTax> ProductTypeTaxes { get; set; }
        public DbSet<MovementTax> MovementTaxes { get; set; }
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<CompanyStore> CompaniesStores { get; set; }

        public DbSet<PurchaseDelivery> PurchaseDeliveries { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<SaleDelivery> SaleDeliveries { get; set; }
        public DbSet<SaleInvoice> SaleInvoices { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }

        public DbSet<ProductTypeTax> ProductTypesTaxes { get; set; }
        public DbSet<ProductTax> ProductsTaxes { get; set; }
        public DbSet<MovementTax> MovementsTaxes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestCloud;Integrated Security=True");
            optionsBuilder.UseSqlServer(@"Data Source=85.214.204.242\SQLEXPRESS,47192;Initial Catalog=GestCloudV1;Persist Security Info=True;User ID=sa;Password=FactoryCloud@2810");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MTGCard>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Product>()
                .HasKey(m => m.ProductID);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

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

            modelBuilder.Entity<Product>()
               .HasOne(a => a.tax)
               .WithMany(b => b.products)
               .HasForeignKey(a => a.TaxID)
               .HasConstraintName("FK_Product_TaxID_Taxes");

            modelBuilder.Entity<ProductType>()
               .HasOne(a => a.tax)
               .WithMany(b => b.productTypes)
               .HasForeignKey(a => a.TaxID)
               .HasConstraintName("FK_ProductTypes_TaxID_Taxes");

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

            modelBuilder.Entity<Movement>()
                .HasOne(a => a.product)
                .WithMany(b => b.Movements)
                .HasForeignKey(a => a.ProductID)
                .HasConstraintName("FK_Movements_ProductID_Products");

            modelBuilder.Entity<Movement>()
                .HasOne(a => a.documentType)
                .WithMany(b => b.Movements)
                .HasForeignKey(a => a.DocumentTypeID)
                .HasConstraintName("FK_Movements_DocumentTypeID_DocumentTypes");

            modelBuilder.Entity<Movement>()
                .HasOne(a => a.condition)
                .WithMany(b => b.Movements)
                .HasForeignKey(a => a.ConditionID)
                .HasConstraintName("FK_Movements_ConditionID_Conditions");

            modelBuilder.Entity<Movement>()
               .HasOne(a => a.tax)
               .WithMany(b => b.movements)
               .HasForeignKey(a => a.TaxID)
               .HasConstraintName("FK_Movements_TaxID_Taxes");

            modelBuilder.Entity<Movement>()
                .HasOne(a => a.store)
                .WithMany(b => b.Movements)
                .HasForeignKey(a => a.StoreID)
                .HasConstraintName("FK_Movements_StoreID_Stores");

            modelBuilder.Entity<CompanyStore>()
                .HasOne(a => a.company)
                .WithMany(b => b.CompaniesStores)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_CompaniesStores_CompanyID_Companies");

            modelBuilder.Entity<CompanyStore>()
                .HasOne(a => a.store)
                .WithMany(b => b.CompaniesStores)
                .HasForeignKey(a => a.StoreID)
                .HasConstraintName("FK_CompaniesStores_StoreID_Stores");

            modelBuilder.Entity<StockAdjust>()
                .HasOne(a => a.company)
                .WithMany(b => b.StockAdjusts)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_StockAdjusts_CompanyID_Companies");

            modelBuilder.Entity<PurchaseDelivery>()
                .HasOne(a => a.company)
                .WithMany(b => b.PurchaseDeliveries)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_PurchaseDeliveries_CompanyID_Companies");

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(a => a.company)
                .WithMany(b => b.PurchaseInvoices)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_PurchaseInvoices_CompanyID_Companies");

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(a => a.company)
                .WithMany(b => b.PurchaseOrders)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_PurchaseOrders_CompanyID_Companies");

            modelBuilder.Entity<SaleDelivery>()
                .HasOne(a => a.company)
                .WithMany(b => b.SaleDeliveries)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_SaleDeliveries_CompanyID_Companies");

            modelBuilder.Entity<SaleInvoice>()
                .HasOne(a => a.company)
                .WithMany(b => b.SaleInvoices)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_SaleInvoices_CompanyID_Companies");

            modelBuilder.Entity<SaleOrder>()
                .HasOne(a => a.company)
                .WithMany(b => b.SaleOrders)
                .HasForeignKey(a => a.CompanyID)
                .HasConstraintName("FK_SaleOrders_CompanyID_Companies");

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(a => a.provider)
                .WithMany(b => b.PurchaseOrders)
                .HasForeignKey(a => a.ProviderID)
                .HasConstraintName("FK_PurchaseOrders_ProviderID_Providers");

            modelBuilder.Entity<SaleOrder>()
                .HasOne(a => a.client)
                .WithMany(b => b.SaleOrders)
                .HasForeignKey(a => a.ClientID)
                .HasConstraintName("FK_SaleOrders_ClientID_Clients");

            modelBuilder.Entity<User>()
                .HasOne(a => a.userType)
                .WithMany(b => b.users)
                .HasForeignKey(a => a.UserTypeID)
                .HasConstraintName("FK_Users_UserTypeID_UserTypes");

            modelBuilder.Entity<Entity>()
                .HasOne(a => a.client)
                .WithOne(b => b.entity)
                .HasForeignKey<Client>(c => c.EntityID);

            modelBuilder.Entity<Entity>()
                .HasOne(a => a.user)
                .WithOne(b => b.entity)
                .HasForeignKey<User>(c => c.EntityID);

            modelBuilder.Entity<Entity>()
               .HasOne(a => a.provider)
               .WithOne(b => b.entity)
               .HasForeignKey<Provider>(c => c.EntityID);

            modelBuilder.Entity<Entity>()
                .HasOne(a => a.entityType)
                .WithMany(b => b.entities)
                .HasForeignKey(a => a.EntityTypeID)
                .HasConstraintName("FK_Entity_EntityTypeID_EntityTypes");

            modelBuilder.Entity<Entity>()
                .HasOne(a => a.country)
                .WithMany(b => b.entities)
                .HasForeignKey(a => a.CountryID)
                .HasConstraintName("FK_Entity_CountryID_Countries");

            modelBuilder.Entity<Entity>()
                .HasOne(a => a.city)
                .WithMany(b => b.entities)
                .HasForeignKey(a => a.CityID)
                .HasConstraintName("FK_Entity_CityID_Cities");

            modelBuilder.Entity<Tax>()
               .HasOne(a => a.taxType)
               .WithMany(b => b.taxes)
               .HasForeignKey(a => a.TaxTypeID)
               .HasConstraintName("FK_Taxes_TaxTypeID_TaxTypes");

            modelBuilder.Entity<ProductTypeTax>()
               .HasOne(a => a.productType)
               .WithMany(b => b.productTypesTaxes)
               .HasForeignKey(a => a.ProductTypeID)
               .HasConstraintName("FK_ProductTypesTaxes_TaxTypeID_TaxTypes");

            modelBuilder.Entity<ProductTypeTax>()
               .HasOne(a => a.tax)
               .WithMany(b => b.productTypesTaxes)
               .HasForeignKey(a => a.TaxID)
               .HasConstraintName("FK_ProductTypesTaxes_ProductTypeID_ProductTypes");

            modelBuilder.Entity<ProductTax>()
               .HasOne(a => a.product)
               .WithMany(b => b.productsTaxes)
               .HasForeignKey(a => a.ProductTaxID)
               .HasConstraintName("FK_ProductsTaxes_TaxTypeID_TaxTypes");

            modelBuilder.Entity<ProductTax>()
               .HasOne(a => a.tax)
               .WithMany(b => b.productsTaxes)
               .HasForeignKey(a => a.TaxID)
               .HasConstraintName("FK_ProductsTaxes_ProductID_Products");

            modelBuilder.Entity<MovementTax>()
               .HasOne(a => a.movement)
               .WithMany(b => b.movementsTaxes)
               .HasForeignKey(a => a.MovementTaxID)
               .HasConstraintName("FK_MovementsTaxes_TaxID_Taxes");

            modelBuilder.Entity<MovementTax>()
               .HasOne(a => a.tax)
               .WithMany(b => b.movementsTaxes)
               .HasForeignKey(a => a.TaxID)
               .HasConstraintName("FK_MovementsTaxes_MovementID_Movements");

            modelBuilder.Entity<TaxType>()
               .HasOne(a => a.company)
               .WithMany(b => b.TaxTypes)
               .HasForeignKey(a => a.CompanyID)
               .HasConstraintName("FK_TaxTypes_CompanyID_Companies");

            modelBuilder.Entity<FiscalYear>()
               .HasOne(a => a.company)
               .WithMany(b => b.FiscalYears)
               .HasForeignKey(a => a.CompanyID)
               .HasConstraintName("FK_FiscalYears_CompanyID_Companies");
        }

        public void UpdateFromMKM()
        {
            RequestHelper req = new RequestHelper();
            req.expansionsMakeRequest();
            req.singlesMakeRequest();
        }

        public void UpdatePricesMKM(List<Product> products)
        {
            RequestHelper req = new RequestHelper();
            req.productsMakeRequest(products);
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
            List<MTGCard> cards = MTGCards.Include(c => c.expansion).ToList();
            foreach (MTGCard card in cards)
            {
                //List<Product> temp = Products.Where(p => card.ProductID == p.ExternalID).Include(p => p.productType).ToList();
                /*if (temp.Count == 0)
                {*/
                    // Accion para cartas que aun no están en la base de Productos
                    Products.Add(new Product
                    {
                        Name = $"{card.EnName} ({card.expansion.Abbreviation})",
                        ProductTypeID = ProductTypes.First(t => t.Name == "MTGCard").ProductTypeID,
                        ExternalID = card.ProductID,
                        DateLaunch = card.expansion.ReleaseDate
                    });
                /*}

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
                }*/
            }
            this.SaveChanges();
        }
    }

    
}
