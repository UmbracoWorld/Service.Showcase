using System.ComponentModel.DataAnnotations;

namespace Service.Showcase.Presentation.Endpoints.Requests;

public class CreateShowcaseRequest
{
    [Required] public string Title { get; set; }
    [Required] public string Summary { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int MajorVersion { get; set; }
    [Required] public int MinorVersion { get; set; }
    [Required] public int PatchVersion { get; set; }
    [Required] public Guid AuthorId { get; set; }
    public IEnumerable<string> Features { get; set; }
    public ICollection<string> Sectors { get; set; }
    public ICollection<string> Hostings { get; set; }
    public string ImageSource { get; set; }
}