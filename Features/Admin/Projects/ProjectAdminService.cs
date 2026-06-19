using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Admin.Projects;

public record ProjectUpsertRequest(
    int Id,
    string Title,
    string Description,
    string TechTags,
    string ImageUrl,
    string? Url,
    int SortOrder);

public class ProjectAdminService(AppDbContext db)
{
    public async Task<IReadOnlyList<Project>> ListAsync(CancellationToken ct = default)
        => await db.Projects.OrderBy(p => p.SortOrder).ToListAsync(ct);

    public async Task<Project?> GetAsync(int id, CancellationToken ct = default)
        => await db.Projects.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<int> UpsertAsync(ProjectUpsertRequest req, CancellationToken ct = default)
    {
        Project? entity = req.Id == 0
            ? null
            : await db.Projects.FirstOrDefaultAsync(p => p.Id == req.Id, ct);

        if (entity is null)
        {
            entity = new Project();
            db.Projects.Add(entity);
        }

        entity.Title = req.Title.Trim();
        entity.Description = req.Description.Trim();
        entity.TechTags = req.TechTags.Trim();
        entity.ImageUrl = req.ImageUrl.Trim();
        entity.Url = string.IsNullOrWhiteSpace(req.Url) ? null : req.Url.Trim();
        entity.SortOrder = req.SortOrder;

        await db.SaveChangesAsync(ct);
        return entity.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Projects.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity is null) return;
        db.Projects.Remove(entity);
        await db.SaveChangesAsync(ct);
    }
}
