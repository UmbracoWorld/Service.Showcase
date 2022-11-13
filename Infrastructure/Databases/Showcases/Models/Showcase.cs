namespace Service.Showcase.Infrastructure.Databases.Showcases.Models;

public record Showcase : Entity
{
    public string Title { get; set; }
    
    public string Summary { get; set; }
    
    public string Description { get; set; }

    public int MajorVersion { get; set; }
    
    public int MinorVersion { get; set; }
    
    public int PatchVersion { get; set; }

    public ICollection<Feature> Features { get; set; } = new HashSet<Feature>();

    // public ICollection<Sector> Sectors { get; set; }
    //
    // public ICollection<Hosting> Hostings { get; set; }
    
    public Guid AuthorId { get; set; }
    
    public string? ImageSource { get; set; }
}