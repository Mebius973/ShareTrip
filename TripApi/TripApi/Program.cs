using Microsoft.EntityFrameworkCore;
using System;
using TripApi.Data;
using TripApi.Repositories;
using TripApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICommentariesRepository, CommentariesRepository>();
builder.Services.AddScoped<CommentariesService>();
builder.Services.AddScoped<IPicturesRepository, PicturesRepository>();
builder.Services.AddScoped<PicturesService>();
builder.Services.AddScoped<ITripsRepository, TripsRepository>();
builder.Services.AddScoped<TripsService>();

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
