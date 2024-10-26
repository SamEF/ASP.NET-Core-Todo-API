using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Todo.Logic.Models;

public class TodoItemModel
{
    public TodoItemModel()
    {
        
    }

    /// <summary>
    /// The to-do item identifier.
    /// </summary>
    [Key]
    [Required]
    [JsonPropertyName("id")]
    public int? Id { get; set; }

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
    public bool? IsCompleted { get; set; }

    /// <summary>
    /// The to-do item creation date.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// The to-do item due date.
    /// </summary>
    [Required]
    [JsonPropertyName("dueDate")]
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The to-do item completed date.
    /// </summary>
    [JsonPropertyName("completedDate")]
    public DateTime CompletedDate { get; set; }
}
