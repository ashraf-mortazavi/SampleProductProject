namespace SampleProductProject.ViewModels;

public record struct UserProductsResult
{
    public UserProductsResult(bool success, string status, List<string> errors, List<string> warnings)
    {
        Success = success;
        Status = status;
        Errors = errors;
        Warnings = warnings;
    }
    public bool Success { get; set; }
    public string Status { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
}
