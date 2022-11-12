namespace Service.Showcase.Infrastructure.Databases.Showcases;

using Application.Showcase;
using Application.Showcase.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

internal class EntityFrameworkShowcaseRepository : IShowcaseRepository
{
    private readonly ShowcaseDbContext _context;
    private readonly IMapper _mapper;

    public EntityFrameworkShowcaseRepository(ShowcaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public virtual async Task<List<Showcase>> GetShowcases(CancellationToken cancellationToken)
    {
        var authors = await _context.Showcases
            .Include(x => x.Features)
            .Include(x => x.Hostings)
            .Include(x => x.Sectors)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<Showcase>>(authors);
    }

    public virtual async Task<Showcase> GetShowcaseById(Guid id, CancellationToken cancellationToken)
    {
        var author = await _context.Showcases
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<Showcase>(author);
    }

    public virtual async Task<bool> ShowcaseExists(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Showcases.AsNoTracking().AnyAsync(a => a.Id == id, cancellationToken);
    }
}