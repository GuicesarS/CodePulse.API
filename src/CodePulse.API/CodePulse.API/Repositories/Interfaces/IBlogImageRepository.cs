using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interfaces;

public interface IBlogImageRepository
{
    Task<BlogImage>Upload(IFormFile file, BlogImage blogImage);
}
