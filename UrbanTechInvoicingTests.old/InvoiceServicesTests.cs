using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using UrbanTechInvoicing.Services;
using UrbanTechInvoicing.Models;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Repositories;

namespace UrbanTechInvoicingTests
{
    public class InvoiceServicesTests
    {
        [Fact]
        public async Task TestGetInvoicesAsync_ReturnsInvoices()
        {
            // Arrange  
            var mockInvoiceRepository = new Mock<IInvoiceRepository>();
            mockInvoiceRepository.Setup(repo => repo.GetAllInvoicesAsync())
                .ReturnsAsync(new List<Invoice>
                {
                   new Invoice { DueDate = DateTime.Now.AddDays(30), InvoiceDate = DateTime.Now, InvoiceId = Guid.NewGuid(), InvoiceNumber = "INV001", InvoiceTotal = 100, Status = Invoice.InvoiceStatus.Unpaid },
                   new Invoice { DueDate = DateTime.Now.AddDays(30), InvoiceDate = DateTime.Now, InvoiceId = Guid.NewGuid(), InvoiceNumber = "INV002", InvoiceTotal = 200, Status = Invoice.InvoiceStatus.Unpaid },
                   new Invoice { DueDate = DateTime.Now.AddDays(30), InvoiceDate = DateTime.Now, InvoiceId = Guid.NewGuid(), InvoiceNumber = "INV003", InvoiceTotal = 300, Status = Invoice.InvoiceStatus.Unpaid },
                   new Invoice { DueDate = DateTime.Now.AddDays(30), InvoiceDate = DateTime.Now, InvoiceId = Guid.NewGuid(), InvoiceNumber = "INV004", InvoiceTotal = 400, Status = Invoice.InvoiceStatus.Unpaid },
                   new Invoice { DueDate = DateTime.Now.AddDays(30), InvoiceDate = DateTime.Now, InvoiceId = Guid.NewGuid(), InvoiceNumber = "INV005", InvoiceTotal = 500, Status = Invoice.InvoiceStatus.Unpaid }

                });

            var invoiceService = new InvoiceServices(mockInvoiceRepository.Object);
            var expectedCount = 5; // Assuming we expect 5 invoices  

            // Act  
            var invoices = await invoiceService.GetAllInvoicesAsync();

            // Assert  
            Assert.NotNull(invoices);
            Assert.Equal(expectedCount, invoices.Count());
        }
    }
}
