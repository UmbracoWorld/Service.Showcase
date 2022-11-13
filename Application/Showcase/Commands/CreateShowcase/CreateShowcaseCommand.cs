using MediatR;

namespace Service.Showcase.Application.Showcase.Commands.CreateShowcase;

public class CreateShowcaseCommand : IRequest<Entities.Showcase>
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public int MajorVersion { get; set; }
    public int MinorVersion { get; set; }
    public int PatchVersion { get; set; }
    public Guid AuthorId { get; set; }
    public IEnumerable<string> Features { get; set; }
    public IEnumerable<string> Sectors { get; set; }
    public IEnumerable<string> Hostings { get; set; }
    public string ImageSource { get; set; }
}