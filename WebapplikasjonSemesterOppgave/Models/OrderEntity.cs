using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;

namespace WebapplikasjonSemesterOppgave.Models
{
	public enum ServiceOrderStatus
	{
		Under_behandling,
		Hos_Mekaniker,
		Hos_Hydraulikk,
		Hos_Elektriker,
		Ferdig
	}
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
            
            public ServiceOrderStatus? OrderStatus
            {
	            get
	            {
		            if (ChecklistItems == null || !ChecklistItems.Any())
		            {
			            return ServiceOrderStatus.Under_behandling; // Or another default status.
		            }

		            bool allMechanicsDone = ChecklistItems.All(item => item?.mechanicDone == true);
		            bool allHydraulicsDone = ChecklistItems.All(item => item?.hydraulicsDone == true);
		            bool allElectriciansDone = ChecklistItems.All(item => item?.electricianDone == true);

		            if (allMechanicsDone && allHydraulicsDone)
		            {
			            if (allElectriciansDone)
			            {
				            return ServiceOrderStatus.Ferdig;
			            }
			            return ServiceOrderStatus.Hos_Elektriker;
		            }

		            if (allMechanicsDone)
		            {
			            if (allHydraulicsDone)
			            {
				            return ServiceOrderStatus.Hos_Elektriker;
			            }
			            return ServiceOrderStatus.Hos_Hydraulikk;
		            }

		            if (!allMechanicsDone)
		            {
			            return ServiceOrderStatus.Hos_Mekaniker;
		            }

		            if (allHydraulicsDone)
		            {
			            return ServiceOrderStatus.Hos_Elektriker;
		            }

		            return ServiceOrderStatus.Under_behandling;
	            }
            }



    }
	
}

