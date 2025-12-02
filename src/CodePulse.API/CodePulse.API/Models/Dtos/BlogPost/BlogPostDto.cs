using CodePulse.API.Models.Dtos.Category;

namespace CodePulse.API.Models.Dtos.BlogPost;

public class BlogPostDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string FeaturedImageUrl { get; set; } = string.Empty;
    public string UrlHandle { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Author { get; set; } = string.Empty;
    public bool IsVisible { get; set; }

    public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}
