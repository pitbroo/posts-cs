using Microsoft.EntityFrameworkCore;
using posts_cs.repository;
using posts_cs.service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddScoped<IPostService, PostService>();
services.AddScoped<IPostRepository, PostRepository>(); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Konfiguracja opcji DbContext, np. connectionString
    options.UseSqlServer("Host=localhost;Database=posts;Username=hexagonal;Password=hexagonal;Schema=public");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
