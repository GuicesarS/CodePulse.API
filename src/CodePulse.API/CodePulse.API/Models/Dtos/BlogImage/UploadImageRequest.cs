namespace CodePulse.API.Models.Dtos.BlogImage;

public class UploadImageRequest
{
    public IFormFile File { get; set; } = null!;
    public string FileName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}
