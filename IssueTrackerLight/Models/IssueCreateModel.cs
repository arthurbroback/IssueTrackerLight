using System.ComponentModel.DataAnnotations;

namespace IssueTrackerLight.Models;

public sealed class IssueCreateModel
{
    [Required(ErrorMessage = "Titel krävs.")]
    [StringLength(60)]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Beskrivning krävs.")]
    [StringLength(500)]
    public string Description { get; set; } = "";
}
