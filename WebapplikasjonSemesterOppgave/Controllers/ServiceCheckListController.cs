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
    // Controller for managing ServiceCheckList
    public class ServiceCheckListController : Controller
    {
        private readonly DBContextSample _context;

        // Constructor to initialize the controller with a database context
        public ServiceCheckListController(DBContextSample context)
        {
            _context = context;
        }

        // Calculates the overall order status based on checklist items
        public string CalculateOrderStatus(int orderId)
        {
            // Retrieve checklist items for the specified order
            var checklistItems = _context.ChecklistItems.Where(item => item.OrderId == orderId).ToList();

            // Initialize flags to track completion status for each role
            bool allMechanicsDone = true;
            bool allHydraulicsDone = true;
            bool allElectriciansDone = true;

            // Iterate through checklist items to determine completion status
            foreach (var item in checklistItems)
            {
                // Check if the mechanic has completed their part
                if (item.mechanicDone != true)
                {
                    allMechanicsDone = false;
                }

                // Check if the hydraulics have completed their part
                if (item.hydraulicsDone != true)
                {
                    allHydraulicsDone = false;
                }

                // Check if the electrician has completed their part
                if (item.electricianDone != true)
                {
                    allElectriciansDone = false;
                }
            }

            // Determine the overall order status based on role completion
            if (allMechanicsDone && allHydraulicsDone && allElectriciansDone)
            {
                return "Ferdig"; // Completed
            }
            if (allMechanicsDone)
            {
                return "Venter på hydraulikk og Elektriker"; // Waiting for hydraulics and electrician
            }

            return "Under_behandling"; // In progress
        }

        // Displays a list of checklist items with their order statuses
        public async Task<IActionResult> Index()
        {
            // Retrieve checklist items with associated orders
            var checklistItems = await _context.ChecklistItems
                .Include(s => s.Order)
                .ToListAsync();

            // Create a list to store order statuses
            var orderStatusList = new List<string>();

            // Calculate and store order status for each checklist item
            foreach (var item in checklistItems)
            {
                string orderStatus = CalculateOrderStatus(item.OrderId);
                orderStatusList.Add(orderStatus);
            }

            // Pass checklist items and order statuses to the view
            ViewBag.ChecklistItems = checklistItems;
            ViewBag.OrderStatusList = orderStatusList;

            return View();
        }

        // Displays details of a specific checklist item
        public async Task<IActionResult> Details(int? id)
        {
            // Validate input
            if (id == null || _context.ChecklistItems == null)
            {
                return NotFound();
            }

            // Retrieve checklist item with associated order
            var serviceChecklistEntity = await _context.ChecklistItems
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);

            // Check if the checklist item exists
            if (serviceChecklistEntity == null)
            {
                return NotFound();
            }

            return View(serviceChecklistEntity);
        }

        // Displays the form to create a new checklist item
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id");
            return View();
        }

        // Handles the creation of a new checklist item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClutchlamelerSlitasje,Bremser,LagerforTrommel,PTOogOpplagring,Kjedestrammer,Wire,PinionLager,KilepåKjedehjul,mechanicDone,SylinderLekkasje,SlangeSkadeLekkasje,HydraulikkblokkTestbenk,SkiftOljeiTank,SkiftOljepåGirboks,Ringsylinder,Bremsesylinder,hydraulicsDone,LedningsnettpåVinsj,TestRadio,Knappekasse,electricianDone,XxBar,VinsjKjørAlleFunksjoner,TrekkraftKN,BremsekraftKN,OrderId")] ServiceChecklistEntity serviceChecklistEntity)
        {
            // Remove the "Order" from model state to prevent overposting
            ModelState.Remove("Order");

            // Validate model state
            if (ModelState.IsValid)
            {
                // Add new checklist item to the context and save changes
                _context.Add(serviceChecklistEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Provide the order IDs for the dropdown list in case of validation errors
            ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id", serviceChecklistEntity.OrderId);
            return View(serviceChecklistEntity);
        }

        // Displays the form to edit a checklist item
        public async Task<IActionResult> Edit(int? id)
        {
            // Validate input
            if (id == null || _context.ChecklistItems == null)
            {
                return NotFound();
            }

            // Retrieve checklist item for editing
            var serviceChecklistEntity = await _context.ChecklistItems.FindAsync(id);

            // Check if the checklist item exists
            if (serviceChecklistEntity == null)
            {
                return NotFound();
            }

            // Provide the order IDs for the dropdown list
            ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id", serviceChecklistEntity.OrderId);
            return View(serviceChecklistEntity);
        }

        // Handles the update of a checklist item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClutchlamelerSlitasje,Bremser,LagerforTrommel,PTOogOpplagring,Kjedestrammer,Wire,PinionLager,KilepåKjedehjul,mechanicDone,SylinderLekkasje,SlangeSkadeLekkasje,HydraulikkblokkTestbenk,SkiftOljeiTank,SkiftOljepåGirboks,Ringsylinder,Bremsesylinder,hydraulicsDone,LedningsnettpåVinsj,TestRadio,Knappekasse,electricianDone,XxBar,VinsjKjørAlleFunksjoner,TrekkraftKN,BremsekraftKN,OrderId")] ServiceChecklistEntity serviceChecklistEntity)
        {
            // Validate if the provided ID matches the checklist item's ID
            if (id != serviceChecklistEntity.Id)
            {
                return NotFound();
            }

            try
            {
                // Update the checklist item and save changes
                _context.Update(serviceChecklistEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check for concurrency issues
                if (!ServiceChecklistEntityExists(serviceChecklistEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Redirect to the index action of the ServiceOrder controller
            return RedirectToAction("Index", "ServiceOrder");
        }

        // Displays the form to confirm the deletion of a checklist item
        public async Task<IActionResult> Delete(int? id)
        {
            // Validate input
            if (id == null || _context.ChecklistItems == null)
            {
                return NotFound();
            }

            // Retrieve checklist item for deletion
            var serviceChecklistEntity = await _context.ChecklistItems
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);

            // Check if the checklist item exists
            if (serviceChecklistEntity == null)
            {
                return NotFound();
            }

            return View(serviceChecklistEntity);
        }

        // Handles the deletion of a checklist item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the checklist item exists
            if (_context.ChecklistItems == null)
            {
                return Problem("Entity set 'DBContextSample.ChecklistItems'  is null.");
            }

            var serviceChecklistEntity = await _context.ChecklistItems.FindAsync(id);

            // Remove the checklist item and save changes
            if (serviceChecklistEntity != null)
            {
                _context.ChecklistItems.Remove(serviceChecklistEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Checks if a checklist item with the given ID exists
        private bool ServiceChecklistEntityExists(int id)
        {
            return (_context.ChecklistItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
