using Todo.Entity.Entities;

namespace Todo.Entity.Repositories.Interfaces;

/// <summary>
/// An interface for the <see cref="TodoItemRepository"/> class.
/// </summary>
public interface ITodoItemRepository
{
    /// <summary>
    /// Retrieves all to-do items.
    /// </summary>
    /// <returns>A list of <see cref="TodoItem"/> objects.</returns>
    public Task<List<TodoItem>> RetrieveAllTodoItemsAsync();

    /// <summary>
    /// Retrieves a to-do item by id.
    /// </summary>
    /// <param name="id">The to-do item identifier.</param>
    /// <returns>A <see cref="TodoItem"/> object; else null.</returns>
    public Task<TodoItem?> RetrieveTodoItemByIdOrNullAsync(int id);

    /// <summary>
    /// Inserts a new to-do item to the database.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    public Task CreateTodoItemAsync(TodoItem entity);

    /// <summary>
    /// Updates existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public Task UpdateTodoItemAsync(TodoItem entity);

    /// <summary>
    /// Deletes existing entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public Task DeleteTodoItemAsync(TodoItem entity);
}
