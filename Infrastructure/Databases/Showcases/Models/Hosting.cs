namespace Service.Showcase.Infrastructure.Databases.Showcases.Models;

public record Hosting : Entity
{
    public string Value { get; set; }
    public ICollection<Showcase> Showcases { get; set; } = new HashSet<Showcase>();
}