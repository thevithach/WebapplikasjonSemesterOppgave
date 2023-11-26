using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using WebapplikasjonSemesterOppgave.Controllers;

namespace WebapplikasjonSemesterOppgave.UnitTests.Controllers
{
    public class AppRolesControllerTests
    {
        private readonly Mock<RoleManager<IdentityRole>> mockRoleManager;
        private readonly AppRolesController controller;

        public AppRolesControllerTests()
        {
            var store = new Mock<IRoleStore<IdentityRole>>();
            mockRoleManager = new Mock<RoleManager<IdentityRole>>(store.Object, null, null, null, null);

            // Initialize the controller with the mocked RoleManager
            controller = new AppRolesController(mockRoleManager.Object);
        }
        
        /// <summary>
        /// Verifies that the Create action in the AppRolesController correctly handles new role creation.
        /// </summary>
        [Fact]
        public async Task Create_Post_NewRole()
        {
            // Arrange
            var newRole = new IdentityRole { Name = "TestRole" };
            mockRoleManager.Setup(x => x.RoleExistsAsync(newRole.Name))
                .ReturnsAsync(false); // Setup the role existence check
            mockRoleManager.Setup(x => x.CreateAsync(newRole))
                .ReturnsAsync(IdentityResult.Success); // Setup the role creation

            // Act
            var result = await controller.Create(newRole);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName); // Redirects to Index
            mockRoleManager.Verify(x => x.CreateAsync(It.Is<IdentityRole>(r => r.Name == "TestRole")), Times.Once); // Verifies that CreateAsync was called once
        }
        
        /// <summary>
        /// Verifies that the Index action in the AppRolesController returns a view with a list of roles.
        /// </summary>
        [Fact]
        public void Index_ReturnsViewWithListOfRoles()
        {
            // Arrange
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Role1" },
                new IdentityRole { Name = "Role2" },
            };

            mockRoleManager.Setup(x => x.Roles)
                .Returns(roles.AsQueryable());

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<IdentityRole>>(viewResult.Model);
            Assert.Equal(2, model.Count()); // Ensure the correct number of roles is passed to the view
        }
    }
}