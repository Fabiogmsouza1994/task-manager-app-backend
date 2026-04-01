using TaskManagerApplication.Controllers;
using TaskManagerApplication.Extensions;
using TaskManagerApplication.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerExplorer()
                .ConfigureIdentityOptions()
                .InjectTaskManagerDbContext(builder.Configuration)
                .InjectUserDbContext(builder.Configuration)
                .AddIdentityHandlersAndStores()
                .AddIdentityAuth(builder.Configuration);

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

app.ConfigureSwaggerExplorer()
   .ConfigureCORS(builder.Configuration)
   .AddIdentityAuthMiddlewares();

app.UseHttpsRedirection();

app.MapControllers();

app
   .MapGroup("/api")
   .MapIdentityApi<AppUser>()
   .WithTags("Auth");

app.MapGroup("/api")
   .MapIdentityUserEndpoints()
   .MapAccountEndpoints();

app.Run();