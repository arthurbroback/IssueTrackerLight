using System.ComponentModel.DataAnnotations;

namespace IssueTrackerLight.Dtos;

public sealed class CreateIssueDto
{
    [Required]
    [StringLength(60)]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = "";
}
