using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ShahinDataBaseConfigue;
namespace FactoryShahin
{
    public class ShahinFactoryDb : DbContext
    {
        public ShahinFactoryDb()
            : base("Data Source=.; Initial Catalog=ShahinFactory; Integrated Security=true;")
        {
            Database.SetInitializer(new EntitiesContextInitializer());
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<PM, ShahinShoese.Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ConfigueAuction());
            modelBuilder.Configurations.Add(new ConfigueCheck());
            modelBuilder.Configurations.Add(new ConfigueCustomer());
            modelBuilder.Configurations.Add(new ConfigueCustomerType());
            modelBuilder.Configurations.Add(new ConfigueFactor());
            modelBuilder.Configurations.Add(new ConfigueFactorStock());
            modelBuilder.Configurations.Add(new ConfigueFactorType());
            modelBuilder.Configurations.Add(new ConfigueProduct());
            modelBuilder.Configurations.Add(new ConfigueStock());
            modelBuilder.Configurations.Add(new ConfigueSize());
            modelBuilder.Configurations.Add(new ConfigueCustomerStock());
            modelBuilder.Configurations.Add(new ConfigueTavizBarabar());
        }
        public DbSet<Auction>  Auction{ get; set; }
        public DbSet<Check>  Check{ get; set; }
        public DbSet<Customer>  Customer{ get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<Factor> Factor { get; set; }
        public DbSet<FactorStock>  FactorStock{ get; set; }
        public DbSet<FactorType>  FactorType{ get; set; }
        public DbSet<Product>  Product{ get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Size>  Size{ get; set; }
        public DbSet<CustomerStock>  CustomerStock{ get; set; }
        public DbSet<TavizBarabar> TavizBarabar { get; set; }

    }
    public class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<ShahinFactoryDb>
    {
        protected override void Seed(ShahinFactoryDb context)
        {

            base.Seed(context);
        }
    }
}

