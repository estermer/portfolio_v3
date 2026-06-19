using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Recommendations.Services;

public class GetRecommendations(AppDbContext db)
{
    public async Task<IReadOnlyList<Recommendation>> HandleAsync(CancellationToken ct = default)
        => await db.Recommendations
            .AsNoTracking()
            .OrderBy(r => r.SortOrder)
            .ToListAsync(ct);
}
