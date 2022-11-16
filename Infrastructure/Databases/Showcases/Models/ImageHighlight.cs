namespace Service.Showcase.Infrastructure.Databases.Showcases.Models;

public record ImageHighlight : Entity
{
    public string Source { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int ShowcaseId { get; set; }
    public Showcase Showcase { get; set; }
}