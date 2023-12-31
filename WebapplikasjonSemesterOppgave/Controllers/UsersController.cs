using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Models;
using Microsoft.AspNetCore.Authorization;
using WebapplikasjonSemesterOppgave.ViewModels;


namespace WebapplikasjonSemesterOppgave.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly DBContextSample _context;
        private readonly UserManager<SampleUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(DBContextSample context, UserManager<SampleUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Users
        /// <summary>
        /// Retrieves a list of users along with their associated roles. 
        /// This method initializes an empty list of 'UserWithRoleView' objects, 
        /// iterates through all users fetched from the database using '_userManager', 
        /// and for each user, retrieves their roles using 'GetRolesAsync'. 
        /// It assumes a single role per user, adds the user and their role to the list, 
        /// and passes this list to the view for display.
        /// </summary>
        /// <returns>
        /// Returns a view displaying all users with their associated roles.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Index(string role = null)
        {
            var usersWithRoles = new List<UserWithRoleVM>();

            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault(); // Assuming one role per user

                if (role == null || userRole == role)
                {
                    usersWithRoles.Add(new UserWithRoleVM
                    {
                        User = user,
                        Role = userRole != null ? await _roleManager.FindByNameAsync(userRole) : null
                    });
                }
            }

            return View(usersWithRoles);
        }


        /// <summary>
        /// Retrieves and displays the details of a specific user based on the provided user ID.
        /// This method checks if the ID is null or if the Users set in the context is null, and returns a NotFound result in these cases.
        /// It searches for the user in the database with the specified ID using 'FirstOrDefaultAsync'.
        /// If the user is not found, it returns a NotFound result. Otherwise, it passes the user details to the view for display.
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <returns>
        /// If the user ID is null, the Users set is null, or the user is not found, it returns a NotFound result.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var sampleUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sampleUser == null)
            {
                return NotFound();
            }

            return View(sampleUser);
        }

        
        /// <summary>
        /// Retrieves a user's details for editing based on the provided user ID. 
        /// This method first checks if the user ID is null, returning NotFound if so.
        /// It attempts to find the user in the database. If not found, it returns NotFound.
        /// The method also fetches all available roles and the roles assigned to the user, 
        /// preparing a 'UserWithRoleView' model with these details for the view.
        /// In case of an exception, it returns a 500 Internal Server Error with the exception message.
        /// </summary>
        /// <param name="id">The ID of the user to be edited.</param>
        /// <returns>
        /// An 'IActionResult' that renders the edit view with the user and role data if the user is found.
        /// Returns a NotFound result if the ID is null or the user isn't found.
        /// In case of an exception, returns a StatusCode 500 result with the error message.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var sampleUser = await _context.Users.FindAsync(id);
                if (sampleUser == null)
                {
                    return NotFound();
                }

                // Get all available roles
                var allRoles = await _roleManager.Roles.ToListAsync();

                // Get the roles assigned to the user
                var userRoles = await _userManager.GetRolesAsync(sampleUser);

                var model = new UserWithRoleVM
                {
                    User = sampleUser,
                    AllRoles = allRoles,
                    SelectedRole = userRoles.FirstOrDefault() // Assuming a user has only one role
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the submission of user edit details. This method verifies the user ID and model's user ID are the same, returning NotFound if not.
        /// It then checks if the ModelState is valid. If valid, it updates the user's properties and roles in the database.
        /// In case of a concurrency exception, it checks if the user still exists and either returns NotFound or rethrows the exception.
        /// If the ModelState is invalid, it reloads the roles and redisplays the form with the current model.
        /// </summary>
        /// <param name="id">The ID of the user being edited.</param>
        /// <param name="model">The 'UserWithRoleView' model containing the user's updated information.</param>
        /// <returns>
        /// Redirects to the 'Index' view if the edit is successful.
        /// Returns a NotFound result if the IDs do not match or if the user no longer exists after a concurrency exception.
        /// If the ModelState is invalid, returns the same view with the current model and reloaded roles.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserWithRoleVM model)
        {
            if (id != model.User.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Order");
            if (ModelState.IsValid)
            {
                try
                {
                    // Update user properties
                    var user = await _context.Users.FindAsync(id);
                    user.FirstName = model.User.FirstName;
                    user.LastName = model.User.LastName;
                    user.Address = model.User.Address;
                    user.Email = model.User.Email;

                    var selectedRole = await _roleManager.FindByNameAsync(model.SelectedRole);

                    // Update user's role
                    var existingRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, existingRoles);
                    await _userManager.AddToRoleAsync(user, model.SelectedRole);

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SampleUserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If the ModelState is not valid, reload roles and redisplay the form
            
            model.AllRoles = await _roleManager.Roles.ToListAsync();
            return View(model);
        }
        
        // GET: Users/Delete/5
        /// <summary>
        /// Retrieves a user's details for deletion based on the provided user ID. 
        /// This method checks if the user ID is null or if the Users set in the context is null, and returns a NotFound result in these cases.
        /// It searches for the user in the database with the specified ID using 'FirstOrDefaultAsync'.
        /// If the user is not found, it returns a NotFound result. Otherwise, it passes the user details to the view for confirmation of deletion.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>
        /// An 'IActionResult' that renders the delete confirmation view if the user is found. 
        /// Returns a NotFound result if the user ID is null, the Users set is null, or the user is not found.
        /// </returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var sampleUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sampleUser == null)
            {
                return NotFound();
            }

            return View(sampleUser);
        }
        
        // POST: Users/Delete/5
        /// <summary>
        /// Handles the confirmation of a user's deletion. This method first checks if the Users set in the context is null, 
        /// returning a problem detail if so. It then attempts to find the user in the database using the provided ID. 
        /// If the user is found, it removes the user from the context and saves the changes. 
        /// The method then redirects to the 'Index' view, indicating the deletion process has been completed.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>
        /// An 'IActionResult' that redirects to the 'Index' view after successful deletion.
        /// Returns a problem detail if the Users set in the context is null.
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DBContextSample.Users'  is null.");
            }
            var sampleUser = await _context.Users.FindAsync(id);
            if (sampleUser != null)
            {
                _context.Users.Remove(sampleUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        /// <summary>
        /// Checks if a user with the specified ID exists in the database.
        /// This method uses the 'Any' LINQ method to determine whether any user in the Users set has the given ID.
        /// It safely handles the case where the Users set might be null.
        /// </summary>
        /// <param name="id">The ID of the user to check for existence.</param>
        /// <returns>
        /// True if a user with the specified ID exists; otherwise, false.
        /// </returns>
        private bool SampleUserExists(string id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
