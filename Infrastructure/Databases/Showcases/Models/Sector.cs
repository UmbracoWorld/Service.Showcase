namespace Service.Showcase.Infrastructure.Databases.Showcases.Models;

internal record Sector : Entity
{
    public string Value { get; set; }
    public Showcase Showcase { get; set; }
}