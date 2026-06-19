using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Experience.Services;

public record SkillGroup(string Category, IReadOnlyList<Skill> Skills);

public class GetSkills(AppDbContext db)
{
    public async Task<IReadOnlyList<SkillGroup>> HandleAsync(CancellationToken ct = default)
    {
        var skills = await db.Skills
            .AsNoTracking()
            .OrderBy(s => s.Category)
            .ThenBy(s => s.SortOrder)
            .ToListAsync(ct);

        return skills
            .GroupBy(s => s.Category)
            .Select(g => new SkillGroup(g.Key, g.ToList()))
            .ToList();
    }
}
