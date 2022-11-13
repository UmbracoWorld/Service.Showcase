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
    public DbSet<Sector> Sector { get; set; }
    public DbSet<Hosting> Hosting { get; set; }
    public DbSet<Feature> Feature { get; set; }
        
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
        
        modelBuilder.Entity<Feature>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        modelBuilder.Entity<Sector>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        modelBuilder.Entity<Hosting>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
