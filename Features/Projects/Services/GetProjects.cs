using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Projects.Services;

public record ProjectListItem(
    int Id,
    string Title,
    string Description,
    IReadOnlyList<string> Tags,
    string ImageUrl,
    string? Url);

public record ProjectsByCategory(string Category, IReadOnlyList<ProjectListItem> Projects);

public class GetProjects(AppDbContext db)
{
    public async Task<IReadOnlyList<ProjectListItem>> HandleAsync(
        string? category = null,
        CancellationToken ct = default)
    {
        IQueryable<Project> query = db.Projects.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(category) && category != "all")
            query = query.Where(p => p.TechTags == category);

        query = query.OrderBy(p => p.SortOrder);

        var projects = await query.ToListAsync(ct);

        return projects
            .Select(p => new ProjectListItem(
                p.Id,
                p.Title,
                p.Description,
                p.TagList.ToList(),
                p.ImageUrl,
                p.Url))
            .ToList();
    }

    public async Task<IReadOnlyList<string>> GetCategoriesAsync(CancellationToken ct = default)
    {
        var tags = await db.Projects
            .AsNoTracking()
            .Select(p => p.TechTags)
            .Where(t => t != null && t != "")
            .Distinct()
            .ToListAsync(ct);

        return tags
            .SelectMany(t => t.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Distinct()
            .OrderBy(t => t)
            .ToList();
    }
}
