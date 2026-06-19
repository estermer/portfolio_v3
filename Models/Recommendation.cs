namespace EricstermerCom.Models;

public class Recommendation
{
    public int Id { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorTitle { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? LinkedInUrl { get; set; }
    public int SortOrder { get; set; }
}
