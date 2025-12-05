using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interfaces;

namespace CodePulse.API.Repositories.Implementations;

public class BlogImageRepository : IBlogImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _dbContext;

    public BlogImageRepository(
        IWebHostEnvironment webHostEnvironment, 
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext dbContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public IWebHostEnvironment WebHostEnvironment { get; }

    public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
    {
        var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");

        using var stream = new FileStream(localPath, FileMode.Create);

        await file.CopyToAsync(stream);

        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

        blogImage.Url = urlPath;

        await _dbContext.BlogImages.AddAsync(blogImage);
        await _dbContext.SaveChangesAsync();

        return blogImage;
    }
}
