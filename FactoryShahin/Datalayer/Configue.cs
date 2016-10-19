using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using ParkingDatabase;
using ShahinShoese;
namespace ShahinDataBaseConfigue
{
    public class ConfigueProduct : EntityTypeConfiguration<Product>
    {
        public ConfigueProduct()
        {
            HasKey(x => x.ProductID);
            Property(x => x.ProductDescription).HasMaxLength(50);
            HasMany(x => x.Stock).WithRequired(x => x.Product);
        }
    }
    public class ConfigueProductUnit : EntityTypeConfiguration<ProductUnit>
    {
        public ConfigueProductUnit()
        {
            HasKey(x => x.ProductUnitID);
            Property(x => x.ProductUnitName).HasMaxLength(50);
        }
    }
    public class ConfigueProductType : EntityTypeConfiguration<ProductType>
    {
        public ConfigueProductType()
        {
            HasKey(x => x.ProductTypeID);
            Property(x => x.ProductTypeName).HasMaxLength(50);
        }
    }
    public class ConfigueStore : EntityTypeConfiguration<Store>
    {
        public ConfigueStore()
        {
            HasKey(x => x.StoreID);
            Property(x => x.StoreName).HasMaxLength(50);
            HasMany(x => x.Product).WithRequired(x => x.Store);
        }
    }
    public class ConfigueStock : EntityTypeConfiguration<Stock>
    {
        public ConfigueStock()
        {
            HasKey(x => x.StockID);
            Property(x => x.Description).HasMaxLength(50);
            HasMany(x => x.FactorStock).WithRequired(x => x.Stock);
            HasMany(x => x.CustomerStock).WithRequired(x => x.Stock);
        }
    }
    public class ConfigueSize : EntityTypeConfiguration<Size>
    {
        public ConfigueSize()
        {
            HasKey(x => x.SizeID);
            HasMany(x => x.Stock).WithRequired(x => x.Size);
        }
    }
    public class ConfigueCustomerStock : EntityTypeConfiguration<CustomerStock>
    {
        public ConfigueCustomerStock()
        {
            HasKey(x => x.CustomerStockID);
            HasMany(x => x.Product1).WithRequired(x => x.CustomerStock1).HasForeignKey(x => x.CustomerStockID1).WillCascadeOnDelete(false);
            HasMany(x => x.Product2).WithRequired(x => x.CustomerStock2).HasForeignKey(x => x.CustomerStockID2).WillCascadeOnDelete(false);
        }
    }
    public class ConfigueTavizBarabar : EntityTypeConfiguration<TavizBarabar>
    {
        public ConfigueTavizBarabar()
        {
            HasKey(x => x.TavizBarabarID);
        }
    }
    public class ConfigueAuction : EntityTypeConfiguration<Auction>
    {
        public ConfigueAuction()
        {
            HasKey(x => x.AuctionID);
        }
    }
    public class ConfigueCustomer : EntityTypeConfiguration<Customer>
    {
        public ConfigueCustomer()
        {
            HasKey(x => x.CustomerID);
            Property(x => x.CustomerName).HasMaxLength(50);
            Property(x => x.Address).HasMaxLength(50);
            HasMany(x => x.Factor).WithRequired(x => x.Customer);
            HasMany(x => x.CustomerStock).WithRequired(x => x.Customer);
        }
    }
    public class ConfigueCustomerType : EntityTypeConfiguration<CustomerType>
    {
        public ConfigueCustomerType()
        {
            HasKey(x => x.CustomerTypeID);
            Property(x => x.CustomerTypeName).HasMaxLength(50);
            HasMany(x => x.Customer).WithRequired(x => x.CustomerType);
        }
    }
    public class ConfigueFactor : EntityTypeConfiguration<Factor>
    {
        public ConfigueFactor()
        {
            HasKey(x => x.FactorID);
            HasMany(x => x.Check).WithRequired(x => x.Factor);
            HasMany(x => x.FactorStock).WithRequired(x => x.Factor);
        }
    }
    public class ConfigueFactorType : EntityTypeConfiguration<FactorType>
    {
        public ConfigueFactorType()
        {
            HasKey(x => x.FactorTypeID);
            Property(x => x.FactorTypeName).HasMaxLength(50);
            HasMany(x => x.Factor).WithRequired(x => x.FactorType);
        }
    }
    public class ConfigueFactorStock : EntityTypeConfiguration<FactorStock>
    {
        public ConfigueFactorStock()
        {
            HasKey(x => x.FactorStockID);
        }
    }
    public class ConfigueCheck : EntityTypeConfiguration<Check>
    {
        public ConfigueCheck()
        {
            HasKey(x => x.CheckID);
        }
    }
}