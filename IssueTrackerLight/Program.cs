using IssueTrackerLight;
using IssueTrackerLight.Components;
using IssueTrackerLight.Dtos;
using IssueTrackerLight.Models;
using IssueTrackerLight.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// In-memory service delas mellan UI och API
builder.Services.AddSingleton<IssueService>();

var app = builder.Build();

// =======================
// Middleware
// =======================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// =======================
// Blazor / Razor
// =======================

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// =======================
// Minimal API – Issues
// =======================

app.MapGet("/api/issues", (IssueService service) =>
{
    return Results.Ok(service.GetAll());
});

app.MapGet("/api/issues/{id:int}", (int id, IssueService service) =>
{
    var issue = service.GetById(id);
    return issue is null
        ? Results.NotFound()
        : Results.Ok(issue);
});

app.MapPost("/api/issues", (CreateIssueDto dto, IssueService service) =>
{
    var errors = Validate(dto);
    if (errors is not null)
        return Results.BadRequest(errors);

    var created = service.Add(dto.Title, dto.Description);
    return Results.Created($"/api/issues/{created.Id}", created);
});

app.MapPatch("/api/issues/{id:int}/status", (int id, UpdateIssueStatusDto dto, IssueService service) =>
{
    var errors = Validate(dto);
    if (errors is not null)
        return Results.BadRequest(errors);

    var ok = service.UpdateStatus(id, dto.Status);
    return ok
        ? Results.NoContent()
        : Results.NotFound();
});

// =======================
// Validation helper
// =======================

static Dictionary<string, string[]>? Validate(object dto)
{
    var context = new ValidationContext(dto);
    var results = new List<ValidationResult>();

    var isValid = Validator.TryValidateObject(
        dto,
        context,
        results,
        validateAllProperties: true
    );

    if (isValid)
        return null;

    var dict = new Dictionary<string, List<string>>();

    foreach (var r in results)
    {
        var key = r.MemberNames.FirstOrDefault() ?? "_";
        if (!dict.ContainsKey(key))
            dict[key] = new List<string>();

        dict[key].Add(r.ErrorMessage ?? "Validation error.");
    }

    return dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());
}

app.Run();
