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
		[Display(Name = "Under behandling")]
		Under_behandling,
		[Display(Name = "Hos Mekaniker")]
		Hos_Mekaniker,
		[Display(Name = "Hos Hydraulikk")]
		Hos_Hydraulikk,
		[Display(Name = "Hos Elektriker")]
		Hos_Elektriker,
		Ferdig
	}
	public class OrderEntity
	{
		
		
			[Key]
            public int Id { get; set; }
            [Display(Name = "Produkt type")]
            [StringLength(100)] 
            public string? ProductType { get; set; }
            [Display(Name = "Serienummer")]
            [StringLength(100)] 
            public string? SerialNumber { get; set; }
            [Display(Name = "Årsmodell")]
            [StringLength(50)] 
            public string? ModelYear { get; set; }
            [Display(Name = "Garanti")]
            public bool? Warranty { get; set; }
            [Display(Name = "Service eller reparasjon")]
            public bool? ServiceOrRepair { get; set; }
            [Display(Name = "Kundeavtale")]
            [StringLength(500)] 
            public string? CustomerAgreement { get; set; }
            [Display(Name = "Reparasjonsdetaljer")]
            [StringLength(500)] 
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
            [Required]
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
            /*
            If we want a one-to-one relationship between OrderEntity and ServiceChecklistEntity
            public ServiceChecklistEntity ChecklistItem { get; set; }
            */
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

