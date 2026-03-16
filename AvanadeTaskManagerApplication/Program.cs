using AvanadeTaskManagerApplication.Controllers;
using AvanadeTaskManagerApplication.Extensions;
using AvanadeTaskManagerApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
   .MapIdentityUserEndpoints();

app.Run();

public class UserRegistrationModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }

}

public class UserLoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
