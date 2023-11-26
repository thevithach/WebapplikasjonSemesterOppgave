using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Controllers;
using WebapplikasjonSemesterOppgave.Models;

namespace WebapplikasjonSemesterOppgave.UnitTests.Controllers;

public class ServiceCheckListControllerTests
{
    private readonly DBContextSample _context;
    private readonly ServiceCheckListController _controller;

    public ServiceCheckListControllerTests()
    {
        var options = new DbContextOptionsBuilder<DBContextSample>()
            .UseInMemoryDatabase(databaseName: "ChecklistDB")
            .Options;
        _context = new DBContextSample(options);
        _controller = new ServiceCheckListController(_context);
    }

    /// <summary>
    /// Verifies that the Index action in the ServiceCheckListController returns a view with a list of service checklists.
    /// </summary>
    [Fact]
    public async Task Index_ReturnsViewResultWithOrders()
    {
        await ClearDatabase();
        // Arrange
        var checklist = TestDataModel();
        _context.ChecklistItems.Add(checklist);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _controller.Index();
        

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<ServiceChecklistEntity>>(viewResult.Model);
        Assert.NotNull(model);
    }
    
    /// <summary>
    /// Verifies that the Create action in the ServiceCheckListController, when given a valid model, returns a RedirectToActionResult.
    /// </summary>
    [Fact]
    public async Task Create_Post_ValidModel_ReturnsRedirectToActionResult()
    {
        await ClearDatabase();

        // Arrange
        var order = TestDataModel();
        
        // Act
        var result = await _controller.Create(order) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action
    }
    
    /// <summary>
    /// Verifies that the Edit action in the ServiceCheckListController, when given a valid model, updates the model and redirects to the Index action.
    /// </summary>
    [Fact]
    public async Task Edit_Post_ValidModel_UpdatesModel_()
    {
        await ClearDatabase();
        // Arrange
        var originalChecklist = TestDataModel();
        _context.ChecklistItems.Add(originalChecklist);
        await _context.SaveChangesAsync();

        originalChecklist.Bremser = ChecklistItemCondition.OK;
        originalChecklist.mechanicDone = false;


        // Act
        var result = await _controller.Edit(originalChecklist.Id, originalChecklist);

        // Assert
        var updatedEntity = _context.ChecklistItems.Find(originalChecklist.Id);
        Assert.NotNull(updatedEntity);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(ChecklistItemCondition.OK, updatedEntity.Bremser);
        Assert.Equal(false, updatedEntity.mechanicDone);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        // Assert other updated properties
    }
    /// <summary>
    /// Verifies that the DeleteConfirmed action in the ServiceCheckListController, when given a valid ID, removes the corresponding model.
    /// </summary>
    [Fact]
    public async Task Delete_Post_ValidId_RemovesModel()
    {
        await ClearDatabase();

        // Arrange
        var checklistToDelete = TestDataModel();
        _context.ChecklistItems.Add(checklistToDelete);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.DeleteConfirmed(checklistToDelete.Id); // Assuming the action is named DeleteConfirmed

        // Assert
        var deletedEntity = _context.ChecklistItems.Find(checklistToDelete.Id);
        Assert.Null(deletedEntity);
    }
    
    /// <summary>
    /// Verifies that the Details action in the ServiceCheckListController, when given an invalid ID, returns a NotFound result.
    /// </summary>
    [Fact]
    public async Task Details_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        int testId = -1; // Invalid ID

        // Act
        var result = await _controller.Details(testId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    
    /// <summary>
    /// Clears the in-memory database to prepare for test cases.
    /// </summary>
    private async Task ClearDatabase()
    {
        foreach (var entity in _context.ChecklistItems)
        {
            _context.Remove(entity);
        }
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Generates a sample ServiceChecklistEntity for testing purposes.
    /// </summary>
    /// <returns>A sample ServiceChecklistEntity object.</returns>
    private ServiceChecklistEntity TestDataModel()
    {
        return new ServiceChecklistEntity()
        {
                Id = 3,
                ClutchlamelerSlitasje = ChecklistItemCondition.OK,
                Bremser = ChecklistItemCondition.Bør_Skiftes,
                LagerforTrommel = ChecklistItemCondition.Defekt,
                PTOogOpplagring = ChecklistItemCondition.OK,
                Kjedestrammer = ChecklistItemCondition.OK,
                Wire = ChecklistItemCondition.OK,
                PinionLager = ChecklistItemCondition.OK,
                KilepåKjedehjul = ChecklistItemCondition.OK,
                mechanicDone = true,
                SylinderLekkasje = ChecklistItemCondition.OK,
                SlangeSkadeLekkasje = ChecklistItemCondition.OK,
                HydraulikkblokkTestbenk = ChecklistItemCondition.OK,
                SkiftOljeiTank = ChecklistItemCondition.OK,
                SkiftOljepåGirboks = ChecklistItemCondition.OK,
                Ringsylinder = ChecklistItemCondition.OK,
                Bremsesylinder = ChecklistItemCondition.OK,
                hydraulicsDone = true,
                LedningsnettpåVinsj = ChecklistItemCondition.OK,
                TestRadio = ChecklistItemCondition.OK,
                Knappekasse = ChecklistItemCondition.OK,
                electricianDone = true,
                XxBar = "100",
                VinsjKjørAlleFunksjoner = "All Functions OK",
                TrekkraftKN = "50",
                BremsekraftKN = "60",
                OrderId = 123, 
        };
    }

}
