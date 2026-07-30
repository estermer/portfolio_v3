using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Data;

public static class SeedData
{
    public static async Task RunAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();

        if (!await db.Projects.AnyAsync())
            await SeedProjectsAsync(db);

        if (!await db.Skills.AnyAsync())
            await SeedSkillsAsync(db);

        if (!await db.Recommendations.AnyAsync())
            await SeedRecommendationsAsync(db);

        if (!await db.SiteUsers.AnyAsync())
            await SeedAdminUserAsync(services, db);

        await db.SaveChangesAsync();
    }

    private static async Task SeedAdminUserAsync(IServiceProvider services, AppDbContext db)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var email = config["Admin:Email"] ?? "admin@ericstermer.com";
        var password = config["Admin:Password"] ?? "Admin123!";
        var displayName = config["Admin:DisplayName"] ?? "Eric Stermer";

        var user = new SiteUser
        {
            Email = email,
            DisplayName = displayName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
        db.SiteUsers.Add(user);

        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");
        logger.LogWarning("Seeded admin user '{Email}'. CHANGE THE DEFAULT PASSWORD before going to production.", email);
    }

    private static async Task SeedProjectsAsync(AppDbContext db)
    {
        var projects = new List<Project>
        {
            new() { Title = "Cloud Infrastructure Migration", Description = "Led the migration of on-premise infrastructure to AWS, reducing deployment time by 80% and cutting hosting costs by 40%. Designed the CI/CD pipeline architecture and managed the phased rollout across engineering teams.", TechTags = "AWS, Terraform, Docker, CI/CD, GitHub Actions", ImageUrl = "", Url = null, SortOrder = 10 },
            new() { Title = "Internal Developer Platform", Description = "Architected and built internal tooling used by engineers daily for deployment automation, environment provisioning, and monitoring. Reduced onboarding time for new developers from days to hours.", TechTags = ".NET, React, Azure, DevOps, PostgreSQL", ImageUrl = "", Url = null, SortOrder = 20 },
            new() { Title = "IT Operations Overhaul", Description = "Consolidated vendor relationships, implemented centralized monitoring, and established security compliance processes. Improved system uptime to 99.9% and reduced incident response time by 60%.", TechTags = "Infrastructure, Monitoring, Security, Vendor Management", ImageUrl = "", Url = null, SortOrder = 30 },
        };

        await db.Projects.AddRangeAsync(projects);
    }

    private static async Task SeedSkillsAsync(AppDbContext db)
    {
        var skills = new List<Skill>
        {
            new() { Category = "Full-Stack Development", Name = "React", SortOrder = 10 },
            new() { Category = "Full-Stack Development", Name = "JavaScript", SortOrder = 20 },
            new() { Category = "Full-Stack Development", Name = "C# / .NET", SortOrder = 30 },
            new() { Category = "Full-Stack Development", Name = "Blazor", SortOrder = 40 },
            new() { Category = "Full-Stack Development", Name = "SQL / PostgreSQL", SortOrder = 50 },
            new() { Category = "Full-Stack Development", Name = "Elixir / Phoenix", SortOrder = 60 },
            new() { Category = "Full-Stack Development", Name = "Redis", SortOrder = 70 },
            new() { Category = "Full-Stack Development", Name = "Salesforce (front-end integration)", SortOrder = 80 },
            new() { Category = "Infrastructure & Operations", Name = "AWS", SortOrder = 10 },
            new() { Category = "Infrastructure & Operations", Name = "Docker", SortOrder = 20 },
            new() { Category = "Infrastructure & Operations", Name = "CI/CD Pipelines", SortOrder = 30 },
            new() { Category = "Infrastructure & Operations", Name = "Monitoring & Observability", SortOrder = 40 },
            new() { Category = "Infrastructure & Operations", Name = "Linux Administration", SortOrder = 50 },
            new() { Category = "Infrastructure & Operations", Name = "On-prem infrastructure", SortOrder = 60 },
            new() { Category = "Infrastructure & Operations", Name = "Cloud cost optimization", SortOrder = 70 },
            new() { Category = "Team Leadership", Name = "Engineering Team Management", SortOrder = 10 },
            new() { Category = "Team Leadership", Name = "Hiring & Onboarding", SortOrder = 20 },
            new() { Category = "Team Leadership", Name = "Mentorship & Code Review", SortOrder = 30 },
            new() { Category = "Team Leadership", Name = "Technical training & teaching", SortOrder = 40 },
            new() { Category = "Team Leadership", Name = "Agile / Scrum", SortOrder = 50 },
            new() { Category = "Team Leadership", Name = "Cross-Functional Collaboration", SortOrder = 60 },
            new() { Category = "Cloud & DevOps", Name = "Terraform", SortOrder = 10 },
            new() { Category = "Cloud & DevOps", Name = "GitHub Actions", SortOrder = 20 },
            new() { Category = "Cloud & DevOps", Name = "Kubernetes", SortOrder = 30 },
            new() { Category = "Cloud & DevOps", Name = "Infrastructure as Code", SortOrder = 40 },
            new() { Category = "Architecture & Design", Name = "System Architecture", SortOrder = 10 },
            new() { Category = "Architecture & Design", Name = "API Design", SortOrder = 20 },
            new() { Category = "Architecture & Design", Name = "Scalability & Performance", SortOrder = 30 },
            new() { Category = "Architecture & Design", Name = "Conversational AI / Chatbots", SortOrder = 40 },
            new() { Category = "Architecture & Design", Name = "Legacy code modernization", SortOrder = 50 },
            new() { Category = "IT Operations", Name = "Vendor Management", SortOrder = 10 },
            new() { Category = "IT Operations", Name = "Budget & Resource Planning", SortOrder = 20 },
            new() { Category = "IT Operations", Name = "Incident Response", SortOrder = 30 },
        };

        await db.Skills.AddRangeAsync(skills);
    }

    private static async Task SeedRecommendationsAsync(AppDbContext db)
    {
        var recs = new List<Recommendation>
        {
            new()
            {
                AuthorName = "Dave Costantino",
                AuthorTitle = "CTO, Datarista Inc.",
                Content = "We hired Eric right out of General Assembly. I knew in our first conversation that he was smart and could get things done! This proved true throughout his time at Datarista. He initially started first on the front end, working with React and Redux, then moved into a full stack role where he worked on parts of our back end, as well. I highly recommend Eric for a software engineering position.",
                LinkedInUrl = "https://www.linkedin.com/in/davecostantino/",
                SortOrder = 10
            },
            new()
            {
                AuthorName = "Pat Sabatino",
                AuthorTitle = "CEO, Datarista Inc.",
                Content = "We hired Eric for his first professional developer job coming out of the General Assembly Developer Immersive. From the start he was clearly a very smart guy and we felt that with time he would learn the business aspects of being a developer. He quickly adapted his problem solving and critical thinking and we were proud to promote him from Jr. Developer to Full Stack Engineer, a title he rightly earned. He is personable, optimistic and hard working and delivers results. I would highly recommend Eric as a developer.",
                LinkedInUrl = "https://www.linkedin.com/in/patsabatino/",
                SortOrder = 20
            },
        };

        await db.Recommendations.AddRangeAsync(recs);
    }
}
