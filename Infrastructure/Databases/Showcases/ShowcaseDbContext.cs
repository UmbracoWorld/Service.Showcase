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
        
        // // Create the join between showcase + feature
        // modelBuilder.Entity<ShowcaseFeature>()
        //     .HasKey(sf => new { sf.FeatureId, sf.ShowcaseId });
        //
        // modelBuilder.Entity<ShowcaseFeature>()
        //     .HasOne(sf => sf.Showcase)
        //     .WithMany(sf => sf.Features)
        //     .HasForeignKey(sf => sf.ShowcaseId);
        //
        // modelBuilder.Entity<ShowcaseFeature>()
        //     .HasOne(sf => sf.Feature)
        //     .WithMany(sf => sf.ShowcaseFeatures)
        //     .HasForeignKey(sf => sf.FeatureId);

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
