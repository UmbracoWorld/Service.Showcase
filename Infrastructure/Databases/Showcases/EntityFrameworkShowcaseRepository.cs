using Service.Showcase.Application.Showcase.Commands.CreateShowcase;
using Service.Showcase.Application.Showcase.Queries.CreateShowcase;
using Service.Showcase.Infrastructure.Databases.Showcases.Models;

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
            // .Include(x => x.Hostings)
            // .Include(x => x.Sectors)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<Showcase>>(authors);
    }

    public virtual async Task<Showcase> GetShowcaseById(Guid id, CancellationToken cancellationToken)
    {
        var author = await _context.Showcases
            .Where(r => r.Id == id)
            .Include(x => x.Features)
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<Showcase>(author);
    }

    public virtual async Task<bool> ShowcaseExists(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Showcases.AsNoTracking().AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Showcase> CreateShowcase(CreateShowcaseCommand showcase, CancellationToken cancellationToken)
    {
        var entityShowcase = new Models.Showcase
        {
            AuthorId = showcase.AuthorId,
            Description = showcase.Description,
            Title = showcase.Title,
            Summary = showcase.Summary,
            ImageSource = showcase.ImageSource,
            MajorVersion = showcase.MajorVersion,
            MinorVersion = showcase.MinorVersion,
            PatchVersion = showcase.PatchVersion,
        };
        
        var entityEntry = await _context.AddAsync(entityShowcase, cancellationToken);

        foreach (var feature in showcase.Features)
        {
            var exists = _context.Feature.FirstOrDefault(x => x.Value == feature);
            if (exists is not null)
            {
                entityEntry.Entity.Features.Add(exists);
            }
            else
            {
                var newFeature = new Feature { Value = feature };
                newFeature.Showcases.Add(entityShowcase);
                _context.Feature.Add(newFeature);
            }
        }

        // after the loop above, enitityShowcase features is 2 (as it should be)
        // after adding, it's actually 4...

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Showcase>(entityEntry.Entity);
    }
}