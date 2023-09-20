using System;
using System.ComponentModel.DataAnnotations;

namespace WebapplikasjonSemesterOppgave.Models
{
	public class EmployeeEntity
	{
		
		
			[Key]
			public int Id { get; set; }
			public string Name { get; set; }
			public string Designation { get; set; }
			public string Department { get; set; }
			public string Email { get; set; }

    }
	
}

