using System;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;

namespace WebapplikasjonSemesterOppgave.Models
{
	public class ServiceOrderView
	{
		public SampleUser User { get; set; }
		public OrderEntity ServiceOrder { get; set; }
	}
}

