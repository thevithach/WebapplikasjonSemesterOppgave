using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;

namespace WebapplikasjonSemesterOppgave.Models
{
	public class UserWithRoleView
	{
        
        public SampleUser User { get; set; }
        public IdentityRole Role { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public string SelectedRole { get; set; }

    }
}

