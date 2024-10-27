using Heimwinz.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Todo.Entity.Repositories;
using Todo.Entity.Repositories.Interfaces;
using Todo.Logic.Services;
using Todo.Logic.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Connections
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

// Add to-do service and repository classes.
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
