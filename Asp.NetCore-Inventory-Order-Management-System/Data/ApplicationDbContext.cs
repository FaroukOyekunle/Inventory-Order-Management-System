using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
﻿using Asp.NetCore_Inventory_Order_Management_System.Models;

namespace Asp.NetCore_Inventory_Order_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Bill> Bill { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.BillType> BillType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Branch> Branch { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.CashBank> CashBank { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Currency> Currency { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Customer> Customer { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.CustomerType> CustomerType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.GoodsReceivedNote> GoodsReceivedNote { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Invoice> Invoice { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.InvoiceType> InvoiceType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.NumberSequence> NumberSequence { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.PaymentReceive> PaymentReceive { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.PaymentType> PaymentType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.PaymentVoucher> PaymentVoucher { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Product> Product { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.ProductType> ProductType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.PurchaseOrderLine> PurchaseOrderLine { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.PurchaseType> PurchaseType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.SalesOrder> SalesOrder { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.SalesOrderLine> SalesOrderLine { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.SalesType> SalesType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Shipment> Shipment { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.ShipmentType> ShipmentType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.UnitOfMeasure> UnitOfMeasure { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Vendor> Vendor { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.VendorType> VendorType { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.Warehouse> Warehouse { get; set; }

        public DbSet<Asp.NetCore_Inventory_Order_Management_System.Models.UserProfile> UserProfile { get; set; }
    }
}
