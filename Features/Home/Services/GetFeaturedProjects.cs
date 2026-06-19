using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Home.Services;

public class GetFeaturedProjects(AppDbContext db)
{
    public const int FeaturedCount = 3;

    public async Task<IReadOnlyList<Project>> HandleAsync(CancellationToken ct = default)
        => await db.Projects
            .AsNoTracking()
            .OrderBy(p => p.SortOrder)
            .Take(FeaturedCount)
            .ToListAsync(ct);
}
