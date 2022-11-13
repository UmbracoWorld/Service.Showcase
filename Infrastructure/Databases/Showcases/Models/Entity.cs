namespace Service.Showcase.Infrastructure.Databases.Showcases.Models;

public abstract record Entity
{
    public int Id { get; init; }
    public DateTime DateCreated { get; init; }
    public DateTime DateModified { get; set; }
}