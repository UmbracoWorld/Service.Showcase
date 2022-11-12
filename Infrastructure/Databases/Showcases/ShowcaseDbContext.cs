namespace Service.Showcase.Infrastructure.Databases.Showcases;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

internal class ShowcaseDbContext : DbContext
{
    public ShowcaseDbContext(DbContextOptions<ShowcaseDbContext> options) : base(options)
    {
    }

    public DbSet<Showcase> Showcases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1400;Database=UmbracoDb;User Id=sa;Password=ComplexPassword123!;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Showcase>()
            .HasMany(pr => pr.Features)
            .WithOne(x => x.Showcase);
        
        modelBuilder.Entity<Showcase>()
            .HasMany(pr => pr.Sectors)
            .WithOne(x => x.Showcase);
        
        modelBuilder.Entity<Showcase>()
            .HasMany(pr => pr.Hostings)
            .WithOne(x => x.Showcase);

        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
