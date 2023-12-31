﻿using System.Reflection.Emit;
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
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        // Configure the primary key for SampleUser entity
        builder.Entity<SampleUser>().HasKey(u => u.Id);
        // Define a one-to-many relationship between SampleUser and OrderEntity
        builder.Entity<SampleUser>()
            .HasMany<OrderEntity> (user => user.Order) 
            .WithOne(order => order.User) 
            .HasForeignKey(order => order.UserId); // The foreign key relationship
        
        // Configure the primary key for OrderEntity
        builder.Entity<OrderEntity>().HasKey(order => order.Id);
        // Define a relationship between ServiceChecklistEntity and OrderEntity
        builder.Entity<ServiceChecklistEntity>()
            .HasOne(c => c.Order)
            .WithMany(o => o.ChecklistItems) // Can be changed to WithOne if we want one-to-one, also need to change navigation in orderentity then.
            .HasForeignKey(c => c.OrderId);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
            }
        );
        var hasher = new PasswordHasher<IdentityUser>();
        // Seeding the User to AspNetUsers table
        builder.Entity<SampleUser>().HasData(
            new SampleUser()
            {
                Id= "8e445865-a24d-4543-a6c6-9443d048cdb9",
                FirstName = "Admin", 
                LastName = "Admin",
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com",
                Address = "Adminveien 1",
                NormalizedUserName = "Admin@gmail.com".ToUpper(),
                NormalizedEmail = "Admin@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Admin123*"),
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            }
        );
        // Seed the relation between user and role
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                RoleId= "2c5e174e-3b0e-446f-86af-483d56fd7210", // 2c5e174e-3b0e-446f-86af-483d56fd7210
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9" // 8e445865-a24d-4543-a6c6-9443d048cdb9
            }
        );
    }

}
/// <summary>
/// Represents the configuration for the ApplicationUserEntity.
/// </summary>
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<SampleUser>
{
    /// <summary>
    /// Configures the properties of the ApplicationUserEntity.
    /// </summary>
    /// <param name="builder">The entity type builder to use for configuration.</param>
    public void Configure(EntityTypeBuilder<SampleUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(100);

    }
}