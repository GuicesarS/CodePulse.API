using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interfaces;

public interface IBlogPostRepository
{
    Task<BlogPost> CreateAsync(BlogPost blogPost);
    Task<IEnumerable<BlogPost>> GetAllAsync();
}
