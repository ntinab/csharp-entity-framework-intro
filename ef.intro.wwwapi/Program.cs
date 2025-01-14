using ef.intro.wwwapi.Context;
using ef.intro.wwwapi.Data;
using ef.intro.wwwapi.EndPoint;
using ef.intro.wwwapi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
builder.Services.AddDbContext<LibraryContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ConfigureBookApi();
app.ConfigureAuthorApi();
app.ConfigurePublisherApi();

app.Seed();

app.Run();
