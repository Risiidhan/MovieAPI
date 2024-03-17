using Interfaces;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Interfaces;
using MovieAPI.Repositores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var movieConnectionString = builder.Configuration.GetConnectionString("MovieDefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(movieConnectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMovie,MovieRepository>();
builder.Services.AddScoped<IActor,ActorRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

