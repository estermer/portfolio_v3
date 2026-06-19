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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")
        ?? "Data Source=ericstermer.db"));

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
    db.Database.Migrate();
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
