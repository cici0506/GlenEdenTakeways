using GlenEdenTakeways.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GlenEdenTakeways.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlenEdenTakeways.Areas.Identity.Data;

public class IdentityContext : IdentityDbContext<GlenEdenTakewaysUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserrEntityConfiguration());
    }

   

    public DbSet<GlenEdenTakeways.Models.Customer> Customer { get; set; } = default!;

    public DbSet<GlenEdenTakeways.Models.Employee> Employee { get; set; } = default!;

    public DbSet<GlenEdenTakeways.Models.Menu> Menu { get; set; } = default!;

    public DbSet<GlenEdenTakeways.Models.Order> Order { get; set; } = default!;

    public DbSet<GlenEdenTakeways.Models.Payment> Payment { get; set; } = default!;

    public DbSet<GlenEdenTakeways.Models.PaymentType> PaymentType { get; set; } = default!;

}

public class ApplicationUserrEntityConfiguration : IEntityTypeConfiguration<GlenEdenTakewaysUser>
{
    public void Configure(EntityTypeBuilder<GlenEdenTakewaysUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);

        builder.Property(u => u.LastName).HasMaxLength(255);
    }


}