namespace EricstermerCom.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    private string _techTags = string.Empty;

    public string TechTags
    {
        get => _techTags;
        set => _techTags = value ?? string.Empty;
    }

    public string ImageUrl { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int SortOrder { get; set; }

    public IEnumerable<string> TagList =>
        string.IsNullOrWhiteSpace(TechTags)
            ? Array.Empty<string>()
            : TechTags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}
