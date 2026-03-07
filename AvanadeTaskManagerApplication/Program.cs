
using AvanadeTaskManagerApplication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TaskManagerTasksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x => 
                 {
                     x.DefaultAuthenticateScheme =
                     x.DefaultChallengeScheme =
                     x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                 }).AddJwtBearer(y =>
                 {
                     y.SaveToken = false;
                     y.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JWTSecret"]!))
                     };
                 });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
    options.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app
   .MapGroup("/api")
   .MapIdentityApi<AppUser>()
   .WithTags("Auth");

app.MapPost("/api/signup", async (
    UserManager<AppUser> userManager, 
    [FromBody] UserRegistrationModel userRegistrationModel
    ) => 
    {
        AppUser user = new AppUser()
        {
            UserName = userRegistrationModel.Email,
            Email = userRegistrationModel.Email,
            FullName = userRegistrationModel.FullName,
        };

        var result = await userManager.CreateAsync( 
            user, 
            userRegistrationModel.Password);

        if (result.Succeeded)
            return Results.Ok(result);
        else return Results.BadRequest(result);
    }).WithTags("Sign Up");

app.MapPost("/api/signin", async (
    UserManager<AppUser> userManager,
    [FromBody] UserLoginModel userLoginModel) =>
{
    var user = await userManager.FindByEmailAsync(userLoginModel.Email);
    if(user != null && await userManager.CheckPasswordAsync(user, userLoginModel.Password))
    {
      var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JWTSecret"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
         {
            new Claim("UserID",user.Id.ToString())
         }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        return Results.Ok(new { token });
    } else return Results.BadRequest(new {message = "Username or password is incorrect."});
});

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
