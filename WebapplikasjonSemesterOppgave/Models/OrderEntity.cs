using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
            public int Id { get; set; }
            [Display(Name = "Produkt type")]
            public string? ProductType { get; set; }
            [Display(Name = "Serienummer")]
            public string? SerialNumber { get; set; }
            [Display(Name = "Årsmodell")]
            public string? ModelYear { get; set; }
            [Display(Name = "Garanti")]
            public bool? Warranty { get; set; }
            [Display(Name = "Service eller reparasjon")]
            public bool? ServiceOrRepair { get; set; }
            [Display(Name = "Kundeavtale")]
            public string? CustomerAgreement { get; set; }
            [Display(Name = "Reparasjonsdetaljer")]
            public string? ReparationDetails { get; set; }
            [Display(Name = "Timer arbeidet")]
            public string? WorkingHours { get; set; }
            [Display(Name = "Brukte deler returnert")]
            public string? ReplacedPartsReturned { get; set; }
            [Display(Name = "Fraktmetoder")]
            public string? ShippingMethods { get; set; }
            
            
            [Display(Name = "Dato opprettet ordre")]
            public DateTime OrderCreatedDate { get; set; }
            [Display(Name = "Ordre kunde")]
            public string OrderPlacerCustomer { get; set; }
            [Display(Name = "Dato mottatt produkt")]
            public DateTime? ProductReceivedDate { get; set; }
            [Display(Name = "Dato avtalt ferdigstillelse")]
			public DateTime? ProductAgreedCompletionDate { get; set; }
			
            public string UserId { get; set; }
            [ValidateNever]
            public SampleUser User { get; set; }
            [ValidateNever]
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

