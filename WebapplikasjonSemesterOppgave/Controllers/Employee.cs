using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Models;

namespace WebapplikasjonSemesterOppgave.Controllers
{
    public class Employee : Controller
    {
        private readonly ApplicationDbContext _context;

        public Employee(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
              return _context.EmployeeDetails != null ? 
                          View(await _context.EmployeeDetails.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.EmployeeDetails'  is null.");
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeEntity = await _context.EmployeeDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            return View(employeeEntity);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Designation,Department,Email")] EmployeeEntity employeeEntity)
        {
            // Sjekker om modellen er gyldig (employeeEntity objektet) ifh data annotasjonene og valideringen.
            if (ModelState.IsValid)
            {
                // Hvis gyldig legger til og lagrer til database
                _context.Add(employeeEntity);
                await _context.SaveChangesAsync();
                // Redirecter deg til index
                return RedirectToAction(nameof(Index));
            }
            // Hvis modellen er ugyldig, eks: validering errors, vil den returnere samme "Create" view med samme objekt
            // Tillater bruker å skrive om errors som oppstår ifh validering og kan dermed submitte formen igjen
            return View(employeeEntity);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeEntity = await _context.EmployeeDetails.FindAsync(id);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            return View(employeeEntity);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Designation,Department,Email")] EmployeeEntity employeeEntity)
        {
            if (id != employeeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeEntityExists(employeeEntity.Id))
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
            return View(employeeEntity);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeEntity = await _context.EmployeeDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            return View(employeeEntity);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeDetails'  is null.");
            }
            var employeeEntity = await _context.EmployeeDetails.FindAsync(id);
            if (employeeEntity != null)
            {
                _context.EmployeeDetails.Remove(employeeEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeEntityExists(int id)
        {
          return (_context.EmployeeDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
