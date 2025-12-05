using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Dtos.BlogImage;

namespace CodePulse.API.Repositories.Interfaces;

public interface IBlogImageRepository
{
    Task<BlogImage>Upload(IFormFile file, BlogImage blogImage);
    Task<IEnumerable<BlogImage>>GetAllAsync();
}
