using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using posts_cs.Controllers;
using posts_cs.repository;
using posts_cs.service;
using posts_cs.Services;
using WebApi.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddScoped<IPostService, PostService>();
services.AddScoped<IPostRepository, PostRepository>();
/*services.AddScoped<IAuthService, AuthService>();*/
services.AddScoped<IJwtUtils, JwtUtils>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Konfiguracja opcji DbContext, np. connectionString
    options.UseSqlServer("Host=localhost;Database=posts;Username=hexagonal;Password=hexagonal;Schema=public");
});


services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SUPER TOP TOP SECRET!!!SUPER TOP TOP SECRET!!!SUPER TOP TOP SECRET!!!")),
        };
    });
services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

/*app.UseHttpsRedirection();*/
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    app.MapControllers();
}
app.MapControllers();

app.Run();
