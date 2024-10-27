using Todo.Logic.Models;

namespace Todo.Logic.Services.Interfaces;

/// <summary>
/// An interface for the <see cref="TodoItemService"/> class.
/// </summary>
public interface ITodoItemService
{
    /// <summary>
    /// Retrieves all existing to-do items from the repository.
    /// </summary>
    /// <returns>A list of <see cref="TodoItemModel"/> objects.</returns>
    public Task<List<TodoItemModel>> RetrieveAllTodoItemsAsync();

    /// <summary>
    /// Retrieves to-do item based on its identifier.
    /// </summary>
    /// <param name="id">The to-do item identifier.</param>
    /// <returns>A <see cref="TodoItemModel"/> object; else null.</returns>
    public Task<TodoItemModel?> RetrieveTodoItemByIdOrNullAsync(int id);

    /// <summary>
    /// Creates a new to-do item based on a user request.
    /// </summary>
    /// <param name="request">The request to create a new to-do item.</param>
    /// <returns>True if successful; else false.</returns>
    public Task<bool> CreateTodoItemAsync(TodoItemCreateRequest request);

    /// <summary>
    /// Updates an existing to-do item with a user update request.
    /// </summary>
    /// <param name="request">The request to update an existing to-do item.</param>
    /// <returns>True if successful; else false.</returns>
    public Task<bool> UpdateTodoItemAsync(TodoItemUpdateRequest request);

    /// <summary>
    /// Deletes an existing to-do item based on its identifier.
    /// </summary>
    /// <param name="id">The identifier of the to-do item to delete.</param>
    /// <returns>True if successful; else false.</returns>
    public Task<bool> DeleteTodoItemByIdAsync(int id);
}
