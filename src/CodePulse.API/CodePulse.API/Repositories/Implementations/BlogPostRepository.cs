using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interfaces;

namespace CodePulse.API.Repositories.Implementations;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BlogPostRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
       await _dbContext.BlogPost.AddAsync(blogPost);
       await _dbContext.SaveChangesAsync();

       return blogPost;
    }
}
