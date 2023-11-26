using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Models;

namespace WebapplikasjonSemesterOppgave.Controllers
{
    [Authorize]
    public class ServiceCheckListController : Controller
    {
        private readonly DBContextSample _context;

        public ServiceCheckListController(DBContextSample context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves a list of checklist items and their associated order statuses for display in the ServiceCheckList index view.
        /// </summary>
        /// <returns>A view with a list of checklists associated with different orders</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var checklistItems = await _context.ChecklistItems
                .Include(s => s.Order)
                .ToListAsync();

            var orderStatusList = new List<string>();

            foreach (var item in checklistItems)
            {
                // Calculate the order status for each checklist item and add it to the list
                string orderStatus = CalculateOrderStatus(item.OrderId);
                orderStatusList.Add(orderStatus);
            }

            // Pass the checklist items and order statuses to the view
            ViewBag.ChecklistItems = checklistItems;
            ViewBag.OrderStatusList = orderStatusList;

            return View(checklistItems);
        }
        /// <summary>
        /// Asynchronously retrieves a list of checklist items and their
        /// associated order statuses for display in the ServiceCheckList index view.
        /// </summary>
        /// <returns>
        /// An IActionResult representing the view of the ServiceCheckList index,
        /// populated with checklist items and their order statuses.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChecklistItems == null)
            {
                return NotFound();
            }

            var serviceChecklistEntity = await _context.ChecklistItems
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceChecklistEntity == null)
            {
                return NotFound();
            }

            return View(serviceChecklistEntity);
        }
        
        /// <summary>
        /// Prepares and displays the view for creating a new service checklist item.
        /// </summary>
        /// <returns>
        /// An IActionResult representing the view for creating a new service checklist item.
        /// </returns>
        /// <remarks>
        /// This method initializes the ViewData with a SelectList of Order IDs.
        /// This SelectList is used to populate a dropdown in the view, allowing the user to select an Order ID for the new checklist item.
        /// The method then returns the view for creating a new service checklist item.
        /// </remarks>
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id");
            return View();
        }
        
        /// <summary>
        /// Handles the submission of a new service checklist entity, saving it to the database.
        /// </summary>
        /// <param name="serviceChecklistEntity">The service checklist entity to be created and saved.</param>
        /// <returns>
        /// An IActionResult that redirects to the Index action upon successful creation,
        /// or returns to the Create view with the submitted data if validation fails.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClutchlamelerSlitasje,Bremser,LagerforTrommel,PTOogOpplagring,Kjedestrammer,Wire,PinionLager,KilepåKjedehjul,mechanicDone,SylinderLekkasje,SlangeSkadeLekkasje,HydraulikkblokkTestbenk,SkiftOljeiTank,SkiftOljepåGirboks,Ringsylinder,Bremsesylinder,hydraulicsDone,LedningsnettpåVinsj,TestRadio,Knappekasse,electricianDone,XxBar,VinsjKjørAlleFunksjoner,TrekkraftKN,BremsekraftKN,OrderId")] ServiceChecklistEntity serviceChecklistEntity)
        {
             if (ModelState.IsValid)
             {
            
                _context.Add(serviceChecklistEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
             }

             ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id", serviceChecklistEntity.OrderId);
             return View(serviceChecklistEntity);
        }
        
        /// <summary>
        /// Prepares and displays the edit view for a specified service checklist item.
        /// </summary>
        /// <param name="id">The ID of the service checklist item to edit.</param>
        /// <returns>
        /// An IActionResult that returns the edit view for the service checklist item.
        /// If the item is not found or the ID is null, a NotFound result is returned.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChecklistItems == null)
            {
                return NotFound();
            }

            var serviceChecklistEntity = await _context.ChecklistItems.FindAsync(id);
            if (serviceChecklistEntity == null)
            {
                return NotFound();
            }
            
            ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id", serviceChecklistEntity.OrderId);
            return View(serviceChecklistEntity);
        }
        
        
        /// <summary>
        /// Handles the submission of an edited service checklist entity and updates it in the database.
        /// </summary>
        /// <param name="id">The ID of the service checklist entity to be updated.</param>
        /// <param name="serviceChecklistEntity">The updated service checklist entity.</param>
        /// <returns>
        /// An IActionResult that redirects to the Index action of ServiceOrder upon successful update.
        /// If the entity is not found or the ID does not match, a NotFound result is returned.
        /// In case of model state invalidation, the edit view is returned with the current entity data.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClutchlamelerSlitasje,Bremser,LagerforTrommel,PTOogOpplagring,Kjedestrammer,Wire,PinionLager,KilepåKjedehjul,mechanicDone,SylinderLekkasje,SlangeSkadeLekkasje,HydraulikkblokkTestbenk,SkiftOljeiTank,SkiftOljepåGirboks,Ringsylinder,Bremsesylinder,hydraulicsDone,LedningsnettpåVinsj,TestRadio,Knappekasse,electricianDone,XxBar,VinsjKjørAlleFunksjoner,TrekkraftKN,BremsekraftKN,OrderId")] ServiceChecklistEntity serviceChecklistEntity)
        {
            if (id != serviceChecklistEntity.Id)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id", serviceChecklistEntity.OrderId);
                return View(serviceChecklistEntity);
            }
            
            try
            {
                _context.Update(serviceChecklistEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceChecklistEntityExists(serviceChecklistEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "ServiceOrder");
        }

        /// <summary>
        /// Prepares and displays the delete confirmation view for a specified service checklist item.
        /// </summary>
        /// <param name="id">The ID of the service checklist item to be deleted.</param>
        /// <returns>
        /// An IActionResult that returns the delete confirmation view for the specified service checklist item.
        /// If the item is not found or the ID is null, a NotFound result is returned.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChecklistItems == null)
            {
                return NotFound();
            }

            var serviceChecklistEntity = await _context.ChecklistItems
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceChecklistEntity == null)
            {
                return NotFound();
            }

            return View(serviceChecklistEntity);
        }

        /// <summary>
        /// Confirms the deletion of a specified service checklist entity and removes it from the database.
        /// </summary>
        /// <param name="id">The ID of the service checklist entity to be deleted.</param>
        /// <returns>
        /// An IActionResult that redirects to the Index action upon successful deletion.
        /// If the entity set is null, a Problem detail is returned.
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChecklistItems == null)
            {
                return Problem("Entity set 'DBContextSample.ChecklistItems'  is null.");
            }
            var serviceChecklistEntity = await _context.ChecklistItems.FindAsync(id);
            if (serviceChecklistEntity != null)
            {
                _context.ChecklistItems.Remove(serviceChecklistEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        /// <summary>
        /// Retrieves the details of a specific checklist item based on the provided order ID.
        /// </summary>
        /// <param name="id">The ID of the order associated with the checklist item to be retrieved.</param>
        /// <returns>
        /// Returns a view displaying the details of the specified checklist item.
        /// If no checklist item is found for the given order ID, a 'NotFound' result is returned.
        /// </returns>
        [HttpGet]
        public IActionResult ChecklistDetails(int id)
        {
            var checklist = _context.ChecklistItems
                .Include(c => c.Order)
                .SingleOrDefault(c => c.OrderId == id);

            if (checklist == null)
            {
                return NotFound();
            }

            return View("~/Views/ServiceCheckList/ChecklistDetails.cshtml", checklist);
        }
        
        /// <summary>
        /// Checks if a service checklist entity with a specific ID exists in the context.
        /// </summary>
        /// <param name="id">The ID of the service checklist entity to check for existence.</param>
        /// <returns>
        /// A boolean indicating whether the specified service checklist entity exists.
        /// </returns>
        private bool ServiceChecklistEntityExists(int id)
        {
          return (_context.ChecklistItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        /// <summary>
        /// Calculates the overall status of an order based on the completion status of tasks by different roles.
        /// </summary>
        /// <param name="orderId">The ID of the order for which the status is to be calculated.</param>
        /// <returns>
        /// A string indicating the overall status of the order. It can be "Ferdig",
        /// "Venter på hydraulikk og Elektriker"
        /// or "Under_behandling"
        /// </returns>
        public string CalculateOrderStatus(int orderId)
        {
            var checklistItems = _context.ChecklistItems.Where(item => item.OrderId == orderId).ToList();

            // Initialize flags to track the overall completion status for each role
            bool allMechanicsDone = true;
            bool allHydraulicsDone = true;
            bool allElectriciansDone = true;

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

            if (allMechanicsDone && allHydraulicsDone && allElectriciansDone)
            {
                return "Ferdig";
            }
            if (allMechanicsDone)
            {
                return "Venter på hydraulikk og Elektriker";
            }

            return "Under_behandling";
        }
    }
}
