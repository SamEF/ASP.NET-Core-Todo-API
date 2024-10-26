using Microsoft.AspNetCore.Mvc;

namespace Todo_API.Controllers;

/// <summary>
/// A class representation of a <see cref="TodoController"/> class.
/// </summary>
[Controller]
[Route("/v1/todo")]
public class TodoController : ControllerBase
{
    /// <summary>
    /// Instantiates a new <see cref="TodoController"/> object.
    /// </summary>
    /// <param name="logger">The logger service.</param>
    public TodoController(
        ILogger<TodoController> logger)
    {
        
    }
}
