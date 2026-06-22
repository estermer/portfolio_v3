using System.Text;
using EricstermerCom.Components;
using EricstermerCom.Data;
using EricstermerCom.Features.Admin;
using EricstermerCom.Features.Admin.Projects;
using EricstermerCom.Features.Admin.Recommendations;
using EricstermerCom.Features.Admin.Skills;
using EricstermerCom.Features.Contact.Services;
using EricstermerCom.Features.Experience.Services;
using EricstermerCom.Features.Home.Services;
using EricstermerCom.Features.Projects.Services;
using EricstermerCom.Features.Recommendations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("Default")
        ?? "Data Source=ericstermer.db";

    if (connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase)
        || connectionString.Contains("Server=", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseSqlite(connectionString);
    }
});

builder.Services.AddAuthentication("EricstermerCookie")
    .AddCookie("EricstermerCookie", options =>
    {
        options.Cookie.Name = "Ericstermer.Auth";
        options.LoginPath = "/admin/login";
        options.LogoutPath = "/admin/logout";
        options.AccessDeniedPath = "/admin/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireClaim("admin", "true"));
});
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<AdminAuthService>();
builder.Services.AddScoped<GetFeaturedProjects>();
builder.Services.AddScoped<GetSkills>();
builder.Services.AddScoped<GetProjects>();
builder.Services.AddScoped<GetRecommendations>();
builder.Services.AddScoped<SubmitContact>();
builder.Services.AddScoped<ProjectAdminService>();
builder.Services.AddScoped<SkillAdminService>();
builder.Services.AddScoped<RecommendationAdminService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("Default")
        ?? "Data Source=ericstermer.db";

    var isPostgres = connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase)
        || connectionString.Contains("Server=", StringComparison.OrdinalIgnoreCase);

    if (isPostgres)
    {
        db.Database.EnsureCreated();
    }
    else
    {
        db.Database.Migrate();
    }

    await SeedData.RunAsync(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/not-found", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPost("/api/admin/logout", async (HttpContext ctx, AdminAuthService auth) =>
{
    await auth.SignOutAsync();
    ctx.Response.Redirect("/");
    return Results.Empty;
}).DisableAntiforgery();

app.MapGet("/sitemap.xml", (HttpContext ctx) =>
{
    var baseUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host.Value}".TrimEnd('/');
    var urls = new[]
    {
        ("/", "1.0", "weekly"),
        ("/experience", "0.8", "monthly"),
        ("/projects", "0.9", "weekly"),
        ("/recommendations", "0.7", "monthly"),
        ("/contact", "0.6", "monthly"),
    };

    var xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
              "<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n" +
              string.Join("\n", urls.Select(u =>
                  $"  <url>\n    <loc>{baseUrl}{u.Item1}</loc>\n    <changefreq>{u.Item3}</changefreq>\n    <priority>{u.Item2}</priority>\n  </url>")) +
              "\n</urlset>\n";

    return Results.Content(xml, "application/xml", Encoding.UTF8);
});

app.MapPost("/api/admin/login", async (HttpContext ctx, AdminAuthService auth) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var email = form["Email"].ToString();
    var password = form["Password"].ToString();
    var returnUrl = form["ReturnUrl"].ToString();
    var result = await auth.SignInAsync(email, password);
    if (result.Success)
    {
        var dest = !string.IsNullOrWhiteSpace(returnUrl) && Uri.IsWellFormedUriString(returnUrl, UriKind.Relative)
            ? returnUrl
            : "/admin";
        ctx.Response.Redirect(dest);
        return Results.Empty;
    }

    var back = "/admin/login?error=" + Uri.EscapeDataString(result.Error ?? "Sign-in failed.");
    if (!string.IsNullOrWhiteSpace(email))
        back += "&email=" + Uri.EscapeDataString(email);
    if (!string.IsNullOrWhiteSpace(returnUrl))
        back += "&returnUrl=" + Uri.EscapeDataString(returnUrl);
    ctx.Response.Redirect(back);
    return Results.Empty;
}).DisableAntiforgery();

app.Run();
