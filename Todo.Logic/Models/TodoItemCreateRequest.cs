using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Todo.Logic.Models;

/// <summary>
/// A class representation of a <see cref="TodoItemCreateRequest"/> object.
/// </summary>
public class TodoItemCreateRequest
{
    /// <summary>
    /// Instantiates a new <see cref="TodoItemCreateRequest"/> object.
    /// </summary>
    public TodoItemCreateRequest()
    {

    }

    /// <summary>
    /// The to-do item due date.
    /// </summary>
    [Required]
    [JsonPropertyName("dueDate")]
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The to-do item title.
    /// </summary>
    [Required]
    [JsonPropertyName("title")]
    [MinLength(3), MaxLength(100)]
    public string Title { get; set; }
}
