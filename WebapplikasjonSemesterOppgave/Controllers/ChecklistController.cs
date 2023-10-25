using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Models;

namespace WebapplikasjonSemesterOppgave.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly DBContextSample _context;

        public ChecklistController(DBContextSample context)
        {
            _context = context;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

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

    }
}