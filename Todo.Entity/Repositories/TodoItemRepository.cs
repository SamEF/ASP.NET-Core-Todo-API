using Heimwinz.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Entity.Entities;
using Todo.Entity.Repositories.Interfaces;

namespace Todo.Entity.Repositories;

/// <summary>
/// A class representation of a <see cref="TodoItemRepository"/> object.
/// </summary>
public class TodoItemRepository : ITodoItemRepository
{
    private readonly ILogger<TodoItemRepository> logger;
    private readonly AppDbContext context;

    /// <summary>
    /// Instantiates a new <see cref="TodoItemRepository"/> object.
    /// </summary>
    /// <param name="logger">The logger service.</param>
    /// <param name="context">The repository service.</param>
    public TodoItemRepository(
        ILogger<TodoItemRepository> logger,
        AppDbContext context)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Retrieves all to-do items.
    /// </summary>
    /// <returns>A list of <see cref="TodoItem"/> objects.</returns>
    public async Task<List<TodoItem>> RetrieveAllTodoItemsAsync()
    {
        try
        {
            List<TodoItem> result = await this.context.TodoItems
            .AsNoTracking()
            .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while retrieving to-do items from repository.");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a to-do item by id.
    /// </summary>
    /// <param name="id">The to-do item identifier.</param>
    /// <returns>A <see cref="TodoItem"/> object; else null.</returns>
    public async Task<TodoItem?> RetrieveTodoItemByIdOrNullAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0);

        try
        {
            TodoItem? result = await this.context.TodoItems
                .Where(x => x.Id == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (result is null)
            {
                this.logger.LogWarning("No to-do items with Id {id} found in repository.", id);
                return null;
            }

            return result;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while retrieving to-do item Id {id} from repository.", id);
            throw;
        }
    }

    /// <summary>
    /// Inserts a new to-do item to the database.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    public async Task CreateTodoItemAsync(TodoItem entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        if (entity.Id != 0)
        {
            // A defensive measure to make sure that the entity we
            // are adding to the database is not an existing one.
            throw new InvalidOperationException("The entity appears to already exist in the database.");
        }

        try
        {
            entity.CreatedDate = DateTime.UtcNow;

            await this.context.TodoItems.AddAsync(entity);
            await this.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while trying to insert a new to-do item.");
            throw;
        }
    }

    /// <summary>
    /// Updates existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public async Task UpdateTodoItemAsync(TodoItem entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            if (entity.IsCompleted is true)
            {
                // To make sure the completion date of the
                // to-do item is set right before we update
                // the repository.
                entity.CompletedDate = DateTime.UtcNow;
            }

            context.TodoItems.Update(entity);
            await this.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while trying to update to-do item Id {id}.", entity.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes existing entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public async Task DeleteTodoItemAsync(TodoItem entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            this.context.TodoItems.Remove(entity);
            await this.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while trying to delete to-do item Id {id}.", entity.Id);
            throw;
        }
    }

    /// <summary>
    /// Saves changes asynchronously to the database.
    /// </summary>
    private async Task SaveChangesAsync()
    {
        await this.context.SaveChangesAsync();
    }
}
