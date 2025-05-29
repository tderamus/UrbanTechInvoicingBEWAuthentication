using Microsoft.EntityFrameworkCore;
using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Data
{
    public class UrbanTechDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoicePayments> InvoicePayments { get; set; }
        public DbSet<InvoiceService> InvoiceServices { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Models.Service> Services { get; set; }

        public UrbanTechDbContext(DbContextOptions<UrbanTechDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys to be auto-generated
            modelBuilder.Entity<Invoice>()
                .Property(i => i.InvoiceId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Status)
                .HasConversion<string>();

            //modelBuilder.Entity<Invoice>()
            //    .HasOne(i => i.Product)
            //    .WithMany()
            //    .HasForeignKey(i => i.ProductId);

            //modelBuilder.Entity<Invoice>()
            //    .HasOne(i => i.Service)
            //    .WithMany()
            //    .HasForeignKey(i => i.ServiceId);


            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Payments>()
                .Property(p => p.PaymentId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Payments>()
                .Property(p => p.PaymentType)
                .HasConversion<string>();


            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Models.Service>()
                .Property(s => s.ServiceId)
                .ValueGeneratedOnAdd();


            // Configure the composite key for InvoicePayments
            modelBuilder.Entity<InvoicePayments>()
                .HasKey(ip => new { ip.InvoiceId, ip.PaymentId });
            // Configure the composite key for InvoiceService
            modelBuilder.Entity<InvoiceService>()
                .HasKey(ins => new { ins.InvoiceId, ins.ServiceId });
            // Configure the composite key for InvoiceProduct
            modelBuilder.Entity<InvoiceProduct>()
                .HasKey(ip => new { ip.InvoiceId, ip.ProductId });

            // Configure the many to many relationships
            modelBuilder.Entity<InvoicePayments>()
                .HasOne(ip => ip.Invoice)
                .WithMany(i => i.InvoicePayments)
                .HasForeignKey(ip => ip.InvoiceId);

            modelBuilder.Entity<InvoiceProduct>()
                .HasOne(ip => ip.Invoice)
                .WithMany(i => i.InvoiceProducts)
                .HasForeignKey(ip => ip.InvoiceId);

            modelBuilder.Entity<InvoiceService>()
                .HasOne(ins => ins.Invoice)
                .WithMany(i => i.InvoiceServices)
                .HasForeignKey(ins => ins.InvoiceId);


            // Add seed data for testing
            var seedInvoiceId = Guid.NewGuid();
            var seedCustomerId = Guid.NewGuid();
            var seedProductId = Guid.NewGuid();
            var seedServiceId = Guid.NewGuid();
            var seedPaymentId = Guid.NewGuid();

            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    InvoiceId = seedInvoiceId,
                    InvoiceNumber = "INV001",
                    InvoiceDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(30),
                    InvoiceTotal = 1000.00m,
                    Status = Invoice.InvoiceStatus.Unpaid,
                    CustomerId = seedCustomerId
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = seedCustomerId,
                    Name = "Robots Inc",
                    EmailAddress = "customer1@email.com",
                    PhoneNumber = "1234567890"
                });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = seedProductId,
                    ProductName = "Robot Cleaner",
                    Description = "A robot that cleans your house."
                });

            modelBuilder.Entity<Service>().HasData(
                new Models.Service
                {
                    ServiceId = seedServiceId,
                    ServiceName = "Cleaning Service",
                    Description = "A service that cleans your house."
                });

            modelBuilder.Entity<Payments>().HasData(
                new Payments
                {
                    PaymentId = seedPaymentId,
                    InvoiceId = seedInvoiceId,
                    PaymentDate = DateTime.UtcNow,
                    PaymentAmount = 1000.00m,
                    PaymentType = Models.Payments.PmtType.CreditCard,
                });

            // 🔗 Link the payment to the invoice via join table
            modelBuilder.Entity<InvoicePayments>().HasData(
                new InvoicePayments
                {
                    InvoiceId = seedInvoiceId,
                    PaymentId = seedPaymentId,
                    PaymentDate = DateTime.UtcNow,
                    PaymentAmount = 1000.00m
                });
        }
    }
}
