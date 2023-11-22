using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebapplikasjonSemesterOppgave.Controllers;
using Xunit;

public class AppRolesControllerTests
{
    [Fact]
    public void Index_ReturnsAViewResult_WithAListOfRoles()
    {
        // Arrange
        var mockRoleManager = new Mock<RoleManager<IdentityRole>>(
            Mock.Of<IUserStore<IdentityRole>>(), null, null, null, null);

        var roles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Admin" },
            new IdentityRole { Name = "User" }
        };

        mockRoleManager.Setup(m => m.Roles).Returns(roles.AsQueryable());

        var controller = new AppRolesController(mockRoleManager.Object);

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<IdentityRole>>(viewResult.ViewData.Model);
        Assert.Equal(2, ((List<IdentityRole>)model).Count);
    }
}