using System.ComponentModel.DataAnnotations;
using EricstermerCom.Data;
using EricstermerCom.Models;

namespace EricstermerCom.Features.Contact.Services;

public class ContactSubmissionRequest
{
    [Required, StringLength(200, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(320)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(4000, MinimumLength = 10)]
    public string Message { get; set; } = string.Empty;
}

public enum SubmitContactStatus
{
    Success,
    ValidationFailed,
    Error
}

public record SubmitContactResult(SubmitContactStatus Status, string? Error = null);

public class SubmitContact(AppDbContext db)
{
    public async Task<SubmitContactResult> HandleAsync(ContactSubmissionRequest request, CancellationToken ct = default)
    {
        var ctx = new ValidationContext(request);
        var errors = new List<ValidationResult>();
        if (!Validator.TryValidateObject(request, ctx, errors, validateAllProperties: true))
            return new SubmitContactResult(SubmitContactStatus.ValidationFailed,
                string.Join(" ", errors.Select(e => e.ErrorMessage)));

        try
        {
            db.ContactSubmissions.Add(new ContactSubmission
            {
                Name = request.Name.Trim(),
                Email = request.Email.Trim(),
                Message = request.Message.Trim()
            });
            await db.SaveChangesAsync(ct);
            return new SubmitContactResult(SubmitContactStatus.Success);
        }
        catch (Exception ex)
        {
            return new SubmitContactResult(SubmitContactStatus.Error, ex.Message);
        }
    }
}
