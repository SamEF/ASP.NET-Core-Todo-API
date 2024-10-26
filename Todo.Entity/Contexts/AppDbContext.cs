using Microsoft.EntityFrameworkCore;
using Todo.Entity.Entities;

namespace Heimwinz.WebApp.Data;

/// <summary>
/// A class representation for defining the application context.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Instantiates a new instance of <see cref="AppDbContext"/>.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}

