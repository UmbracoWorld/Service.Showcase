using Algolia.Search.Utils;
using Entity = Service.Showcase.Application.Common.Entities.Entity;

namespace Service.Showcase.Application.Showcase.Entities;

public record Showcase : Entity
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string PublicUrl { get; set; }

    public int MajorVersion { get; set; }

    public int MinorVersion { get; set; }

    public int PatchVersion { get; set; }

    public IEnumerable<string> Features { get; set; }

    public ICollection<string> Sectors { get; set; }

    public ICollection<string> Hostings { get; set; }

    public Guid AuthorId { get; set; }

    public string ImageSource { get; set; }

    public IEnumerable<ImageHighlight> ImageHighlights { get; set; }
    
    // Used for indexing in Algolia.
    public string ObjectID => Id;
    public long Date => DateCreated.ToUnixTimeSeconds();
}

public class ImageHighlight
{
    public string Source { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}