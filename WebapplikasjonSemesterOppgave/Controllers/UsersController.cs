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


namespace WebapplikasjonSemesterOppgave.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Index()
        {
            //Starter tom liste "UserWithRoleView"
            var usersWithRoles = new List<UserWithRoleView>();
            //Itererer gjennom kolleksjonen av brukere
            //Fetcher alle brukere ved bruk av UserManager<SampleUser> instansen og konverterer resultatet til liste
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                //Fetcher rollen assosiert med bruker med GetRolesAsync som er en del av UserManager, lagres i var Roles
                var roles = await _userManager.GetRolesAsync(user);
                //Henter første rollenavn, men er bare en rolle til hver bruker så er perf
                var role = roles.FirstOrDefault(); // Antakelse av en bruker
                //Loopen iterer gjennom alle brukere og legger til usersWithRoles listen,
                //sender usersWithRoles som modell til view
                usersWithRoles.Add(new UserWithRoleView
                {
                    User = user,
                    Role = role != null ? await _roleManager.FindByNameAsync(role) : null
                });
            }

            return View(usersWithRoles);
            // Pass the List<UserWithRoleViewModel> to the view
            //return _context.Users != null ? 
            //View(await _context.Users.ToListAsync()) :
            //Problem("Entity set 'DBContextSample.Users'  is null.");
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] SampleUser sampleUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sampleUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sampleUser);
        }
        // GET: Users/Edit/5
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

                var model = new UserWithRoleView
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserWithRoleView model)
        {
            if (id != model.User.Id)
            {
                return NotFound();
            }

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

        //// GET: Users/Edit/5
        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var sampleUser = await _context.Users.FindAsync(id);
        //    if (sampleUser == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(sampleUser);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Address,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] SampleUser sampleUser)
        //{
        //    if (id != sampleUser.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(sampleUser);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SampleUserExists(sampleUser.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(sampleUser);
        //}


        // GET: Users/Delete/5
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

        private bool SampleUserExists(string id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
