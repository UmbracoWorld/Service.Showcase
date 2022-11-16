using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Showcase.Application.Showcase.Commands.CreateShowcase;
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
        var showcases = await _context.Showcases
            .Include(x => x.Features)
            .Include(x => x.Hostings)
            .Include(x => x.Sectors)
            .Include(x => x.ImageHighlights)
            .OrderByDescending(x => x.DateCreated)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<Showcase>>(showcases);
    }

    public virtual async Task<Showcase> GetShowcaseById(int id, CancellationToken cancellationToken)
    {
        var author = await _context.Showcases
            .Where(r => r.Id == id)
            .Include(x => x.Features)
            .Include(x => x.Hostings)
            .Include(x => x.Sectors)
            .Include(x => x.ImageHighlights)
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<Showcase>(author);
    }

    public virtual async Task<bool> ShowcaseExists(int id, CancellationToken cancellationToken)
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
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
            ImageHighlights = CreateImageHighlights(showcase),
            PublicUrl = showcase.PublicUrl
        };

        var entityEntry = await _context.AddAsync(entityShowcase, cancellationToken);

        CreateOrAddFeature(showcase, entityEntry, entityShowcase);
        CreateOrAddSector(showcase, entityEntry, entityShowcase);
        CreateOrAddHosting(showcase, entityEntry, entityShowcase);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Showcase>(entityEntry.Entity);
    }

    private static List<Models.ImageHighlight> CreateImageHighlights(CreateShowcaseCommand showcase)
    {
        return showcase.ImageHighlights.Select(x => new Models.ImageHighlight()
        {
            Title = x.Title, 
            Description = x.Description, 
            Source = x.Source
        }).ToList();
    }

    private void CreateOrAddFeature(CreateShowcaseCommand showcase, EntityEntry<Models.Showcase> entityEntry,
        Models.Showcase entityShowcase)
    {
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
    }

    private void CreateOrAddSector(CreateShowcaseCommand showcase, EntityEntry<Models.Showcase> entityEntry,
        Models.Showcase entityShowcase)
    {
        foreach (var sector in showcase.Sectors)
        {
            var exists = _context.Sector.FirstOrDefault(x => x.Value == sector);
            if (exists is not null)
            {
                entityEntry.Entity.Sectors.Add(exists);
            }
            else
            {
                var newSector = new Sector { Value = sector };
                newSector.Showcases.Add(entityShowcase);
                _context.Sector.Add(newSector);
            }
        }
    }

    private void CreateOrAddHosting(CreateShowcaseCommand showcase, EntityEntry<Models.Showcase> entityEntry,
        Models.Showcase entityShowcase)
    {
        foreach (var hosting in showcase.Hostings)
        {
            var exists = _context.Hosting.FirstOrDefault(x => x.Value == hosting);
            if (exists is not null)
            {
                entityEntry.Entity.Hostings.Add(exists);
            }
            else
            {
                var newHosting = new Hosting { Value = hosting };
                newHosting.Showcases.Add(entityShowcase);
                _context.Hosting.Add(newHosting);
            }
        }
    }
}