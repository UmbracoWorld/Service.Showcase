using AutoMapper.Features;
using Service.Showcase.Application.Common.Entities;

namespace Service.Showcase.Application.Showcase.Entities;

public record Showcase : Entity
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    
    public int MajorVersion { get; set; }
    
    public int MinorVersion { get; set; }
    
    public int PatchVersion { get; set; }
    
    public IEnumerable<string> Features { get; set; }
    
    public ICollection<string> Sectors { get; set; }
    
    public ICollection<string> Hostings { get; set; }
    
    public Guid AuthorId { get; set; }
}

