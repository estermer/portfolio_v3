using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Admin.Skills;

public record SkillUpsertRequest(int Id, string Name, string Category, int SortOrder);

public class SkillAdminService(AppDbContext db)
{
    public async Task<IReadOnlyList<Skill>> ListAsync(CancellationToken ct = default)
        => await db.Skills.OrderBy(s => s.Category).ThenBy(s => s.SortOrder).ToListAsync(ct);

    public async Task<Skill?> GetAsync(int id, CancellationToken ct = default)
        => await db.Skills.FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<int> UpsertAsync(SkillUpsertRequest req, CancellationToken ct = default)
    {
        Skill? entity = req.Id == 0
            ? null
            : await db.Skills.FirstOrDefaultAsync(s => s.Id == req.Id, ct);

        if (entity is null)
        {
            entity = new Skill();
            db.Skills.Add(entity);
        }

        entity.Name = req.Name.Trim();
        entity.Category = req.Category.Trim();
        entity.SortOrder = req.SortOrder;

        await db.SaveChangesAsync(ct);
        return entity.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Skills.FirstOrDefaultAsync(s => s.Id == id, ct);
        if (entity is null) return;
        db.Skills.Remove(entity);
        await db.SaveChangesAsync(ct);
    }
}
