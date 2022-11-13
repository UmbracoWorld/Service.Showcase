using Service.Showcase.Application.Showcase.Commands.CreateShowcase;

namespace Service.Showcase.Application.Showcase;

using System.Threading.Tasks;
using Entities;

public interface IShowcaseRepository
{
    Task<List<Showcase>> GetShowcases(CancellationToken cancellationToken);
    Task<Showcase> GetShowcaseById(int id, CancellationToken cancellationToken);
    Task<bool> ShowcaseExists(int id, CancellationToken cancellationToken);
    Task<Showcase> CreateShowcase(CreateShowcaseCommand showcase, CancellationToken cancellationToken);
}