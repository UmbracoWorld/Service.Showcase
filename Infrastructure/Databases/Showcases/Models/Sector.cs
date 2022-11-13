namespace Service.Showcase.Infrastructure.Databases.Showcases.Models;

public record Sector : Entity
{
    public string Value { get; set; }
    public ICollection<Showcase> Showcases { get; set; } = new HashSet<Showcase>();
}