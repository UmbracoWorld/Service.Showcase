namespace Service.Showcase.Infrastructure.Databases.Showcases.Models;

public record Feature : Entity
{
    public string Value { get; set; }
    public ICollection<Showcase> Showcases { get; set; } = new HashSet<Showcase>();
}
