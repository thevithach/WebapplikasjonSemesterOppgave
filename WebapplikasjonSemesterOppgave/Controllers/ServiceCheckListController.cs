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
    public class ServiceCheckListController : Controller
    {
        private readonly DBContextSample _context;

        public ServiceCheckListController(DBContextSample context)
        {
            _context = context;
        }
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
        // GET: ServiceCheckList
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

            return View();
        }


        // GET: ServiceCheckList/Details/5
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

        // GET: ServiceCheckList/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id");
            return View();
        }

        // POST: ServiceCheckList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClutchlamelerSlitasje,Bremser,LagerforTrommel,PTOogOpplagring,Kjedestrammer,Wire,PinionLager,KilepåKjedehjul,SylinderLekkasje,SlangeSkadeLekkasje,HydraulikkblokkTestbenk,SkiftOljeiTank,SkiftOljepåGirboks,Ringsylinder,Bremsesylinder,LedningsnettpåVinsj,TestRadio,Knappekasse,XxBar,VinsjKjørAlleFunksjoner,TrekkraftKN,BremsekraftKN,OrderId")] ServiceChecklistEntity serviceChecklistEntity)
        {
            ModelState.Remove("Order");
             if (ModelState.IsValid)
             {
            
                _context.Add(serviceChecklistEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
             }

             ViewData["OrderId"] = new SelectList(_context.OrderEntity, "Id", "Id", serviceChecklistEntity.OrderId);
             return View(serviceChecklistEntity);
        }

        // GET: ServiceCheckList/Edit/5
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

        // POST: ServiceCheckList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClutchlamelerSlitasje,Bremser,LagerforTrommel,PTOogOpplagring,Kjedestrammer,Wire,PinionLager,KilepåKjedehjul,mechanicDone,SylinderLekkasje,SlangeSkadeLekkasje,HydraulikkblokkTestbenk,SkiftOljeiTank,SkiftOljepåGirboks,Ringsylinder,Bremsesylinder,hydraulicsDone,LedningsnettpåVinsj,TestRadio,Knappekasse,electricianDone,XxBar,VinsjKjørAlleFunksjoner,TrekkraftKN,BremsekraftKN,OrderId")] ServiceChecklistEntity serviceChecklistEntity)
        {
            if (id != serviceChecklistEntity.Id)
            {
                return NotFound();
            }
            try
            {
                // Update your model state for "orderID" instead of "order"
                if (ModelState.ContainsKey("OrderId"))
                {
                    ModelState["orderID"].Errors.Clear();
                }

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


        // GET: ServiceCheckList/Delete/5
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

        // POST: ServiceCheckList/Delete/5
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

        private bool ServiceChecklistEntityExists(int id)
        {
          return (_context.ChecklistItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
