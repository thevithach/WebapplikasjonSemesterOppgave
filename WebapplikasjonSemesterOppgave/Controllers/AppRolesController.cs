using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebapplikasjonSemesterOppgave.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// Initializes a new instance of the AppRolesController class.
        /// This constructor takes a RoleManager<IdentityRole> as a dependency to handle role operations.
        /// </summary>
        /// <param name="roleManager">RoleManager is used to handle all user roles in the application </param>
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        /// <summary>
        /// Retriever a list of all roles in the application. This includes all roles created.
        /// </summary>
        /// <returns>A View that includes a list of all roles</returns>
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        
        /// <summary>
        /// Returns a view where a role can be edited. This method shows a form for editing a role.
        /// </summary>
        /// <returns>A view to create a role</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Creates a new role in the database. The method checks if the role already exists
        /// and avoids creating duplicates. If the role does not exist it is created.
        /// </summary>
        /// <param name="model">The parameter takes a IdentityRole model that includes the name of the role</param>
        /// <returns>Redirects to index and shows the list of created roles</returns>
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            //Avoid duplicate role
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
        
    }
}

