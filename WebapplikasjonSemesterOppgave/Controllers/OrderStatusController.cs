/*using Microsoft.AspNetCore.Mvc;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;

namespace WebapplikasjonSemesterOppgave.Controllers;

public class OrderStatusController : Controller
{
    private readonly DBContextSample _context;

    public OrderStatusController(DBContextSample context)
    {
        _context = context;
    }
    // GET
    
    public string CalculateOrderStatus(int orderId)
    {
        var checklistItems = _context.ChecklistItems.Where(item => item.OrderId == orderId).ToList();

        bool allMechanicsDone = true;
        bool allHydraulicsDone = true;
        bool allElectriciansDone = true;

        foreach (var item in checklistItems)
        {
            if (item.mechanicDone != true)
            {
                allMechanicsDone = false;
            }

            if (item.hydraulicsDone != true)
            {
                allHydraulicsDone = false;
            }

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



}*/