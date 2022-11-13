using Service.Showcase.Presentation.Endpoints;
using Serilog;
using Service.Showcase.Infrastructure.Databases.Showcases;
using Service.Showcase.Infrastructure.Databases.Showcases.Extensions;
using Service.Showcase.Presentation.Extensions;


var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder();

var app = builder
    .Build()
    .ConfigureApplication();

// if (app.Environment.IsDevelopment())
// {
//     using (var scope = app.Services.CreateScope())
//     {
//         var services = scope.ServiceProvider;
//         services.GetService<ShowcaseDbContext>()?.AddData();
//     }
// }

_ = app.MapEndpoints();

try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}