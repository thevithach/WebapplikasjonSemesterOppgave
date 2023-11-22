using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;

namespace WebapplikasjonSemesterOppgave.Models
{
	public class UserWithRoleView
	{
        
        public SampleUser User { get; set; }
        [ValidateNever]
        public IdentityRole? Role { get; set; }
        [ValidateNever]
        public List<IdentityRole>? AllRoles { get; set; }
        public string SelectedRole { get; set; }

    }
}

