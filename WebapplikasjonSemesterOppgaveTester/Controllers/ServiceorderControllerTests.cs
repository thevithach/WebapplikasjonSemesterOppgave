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
    private readonly DBContextSample _context;
    private readonly ServiceorderController _controller;    
    
    public ServiceorderControllerUnitTests()
    {
        var options = new DbContextOptionsBuilder<DBContextSample>()
            .UseInMemoryDatabase(databaseName: "OrderDB")
            .Options;
        _context = new DBContextSample(options);
        _controller = new ServiceorderController(_context);
    }
    
    /// <summary>
    /// Verifies that the Index action in the ServiceorderController returns a view with a list of service orders.
    /// </summary>
    [Fact]
    public async Task Index_ReturnsViewResultWithOrders()
    {
        await ClearDatabase();
        // Arrange
        var orders = TestDataModel();
        _context.OrderEntity.Add(orders);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _controller.Index();
        

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<OrderEntity>>(viewResult.Model);
        Assert.NotNull(model);
    }

    /// <summary>
    /// Verifies that the Create action in the ServiceorderController, when given a valid model, returns a RedirectToActionResult.
    /// </summary>
    [Fact]
    public async Task Create_Post_ValidModel_ReturnsRedirectToActionResult()
    {
        await ClearDatabase();

        // Arrange

        var controller = new ServiceorderController(_context);

        var order = TestDataModel();
        
        // Act
        var result = await controller.Create(order) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action
    }
    
    /// <summary>
    /// Verifies that the Edit action in the ServiceorderController, when given a valid model, updates the model and redirects to the Index action.
    /// </summary>
    [Fact]
    public async Task Edit_Post_ValidModel_UpdatesModel_()
    {
        await ClearDatabase();
        // Arrange
        var originalOrder = TestDataModel();
        _context.OrderEntity.Add(originalOrder);
        await _context.SaveChangesAsync();

        originalOrder.ProductType = "Updated Product Type";
        originalOrder.SerialNumber = "Updated Serial Number";


        // Act
        var result = await _controller.Edit(originalOrder.Id, originalOrder);

        // Assert
        var updatedEntity = _context.OrderEntity.Find(originalOrder.Id);
        Assert.NotNull(updatedEntity);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Updated Product Type", updatedEntity.ProductType);
        Assert.Equal("Updated Serial Number", updatedEntity.SerialNumber);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        // Assert other updated properties
    }
    
    /// <summary>
    /// Verifies that the Delete action in the ServiceorderController, when given a valid ID, removes the corresponding model.
    /// </summary>
    [Fact]
    public async Task Delete_Post_ValidId_RemovesModel()
    {
        await ClearDatabase();

        // Arrange
        var orderToDelete = TestDataModel();
        _context.OrderEntity.Add(orderToDelete);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.DeleteConfirmed(orderToDelete.Id); // Assuming the action is named DeleteConfirmed

        // Assert
        var deletedEntity = _context.OrderEntity.Find(orderToDelete.Id);
        Assert.Null(deletedEntity);
    }
    
    /// <summary>
    /// Verifies that the ServiceOrderDetails action in the ServiceorderController, when given a valid ID, returns the corresponding service order.
    /// </summary>
    [Fact]
    public async Task DetailsWithValidIdReturnsOrdre()
    {
        await ClearDatabase();

        // Arrange
        var testOrdre = TestDataModel();
        await _context.OrderEntity.AddAsync(testOrdre);
        await _context.SaveChangesAsync();

        var orderId = testOrdre.Id; // Get the generated ID

        // Act
        var result = _controller.ServiceOrderDetails(orderId); 

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<OrderEntity>(viewResult.Model);
        Assert.Equal(orderId, model.Id); // Use the generated ID for comparison
    }

    /// <summary>
    /// Verifies that the ServiceOrderDetails action in the ServiceorderController, when given an invalid ID, returns a NotFound result.
    /// </summary>
    [Fact]
    public async Task DetailsWithInvalidIdReturnsNotFound()
    {
        await ClearDatabase();
        // Act
        var result = await _controller.Details(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    /// <summary>
    /// Clears the in-memory database to prepare for test cases.
    /// </summary>
    private async Task ClearDatabase()
    {
        foreach (var entity in _context.OrderEntity)
        {
            _context.Remove(entity);
        }
        await _context.SaveChangesAsync();
    }
    /// <summary>
    /// Generates a sample OrderEntity for testing purposes.
    /// </summary>
    /// <returns>A sample OrderEntity object.</returns>
    private OrderEntity TestDataModel()
    {
        return new OrderEntity()
        {
            Id = 1,
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
        };
    }
}