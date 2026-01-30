using System.ComponentModel.DataAnnotations;
using IssueTrackerLight.Models;

namespace IssueTrackerLight.Dtos;

public sealed class UpdateIssueStatusDto
{
    [Required]
    public IssueStatus Status { get; set; }
}
