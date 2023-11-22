using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebapplikasjonSemesterOppgave.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// Initialiserer en ny instans av AppRolesController klassen.
        /// Denne konstruktøren tar en RoleManager<IdentityRole> som en avhengighet for å håndtere rolleoperasjoner.
        /// </summary>
        /// <param name="roleManager">RoleManager brukes til å håndtere brukerroller i applikasjonen.</param>
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        /// <summary>
        /// Henter og viser en liste over alle roller i applikasjonen. 
        /// Dette inkluderer alle roller som er opprettet.
        /// </summary>
        /// <returns>En View som inneholder en liste over roller.</returns>
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        
        /// <summary>
        /// Returnerer en view hvor en ny rolle kan opprettes. Denne metoden viser et skjema for oppretting av en ny rolle.
        /// </summary>
        /// <returns>En View for å opprette en ny rolle.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Oppretter en ny rolle i databasen, metoden sjekker om rollen eksisterer fra før
        /// og unngår å opprette duplikater. Dersom rollen ikke eksieterer fra før opprettes den
        /// </summary>
        /// <param name="model">Parameteren tar i mot en IdentityRole model som inneholder navnet på rollen</param>
        /// <returns>Redirectes til index og viser en tabell med roller opprettet</returns>
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            //Avoid duplicate role
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}

