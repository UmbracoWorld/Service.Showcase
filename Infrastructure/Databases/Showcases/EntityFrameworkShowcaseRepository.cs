namespace Service.Showcase.Infrastructure.Databases.Showcases;

using Application.Showcase;
using Application.Showcase.Entities;
using AutoMapper;
using Extensions;
using Microsoft.EntityFrameworkCore;
using SimpleDateTimeProvider;

internal class EntityFrameworkShowcaseRepository : IShowcaseRepository
{
    private readonly ShowcaseDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public EntityFrameworkShowcaseRepository(ShowcaseDbContext context, IDateTimeProvider dateTimeProvider,
        IMapper mapper, IHostEnvironment environment)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;

        // if (!environment.IsDevelopment()) 
        //     return;
        //
        // _ = _context.Database.EnsureCreated();
        // _ = _context.AddData();

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