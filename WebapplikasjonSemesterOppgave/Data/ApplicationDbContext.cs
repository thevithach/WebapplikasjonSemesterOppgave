using System;
using Microsoft.EntityFrameworkCore;
using WebapplikasjonSemesterOppgave.Models;

namespace WebapplikasjonSemesterOppgave.Areas.Identity.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        

    }
}

