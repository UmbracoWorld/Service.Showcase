namespace Service.Showcase.Application.Common.Entities;

public abstract record Entity
{
    public string Id { get; init; }
    public DateTime DateCreated { get; init; }
    public DateTime DateModified { get; init; }
}