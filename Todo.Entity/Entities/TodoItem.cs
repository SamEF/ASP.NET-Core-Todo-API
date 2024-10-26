using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Todo.Entity.Entities;

/// <summary>
/// A class representation of a <see cref="TodoItem"/> object.
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Instantiates a new <see cref="TodoItem"/> object.
    /// </summary>
    public TodoItem()
    {

    }

    /// <summary>
    /// The to-do item identifier.
    /// </summary>
    [Key]
    [Required]
    public int? Id { get; set; }

    /// <summary>
    /// The to-do item title.
    /// </summary>
    [Required]
    [MinLength(3), MaxLength(100)]
    public string Title { get; set; }

    /// <summary>
    /// The to-do item completion status.
    /// </summary>
    public bool? IsCompleted { get; set; }

    /// <summary>
    /// The to-do item creation date.
    /// </summary>
    [Required]
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// The to-do item due date.
    /// </summary>
    [Required]
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The to-do item completed date.
    /// </summary>
    public DateTime CompletedDate { get; set; }
}
