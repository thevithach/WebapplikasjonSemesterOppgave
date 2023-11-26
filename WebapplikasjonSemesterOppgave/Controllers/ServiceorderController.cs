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


namespace WebapplikasjonSemesterOppgave.Data
{
    [Authorize]
    public class ServiceorderController : Controller
    {
        private readonly DBContextSample _context;

        public ServiceorderController(DBContextSample context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Retrieves and displays details of a specific service order.
        /// </summary>
        /// <param name="id">The ID of the service order to display.</param>
        /// <returns>An IActionResult representing the details view of the service order. Returns NotFound if the order is not found.</returns>
        [HttpGet]
        public IActionResult ServiceOrderDetails(int id)
        {
            var serviceOrder = _context.OrderEntity.Find(id);
            if (serviceOrder == null)
            {
                return NotFound();
            }
            return View("Details", serviceOrder);
        }
        
        /// <summary>
        /// Displays a list of service orders, optionally filtered by status.
        /// </summary>
        /// <param name="statusFilter">An optional filter to view service orders by their status.</param>
        /// <returns>An IActionResult representing the index view with a list of service orders.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(ServiceOrderStatus? statusFilter = null)
        {
            var orders = await _context.OrderEntity
                .Include(o => o.ChecklistItems)
                .Include(o => o.User)
                .ToListAsync();

            if (statusFilter.HasValue)
            {
                orders = orders.Where(o => o.OrderStatus == statusFilter.Value).ToList();
            }

            ViewBag.StatusFilter = statusFilter;
            return View(orders);
        }
        
        /// <summary>
        /// Displays details of a specific service order.
        /// </summary>
        /// <param name="id">The ID of the service order.</param>
        /// <returns>An IActionResult representing the details view of the service order. Returns NotFound if the order or ID is null.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderEntity == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.OrderEntity
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        /// <summary>
        /// Displays the view for creating a new service order.
        /// </summary>
        /// <returns>An IActionResult representing the create view for service orders.</returns>
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        /// <summary>
        /// Handles the creation of a new service order.
        /// </summary>
        /// <param name="order">The service order to create.</param>
        /// <returns>Redirects to the Index action on success, or returns to the Create view on failure.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(OrderEntity order)
        {

            if (ModelState.IsValid)
            {
                // Save the order to the database
                // Response.Headers.Add("X-Content-Type-Options", "nosniff");
                _context.OrderEntity.Add(order);
                await _context.SaveChangesAsync();

                var checklist = new ServiceChecklistEntity()
                {
                    // Set the attributes of the checklist item
                    OrderId = order.Id,

                    // Properties based on the checklist
                    ClutchlamelerSlitasje = null,
                    Bremser = null,
                    LagerforTrommel = null,
                    PTOogOpplagring = null,
                    Kjedestrammer = null,
                    Wire = null,
                    PinionLager = null,
                    KilepåKjedehjul = null,
                    mechanicDone = null,
                    //Hydraulikk
                    SylinderLekkasje = null,
                    SlangeSkadeLekkasje = null,
                    HydraulikkblokkTestbenk = null,
                    SkiftOljeiTank = null,
                    SkiftOljepåGirboks = null,
                    Ringsylinder = null,
                    Bremsesylinder = null,
                    hydraulicsDone = null,
                    //Elektriker
                    LedningsnettpåVinsj = null,
                    TestRadio = null,
                    Knappekasse = null,
                    electricianDone = null,
                    XxBar = null,
                    VinsjKjørAlleFunksjoner = null,
                    TrekkraftKN = null,
                    BremsekraftKN = null,
                };
                // Add the checklist item to the context
                _context.ChecklistItems.Add(checklist);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        
        /// <summary>
        /// Displays the edit view for a specific service order.
        /// </summary>
        /// <param name="id">The ID of the service order to edit.</param>
        /// <returns>An IActionResult representing the edit view for the service order. Returns NotFound if the order or ID is null.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderEntity == null)
            {
                return NotFound();
            }
            var orderEntity = await _context.OrderEntity.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderEntity.UserId);
            return View(orderEntity);
        }

        /// <summary>
        /// Handles the submission of an edited service order.
        /// </summary>
        /// <param name="id">The ID of the service order being edited.</param>
        /// <param name="orderEntity">The edited service order.</param>
        /// <returns>Redirects to the Index action on success, or returns to the Edit view on failure.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductType,SerialNumber,ModelYear,Warranty,ServiceOrRepair,CustomerAgreement,ReparationDetails,WorkingHours,ReplacedPartsReturned,ShippingMethods,OrderCreatedDate,OrderPlacerCustomer,ProductReceivedDate,ProductAgreedCompletionDate,UserId")] OrderEntity orderEntity)
        {
            if (id != orderEntity.Id)
            {
                return NotFound();
            } 
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderEntityExists(orderEntity.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderEntity.UserId);
            return View(orderEntity);
        }
        
        /// <summary>
        /// Displays the delete confirmation view for a specific service order.
        /// </summary>
        /// <param name="id">The ID of the service order to delete.</param>
        /// <returns>An IActionResult representing the delete confirmation view for the service order. Returns NotFound if the order or ID is null.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderEntity == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.OrderEntity
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        /// <summary>
        /// Confirms and processes the deletion of a specific service order.
        /// </summary>
        /// <param name="id">The ID of the service order to delete.</param>
        /// <returns>Redirects to the Index action on successful deletion, or returns a Problem detail if the entity set is null.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderEntity == null)
            {
                return Problem("Entity set 'DBContextSample.OrderEntity'  is null.");
            }
            
            var orderEntity = await _context.OrderEntity.FindAsync(id);
            if (orderEntity != null)
            {
                _context.OrderEntity.Remove(orderEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        /// <summary>
        /// Checks if a service order entity exists in the database.
        /// </summary>
        /// <param name="id">The ID of the service order entity to check.</param>
        /// <returns>True if the service order entity exists, otherwise false.</returns>
        private bool OrderEntityExists(int id)
        {
          return (_context.OrderEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
