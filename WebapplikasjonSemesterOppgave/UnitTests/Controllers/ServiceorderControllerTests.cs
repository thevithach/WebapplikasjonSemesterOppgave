using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Controllers;
using WebapplikasjonSemesterOppgave.Data;
using WebapplikasjonSemesterOppgave.Models;
using Xunit;

public class ServiceorderControllerUnitTests
{
    [Fact]
    public async Task Index_ReturnsViewResultWithOrders()
    {
        // Arrange
        var orders = new List<OrderEntity>
        {
            // Create sample orders here
        };

        var dbContextOptions = new DbContextOptionsBuilder<DBContextSample>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        var dbContext = new DBContextSample(dbContextOptions);
        dbContext.Database.EnsureCreated();

        // Populate the in-memory database with sample orders
        dbContext.OrderEntity.AddRange(orders);
        dbContext.SaveChanges();

        var controller = new ServiceorderController(dbContext);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<OrderEntity>>(viewResult.Model);
        Assert.NotNull(model);

        // You can further assert the model contents or other aspects of the action result as needed
    }

    [Fact]
    public async Task Create_Post_ValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<DBContextSample>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        var dbContext = new DBContextSample(dbContextOptions);

        var controller = new ServiceorderController(dbContext);

        var order = new OrderEntity
        {
            ProductType = "Sample Product Type",
            SerialNumber = "Sample Serial Number",
            ModelYear = "2023",
            Warranty = true,
            ServiceOrRepair = true,
            CustomerAgreement = "Sample Customer Agreement",
            ReparationDetails = "Sample Reparation Details",
            WorkingHours = "5",
            ReplacedPartsReturned = "Sample Replaced Parts",
            ShippingMethods = "Sample Shipping Methods",
            OrderCreatedDate = DateTime.Now,
            OrderPlacerCustomer = "Sample Customer",
            ProductReceivedDate = DateTime.Now,
            ProductAgreedCompletionDate = DateTime.Now.AddDays(7),
            UserId = "610e4677-e391-4cf4-a6fa-a6280f62d79a", // You should replace this with a valid user ID.
            
            // OrderStatus property is calculated and not set here.
        };


        // Act
        var result = await controller.Create(order) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action
    }

    // Add more test cases for other controller actions as needed...
}