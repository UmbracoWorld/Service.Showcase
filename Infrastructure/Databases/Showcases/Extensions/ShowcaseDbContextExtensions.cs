namespace Service.Showcase.Infrastructure.Databases.Showcases.Extensions;

using System;
using Bogus;
using Models;

internal static class ShowcaseDbContextExtensions
{
    public static ShowcaseDbContext AddData(this ShowcaseDbContext context)
    {

        var showcases = new Faker<Showcase>()
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.Title, f => f.Company.CompanyName())
            .RuleFor(a => a.Summary, f => f.Company.Bs())
            .RuleFor(a => a.Description, f => f.Commerce.ProductDescription())
            .RuleFor(a => a.AuthorId, Guid.NewGuid())
            .RuleFor(a => a.DateCreated, f => f.Date.Past())
            .RuleFor(a => a.DateModified, f => f.Date.Past())
            .RuleFor(a => a.MajorVersion, f => f.Random.Int(4, 10))
            .RuleFor(a => a.MinorVersion, f => f.Random.Int(1, 10))
            .RuleFor(a => a.PatchVersion, f => f.Random.Int(1, 10))
            .Generate(5);

        foreach (var showcase in showcases)
        {
            var features = GenerateFeature(showcase);
            var hosting = GenerateHosting(showcase);
            var sectors = GenerateSector(showcase);
        
            // showcase.Features = features;
            // showcase.Hostings = hosting;
            // showcase.Sectors = sectors;
            
            context.AddRange(hosting);
            context.AddRange(features);
            context.AddRange(sectors);
            
            context.Showcases.Add(showcase);
            context.SaveChanges();
            
        }

        return context;
    }
    
    private static List<Sector> GenerateSector(Showcase showcase)
    {
        var hostingList = new[]
            { "leisure", "hobby", "tourism", "health", "b2b" };
        var hosting = new Faker<Sector>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Value, f => f.PickRandom(hostingList));

        return hosting.Generate(2);
    }

    private static List<Hosting> GenerateHosting(Showcase showcase)
    {
        var hostingList = new[]
            { "azure", "aws", "gcloud", "cloud", "umbhost" };
        var hosting = new Faker<Hosting>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Value, f => f.PickRandom(hostingList));

        return hosting.Generate(2);
    }
    private static List<Feature> GenerateFeature(Showcase showcase)
    {
        var featuresList = new[]
            { "accessibility", "design", "ecommerce", "editor experience", "integrations", "headless" };
        var features = new Faker<Feature>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Value, f => f.PickRandom(featuresList));
        
        return features.Generate(2);
    }
}
