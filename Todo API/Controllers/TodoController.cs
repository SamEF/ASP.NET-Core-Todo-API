using Microsoft.AspNetCore.Mvc;
using Todo.Logic.Models;
using Todo.Logic.Services.Interfaces;

namespace Todo_API.Controllers;

/// <summary>
/// A class representation of a <see cref="TodoController"/> class.
/// </summary>
[Controller]
[Route("/v1/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> logger;
    private readonly ITodoItemService todoItemService;

    /// <summary>
    /// Instantiates a new <see cref="TodoController"/> object.
    /// </summary>
    /// <param name="logger">The logger service.</param>
    /// <param name="todoItemService">The to-do item service.</param>
    public TodoController(
        ILogger<TodoController> logger,
        ITodoItemService todoItemService)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.todoItemService = todoItemService ?? throw new ArgumentNullException(nameof(todoItemService));
    }

    /// <summary>
    /// This endpoint returns all the existing to-do items.
    /// </summary>
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> RetrieveAllTodoItemsAsync()
    {
        try
        {
            List<TodoItemModel> result = await this.todoItemService.RetrieveAllTodoItemsAsync();
            if (result.Count is 0)
            {
                return this.NoContent();
            }

            return this.Ok(result);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An unexpected error occurred while trying to fetch all to-do items.");
            return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// This endpoint retrieves a to-do item based on its identifier.
    /// </summary>
    /// <param name="id">The to-do item identifier.</param>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> RetrieveTodoItemById(int id)
    {
        if (id <= 0)
        {
            return this.BadRequest("Id must be greater than zero.");
        }

        try
        {
            TodoItemModel? result = await this.todoItemService.RetrieveTodoItemByIdOrNullAsync(id);
            if (result is null)
            {
                return this.NotFound($"To-do item with Id {id} not found.");
            }

            return this.Ok(result);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An unexpected error occurred while trying to fetch to-do item with Id {id}.", id);
            return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// This endpoint creates a new to-do item from a user request.
    /// </summary>
    /// <param name="request">The request from which to create a new to-do item.</param>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateTodoItemAsync([FromBody] TodoItemCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            bool isSuccessful = await this.todoItemService.CreateTodoItemAsync(request);
            if (!isSuccessful)
            {
                return this.BadRequest($"Unable to create new to-do item from request {nameof(request)}.");
            }

            return this.Ok();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An unexpected error occurred while trying to create a new to-do item.");
            return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// This endpoint updates an existing to-do item based on a user request.
    /// </summary>
    /// <param name="request">The request from which to update the to-do item.</param>
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateTodoItemAsync([FromBody] TodoItemUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            bool isSuccessful = await this.todoItemService.UpdateTodoItemAsync(request);
            if (!isSuccessful)
            {
                return this.NotFound($"To-do item with Id {request.Id} was not found.");
            }

            return this.Ok();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An unexpected error occurred while trying to update to-do item with Id {id}.", request.Id);
            return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// This endpoint deletes an existing to-do item based on its identifier.
    /// </summary>
    /// <param name="id">The to-do item identifier.</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTodoItemAsync(int id)
    {
        if (id <= 0)
        {
            return this.BadRequest("Id must be greater than zero.");
        }

        try
        {
            bool isSuccessful = await this.todoItemService.DeleteTodoItemByIdAsync(id);
            if (!isSuccessful)
            {
                return this.NotFound($"To-do item with Id {id} was not found.");
            }

            return this.Ok();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An unexpected error occurred while trying to delete to-do item with Id {id}.", id);
            return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}
