using System.Security.Claims;
using EricstermerCom.Data;
using EricstermerCom.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Features.Admin;

public record LoginResult(bool Success, string? Error = null);

public class AdminAuthService(AppDbContext db, IHttpContextAccessor httpContextAccessor)
{
    public async Task<LoginResult> SignInAsync(string email, string password, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return new LoginResult(false, "Email and password are required.");

        var user = await db.SiteUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email.Trim(), ct);

        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return new LoginResult(false, "Invalid email or password.");

        var http = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No active HttpContext.");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.DisplayName),
            new("admin", "true"),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await http.SignInAsync("EricstermerCookie", principal,
            new AuthenticationProperties { IsPersistent = true });

        return new LoginResult(true);
    }

    public async Task SignOutAsync()
    {
        var http = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No active HttpContext.");
        await http.SignOutAsync("EricstermerCookie");
    }
}
