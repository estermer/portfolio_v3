using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
    public DbSet<ContactSubmission> ContactSubmissions => Set<ContactSubmission>();
    public DbSet<SiteUser> SiteUsers => Set<SiteUser>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Project>(e =>
        {
            e.HasIndex(p => p.SortOrder);
            e.Property(p => p.Title).HasMaxLength(200).IsRequired();
            e.Property(p => p.Description).HasMaxLength(1000).IsRequired();
            e.Property(p => p.TechTags).HasMaxLength(500);
            e.Property(p => p.ImageUrl).HasMaxLength(500).IsRequired();
            e.Property(p => p.Url).HasMaxLength(500);
        });

        b.Entity<Skill>(e =>
        {
            e.HasIndex(s => new { s.Category, s.SortOrder });
            e.Property(s => s.Name).HasMaxLength(100).IsRequired();
            e.Property(s => s.Category).HasMaxLength(100).IsRequired();
        });

        b.Entity<Recommendation>(e =>
        {
            e.HasIndex(r => r.SortOrder);
            e.Property(r => r.AuthorName).HasMaxLength(200).IsRequired();
            e.Property(r => r.AuthorTitle).HasMaxLength(200).IsRequired();
            e.Property(r => r.Content).HasMaxLength(4000).IsRequired();
            e.Property(r => r.LinkedInUrl).HasMaxLength(500);
        });

        b.Entity<ContactSubmission>(e =>
        {
            e.HasIndex(c => c.CreatedAt);
            e.Property(c => c.Name).HasMaxLength(200).IsRequired();
            e.Property(c => c.Email).HasMaxLength(320).IsRequired();
            e.Property(c => c.Message).HasMaxLength(4000).IsRequired();
        });

        b.Entity<SiteUser>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Email).HasMaxLength(320).IsRequired();
            e.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
            e.Property(u => u.DisplayName).HasMaxLength(200).IsRequired();
        });
    }
}
