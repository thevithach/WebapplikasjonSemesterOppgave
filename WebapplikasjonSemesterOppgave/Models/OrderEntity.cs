using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;

namespace WebapplikasjonSemesterOppgave.Models
{
	public class OrderEntity
	{
		
		
			[Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string? ProductType { get; set; }
            public string? SerialNumber { get; set; }
            public string? ModelYear { get; set; }
            public bool? Warranty { get; set; }
            public bool? ServiceOrRepair { get; set; }
            public string? CustomerAgreement { get; set; }
            public string? ReparationDetails { get; set; }
            public string? WorkingHours { get; set; }
            public string? ReplacedPartsReturned { get; set; }
            public string? ShippingMethods { get; set; }
            public string UserId { get; set; }
            public SampleUser User { get; set; }
            public List<ServiceChecklistEntity> ChecklistItems { get; set; }

    }
	
}

