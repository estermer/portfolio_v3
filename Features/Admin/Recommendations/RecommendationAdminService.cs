using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Admin.Recommendations;

public record RecommendationUpsertRequest(
    int Id,
    string AuthorName,
    string AuthorTitle,
    string Content,
    string? LinkedInUrl,
    int SortOrder);

public class RecommendationAdminService(AppDbContext db)
{
    public async Task<IReadOnlyList<Recommendation>> ListAsync(CancellationToken ct = default)
        => await db.Recommendations.OrderBy(r => r.SortOrder).ToListAsync(ct);

    public async Task<Recommendation?> GetAsync(int id, CancellationToken ct = default)
        => await db.Recommendations.FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<int> UpsertAsync(RecommendationUpsertRequest req, CancellationToken ct = default)
    {
        Recommendation? entity = req.Id == 0
            ? null
            : await db.Recommendations.FirstOrDefaultAsync(r => r.Id == req.Id, ct);

        if (entity is null)
        {
            entity = new Recommendation();
            db.Recommendations.Add(entity);
        }

        entity.AuthorName = req.AuthorName.Trim();
        entity.AuthorTitle = req.AuthorTitle.Trim();
        entity.Content = req.Content.Trim();
        entity.LinkedInUrl = string.IsNullOrWhiteSpace(req.LinkedInUrl) ? null : req.LinkedInUrl.Trim();
        entity.SortOrder = req.SortOrder;

        await db.SaveChangesAsync(ct);
        return entity.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Recommendations.FirstOrDefaultAsync(r => r.Id == id, ct);
        if (entity is null) return;
        db.Recommendations.Remove(entity);
        await db.SaveChangesAsync(ct);
    }
}
