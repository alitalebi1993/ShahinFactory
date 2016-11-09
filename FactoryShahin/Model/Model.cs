using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace FactoryShahin
{


    public class Product
    {
        public Product()
        {
            
        }
        public virtual int ProductID { get; set; }
        public virtual string ProductDescription { get; set; }
        public virtual DateTime DateRegister { get; set; }
        public virtual int BeforeCount { get; set; }
        public virtual long UnitPrice { get; set; }
        public virtual long SalePrice { get; set; }
        public virtual long FactoryPrice { get; set; }
        public virtual int MaxCount { get; set; }
        public virtual int MinCount { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
        public virtual int ProductUnitID { get; set; }
        public virtual ProductUnit ProductUnit { get; set; }
        public virtual int ProductTypeID { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual int StoreID { get; set; }
        public virtual Store Store { get; set; }
    }
    public class ProductUnit
    {
        public ProductUnit()
        {
        }
        public virtual int ProductUnitID { get; set; }
        [DisplayName("نام واحد محصول")]
        public virtual string ProductUnitName { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public override string ToString()
        {
            return ProductUnitName;
        }
    }
    public class ProductType
    {
        public ProductType()
        {
        }
        public virtual int ProductTypeID { get; set; }
        [DisplayName("نام نوع محصول")]
        public virtual string ProductTypeName { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public override string ToString()
        {
            return ProductTypeName;
        }
    }
    public class Store
    {
        public Store()
        {
        }
        public virtual int StoreID { get; set; }
        [DisplayName("نام انبار")]
        public virtual string StoreName { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public override string ToString()
        {
            return StoreName;
        }
    }
    public class Stock
    {
        public Stock()
        {
        }
        public virtual int StockID { get; set; }
        public virtual long SalePrice { get; set; }
        public virtual long UnitPrice { get; set; }
        public virtual int Count { get; set; }
        public virtual string Description { get; set; }
        public virtual int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public virtual int SizeID { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<FactorStock> FactorStock { get; set; }
        public virtual ICollection<CustomerStock> CustomerStock { get; set; }
    }
    public class Size
    {
        public Size()
        {
        }
        public virtual int SizeID { get; set; }
        [DisplayName("نام سایز")]
        public virtual int SizeNumber { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
        public override string ToString()
        {
            return SizeNumber.ToString();
        }
    }
    public class CustomerStock
    {
        public CustomerStock()
        {
        }
        public virtual int CustomerStockID { get; set; }
        public virtual int StockID { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Auction Auction { get; set; }
        public virtual ICollection<TavizBarabar> Product1 { get; set; }
        public virtual ICollection<TavizBarabar> Product2 { get; set; }
    }
    public class TavizBarabar
    {
        public TavizBarabar()
        {
        }
        public virtual int TavizBarabarID { get; set; }
        public virtual int CustomerStockID1 { get; set; }
        public virtual CustomerStock CustomerStock1 { get; set; }
        public virtual int CustomerStockID2 { get; set; }
        public virtual CustomerStock CustomerStock2 { get; set; }
    }
    public class Auction
    {
        public Auction()
        {
        }
        public virtual int AuctionID { get; set; }
    }
    public class Customer
    {
        public Customer()
        {
        }
        public virtual int CustomerID { get; set; }
        [DisplayName("نام مشتری")]
        public virtual string CustomerName { get; set; }
        [DisplayName("ادرس")]
        public virtual string Address { get; set; }
        [DisplayName("BALANCE")]
        public virtual long Balance { get; set; }
        public virtual int CustomerTypeID { get; set; }
        [DisplayName("نوع مشتری")]
        public virtual CustomerType CustomerType { get; set; }
        public virtual ICollection<Factor> Factor { get; set; }
        public virtual ICollection<CustomerStock> CustomerStock { get; set; }
    }
    public class CustomerType
    {
        public CustomerType()
        {
        }
        public virtual int CustomerTypeID { get; set; }
        [DisplayName("نام نوع مشتری")]
        public virtual string CustomerTypeName { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
    public class Factor
    {
        public Factor()
        {
        }
        public virtual int FactorID { get; set; }
        public virtual int CustomerFactorNumber { get; set; }
        public virtual int CustomerID { get; set; }
        public virtual long TotalPrice { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual int FactorTypeID { get; set; }
        public virtual FactorType FactorType { get; set; }
        public virtual ICollection<Check> Check { get; set; }
        public virtual ICollection<FactorStock> FactorStock { get; set; }
    }
    public class FactorType
    {
        public FactorType()
        {
        }
        public virtual int FactorTypeID { get; set; }
        public virtual string FactorTypeName { get; set; }
        public virtual ICollection<Factor> Factor { get; set; }
    }
    public class FactorStock
    {
        public FactorStock()
        {
        }
        public virtual int FactorStockID { get; set; }
        public virtual long StockPriceInDay { get; set; }
        public virtual int Count { get; set; }
        public virtual int StockID { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual int FactorID { get; set; }
        public virtual Factor Factor { get; set; }
    }
    public class Check
    {
        public Check()
        {
        }
        public virtual int CheckID { get; set; }
        public virtual int CheckNumber { get; set; }
        public virtual string Description { get; set; }
        public virtual long Price { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int FactorID { get; set; }
        public virtual Factor Factor { get; set; }
    }
}