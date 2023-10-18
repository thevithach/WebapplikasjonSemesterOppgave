using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebapplikasjonSemesterOppgave.Models;

namespace WebapplikasjonSemesterOppgave.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SampleUser class
public class SampleUser : IdentityUser
{
    [Key] 
    public override string Id { get; set; }
    public String FirstName {  get; set; }
    public String LastName { get; set; }
    public String Address { get; set; }
    public ICollection<OrderEntity> Order { get; set; }


}

