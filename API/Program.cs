using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContextPool<ApplicationDbContext<User>>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetValue<string>("Database:ConnectionString"),
        x => { x.MigrationsAssembly(typeof(ApplicationDbContext<User>).Assembly.FullName); });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
