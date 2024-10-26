using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Todo.Logic.Models;

/// <summary>
/// A class representation of a <see cref="TodoItemUpdateRequest"/> object.
/// </summary>
public class TodoItemUpdateRequest
{
    /// <summary>
    /// Instantiates a new <see cref="TodoItemUpdateRequest"/> object.
    /// </summary>
    public TodoItemUpdateRequest()
    {

    }

    /// <summary>
    /// The to-do item identifier.
    /// </summary>
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }

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

    /// <summary>
    /// The to-do item completion status.
    /// </summary>
    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }
}
