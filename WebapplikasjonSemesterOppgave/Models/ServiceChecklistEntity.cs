using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebapplikasjonSemesterOppgave.Models;

public enum ChecklistItemCondition
{
        OK,
        Bør_Skiftes,
        Defekt
}

public class ServiceChecklistEntity
{
        [Key]
        public int Id { get; set; }
        //Mekaniker
        public ChecklistItemCondition? ClutchlamelerSlitasje { get; set; }
        public ChecklistItemCondition? Bremser { get; set; }
        public ChecklistItemCondition? LagerforTrommel { get; set; }
        public ChecklistItemCondition? PTOogOpplagring { get; set; }
        public ChecklistItemCondition? Kjedestrammer { get; set; }
        public ChecklistItemCondition? Wire { get; set; }
        public ChecklistItemCondition? PinionLager { get; set; }
        public ChecklistItemCondition? KilepåKjedehjul { get; set; }
        //Hydraulikk
        public ChecklistItemCondition? SylinderLekkasje { get; set; }
        public ChecklistItemCondition? SlangeSkadeLekkasje { get; set; }
        public ChecklistItemCondition? HydraulikkblokkTestbenk { get; set; }
        public ChecklistItemCondition? SkiftOljeiTank { get; set; }
        public ChecklistItemCondition? SkiftOljepåGirboks { get; set; }
        public ChecklistItemCondition? Ringsylinder { get; set; }
        public ChecklistItemCondition? Bremsesylinder { get; set; }
        //Elektriker
        public ChecklistItemCondition? LedningsnettpåVinsj{ get; set; }
        public ChecklistItemCondition? TestRadio { get; set; }
        public ChecklistItemCondition? Knappekasse { get; set; }
        //Trykksettinger
        public string? XxBar {get; set;}
        public string? VinsjKjørAlleFunksjoner { get; set; }
        public string? TrekkraftKN{ get; set; }
        public string? BremsekraftKN { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
    

}