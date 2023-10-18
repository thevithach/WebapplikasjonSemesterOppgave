using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;
using WebapplikasjonSemesterOppgave.Models;

namespace WebapplikasjonSemesterOppgave.Areas.Identity.Data;

public class DBContextSample : IdentityDbContext<SampleUser>
{
    public DBContextSample(DbContextOptions<DBContextSample> options)
        : base(options)
    {
    }
    public DbSet<OrderEntity> OrderEntity { get; set; }
    public DbSet<ServiceChecklistEntity> ChecklistItems { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.Entity<SampleUser>().HasKey(u => u.Id);
        builder.Entity<SampleUser>()
            .HasMany<OrderEntity> (user => user.Order) // Assuming you have a navigation property named "Order" in SampleUser
            .WithOne(order => order.User) // Assuming you have a navigation property named "User" in OrderEntity
            .HasForeignKey(order => order.UserId); // The foreign key relationship

        builder.Entity<OrderEntity>().HasKey(order => order.Id);
        builder.Entity<ServiceChecklistEntity>()
            .HasOne(c => c.Order)
            .WithMany(o => o.ChecklistItems)
            .HasForeignKey(c => c.OrderId);

    }

}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<SampleUser>
{
    public void Configure(EntityTypeBuilder<SampleUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(100);

    }
}