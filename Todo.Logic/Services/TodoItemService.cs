using Microsoft.Extensions.Logging;
using Todo.Entity.Entities;
using Todo.Entity.Repositories.Interfaces;
using Todo.Logic.Models;
using Todo.Logic.Services.Interfaces;

namespace Todo.Logic.Services;

/// <summary>
/// A class representation of a <see cref="TodoItemService"/> object.
/// </summary>
public class TodoItemService : ITodoItemService
{
    private readonly ILogger<TodoItemService> logger;
    private readonly ITodoItemRepository repository;

    /// <summary>
    /// Instantiates a new <see cref="TodoItemService"/> object.
    /// </summary>
    /// <param name="logger">The logger service.</param>
    /// <param name="repository">The repository service.</param>
    public TodoItemService(
        ILogger<TodoItemService> logger,
        ITodoItemRepository repository)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Retrieves all existing to-do items from the repository.
    /// </summary>
    /// <returns>A list of <see cref="TodoItemModel"/> objects.</returns>
    public async Task<List<TodoItemModel>> RetrieveAllTodoItemsAsync()
    {
        try
        {
            List<TodoItem> entities = await this.repository.RetrieveAllTodoItemsAsync();
            if (entities.Count is 0)
            {
                this.logger.LogWarning("No to-do items found in repository.");
                return [];
            }

            List<TodoItemModel> result = [];
            foreach (TodoItem entity in entities)
            {
                result.Add(ToModel(entity));
            }

            return result;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while retrieving all to-do items.");
            throw;
        }
    }

    /// <summary>
    /// Retrieves to-do item based on its identifier.
    /// </summary>
    /// <param name="id">The to-do item identifier.</param>
    /// <returns>A <see cref="TodoItemModel"/> object; else null.</returns>
    public async Task<TodoItemModel?> RetrieveTodoItemByIdOrNullAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0);

        try
        {
            TodoItem? entity = await this.repository.RetrieveTodoItemByIdOrNullAsync(id);
            if (entity is null)
            {
                this.logger.LogError("To-do item Id {id} not found.", id);
                return null;
            }

            return ToModel(entity);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while retrieving to-do item with Id {id}.", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new to-do item based on a user request.
    /// </summary>
    /// <param name="request">The request to create a new to-do item.</param>
    /// <returns>True if successful; else false.</returns>
    public async Task<bool> CreateTodoItemAsync(TodoItemCreateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Title, nameof(request.Title));

        try
        {
            await this.repository.CreateTodoItemAsync(new TodoItem()
            {
                Title = request.Title,
                DueDate = request.DueDate,
                IsCompleted = false, // Set to-do item completed to false by default on creation
            });

            return true;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while creating new to-do item.");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing to-do item with a user update request.
    /// </summary>
    /// <param name="request">The request to update an existing to-do item.</param>
    /// <returns>True if successful; else false.</returns>
    public async Task<bool> UpdateTodoItemAsync(TodoItemUpdateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(request.Id, 0);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Title, nameof(request.Title));

        try
        {
            TodoItem? entity = await this.repository.RetrieveTodoItemByIdOrNullAsync(request.Id);
            if (entity is null)
            {
                this.logger.LogWarning("To-do item Id {id} not found.", request.Id);
                return false;
            }

            entity.Title = request.Title;
            entity.DueDate = request.DueDate;
            entity.IsCompleted = request.IsCompleted;

            await this.repository.UpdateTodoItemAsync(entity);

            return true;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while updating to-do item with Id {id}.", request.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes an existing to-do item based on its identifier.
    /// </summary>
    /// <param name="id">The identifier of the to-do item to delete.</param>
    /// <returns>True if successful; else false.</returns>
    public async Task<bool> DeleteTodoItemByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0);

        try
        {
            TodoItem? entity = await this.repository.RetrieveTodoItemByIdOrNullAsync(id);
            if (entity is null)
            {
                this.logger.LogWarning("To-do item Id {id} not found.", id);
                return false;
            }

            await this.repository.DeleteTodoItemAsync(entity);

            return true;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while deleting to-do item with Id {id}.", id);
            throw;
        }
    }

    /// <summary>
    /// A helper method to convert to-do item entities to their model equivalent.
    /// </summary>
    /// <param name="entity">The entity to convert.</param>
    /// <returns>A <see cref="TodoItemModel"/> object.</returns>
    private static TodoItemModel ToModel(TodoItem entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new TodoItemModel
        {
            Id = entity.Id,
            Title = entity.Title,
            DueDate = entity.DueDate,
            CreatedDate = entity.CreatedDate,
            IsCompleted = entity.IsCompleted,
            CompletedDate = entity.CompletedDate,
        };
    }
}
