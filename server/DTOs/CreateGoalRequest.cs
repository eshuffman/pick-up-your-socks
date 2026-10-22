
using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class CreateGoalRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
}
