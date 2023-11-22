using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Models;


namespace WebapplikasjonSemesterOppgave.Data
{
    public class ServiceorderController : Controller
    {
        private readonly DBContextSample _context;

        public ServiceorderController(DBContextSample context)
        {
            _context = context;
        }
        

        public IActionResult ServiceOrderDetails(int id)
        {
            var serviceOrder = _context.OrderEntity.Find(id);
            if (serviceOrder == null)
            {
                return NotFound();
            }
            return View("Details", serviceOrder);
        }

        // GET: Serviceorder
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






        // public async Task<IActionResult> Index()
        // {
        //     var dBContextSample = _context.OrderEntity.Include(o => o.User);
        //     return View(await dBContextSample.ToListAsync());
        // }
        

        // GET: Serviceorder/Details/5
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

        // GET: Serviceorder/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
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

                    // Set the properties for the checklist items based on your form data or requirements
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
                    XxBar = "",
                    VinsjKjørAlleFunksjoner = "",
                    TrekkraftKN = "",
                    BremsekraftKN = "",
                    // ... set other properties similarly
                };
                // Add the checklist item to the context
                _context.ChecklistItems.Add(checklist);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        
        // GET: Serviceorder/Edit/5
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

        // POST: Serviceorder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductType,SerialNumber,ModelYear,Warranty,ServiceOrRepair,CustomerAgreement,ReparationDetails,WorkingHours,ReplacedPartsReturned,ShippingMethods,OrderCreatedDate,OrderPlacerCustomer,ProductReceivedDate,ProductAgreedCompletionDate,UserId")] OrderEntity orderEntity)
        {
            if (id != orderEntity.Id)
            {
                return NotFound();
            }
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
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

        // GET: Serviceorder/Delete/5
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

        // POST: Serviceorder/Delete/5
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

        private bool OrderEntityExists(int id)
        {
          return (_context.OrderEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
